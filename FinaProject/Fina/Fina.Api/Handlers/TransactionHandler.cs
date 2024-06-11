using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Fina.Core.Enums;
using Fina.Api.Data;
using Microsoft.EntityFrameworkCore;
using Fina.Core.Common;

namespace Fina.Api.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        private readonly FinaDbContext _dbContext;

        public TransactionHandler(FinaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest modelRequest)
        {
            try
            {
                var transaction = await _dbContext.Transactions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == modelRequest.Id && x.UserId == modelRequest.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, $"The transaction with Id: {modelRequest.Id} not exist");

                return new Response<Transaction?>(transaction, 200, $"Transaction gets successfully");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                Console.Write(ex.Message);
                Console.Write(ex.InnerException);

                return new Response<Transaction?>(null, 500, "There is not possible gets the transaction");
            }

        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest modelRequest)
        {
            try
            {
                modelRequest.StartDate ??= DateTime.Now.GetFirstDay();
                modelRequest.EndDate ??= DateTime.Now.GetLastDay();

                var query = _dbContext.Transactions
                    .AsNoTracking()
                    .Where(x => x.UserId == modelRequest.UserId &&
                                x.CreatedAt >= modelRequest.StartDate &&
                                x.CreatedAt <= modelRequest.EndDate)
                    .OrderBy(x => x.Title)
                    .ThenBy(x => x.Value)
                    .ThenBy(x => x.CreatedAt);

                var transactions = await query
                    .Skip((modelRequest.PageNumber - 1) * modelRequest.PageSize)
                    .Take(modelRequest.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>>(transactions, count, modelRequest.PageNumber, modelRequest.PageSize);

            }
            catch (Exception ex)
            {

                Console.Write(ex.StackTrace);
                Console.Write(ex.Message);
                Console.Write(ex.InnerException);

                return new PagedResponse<List<Transaction>>(null, 500, "There is not possible gets the transactions");
            }

        }

        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest modelRequest)
        {
            try
            {
                if (modelRequest is { Type: ETransactionType.Paid, Value: > 0 })
                    modelRequest.Value *= -1;

                var newTransaction = new Transaction()
                {
                    Title = modelRequest.Title,
                    Type = modelRequest.Type,
                    Value = modelRequest.Value,
                    CategoryId = modelRequest.CategoryId
                };

                await _dbContext.Transactions.AddAsync(newTransaction);
                await _dbContext.SaveChangesAsync();

                return new Response<Transaction?>(newTransaction, 200, "Transaction created successfully");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                Console.Write(ex.Message);
                Console.Write(ex.InnerException);

                return new Response<Transaction?>(null, 500, "There is not possible create the transaction");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest modelRequest)
        {
            try
            {
                var transactionUpdate = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == modelRequest.Id && x.UserId == modelRequest.UserId);

                if (transactionUpdate == null)
                    return new Response<Transaction?>(null, 404, $"The transaction with Id: {modelRequest.Id} not exist");

                transactionUpdate.Title = modelRequest.Title;
                transactionUpdate.Type = modelRequest.Type;
                transactionUpdate.Value = modelRequest.Value;
                transactionUpdate.CategoryId = modelRequest.CategoryId;

                _dbContext.Transactions.Update(transactionUpdate);
                await _dbContext.SaveChangesAsync();

                return new Response<Transaction?>(transactionUpdate, 200, "Transaction updated successfully");
            }
            catch (Exception ex)
            {

                Console.Write(ex.StackTrace);
                Console.Write(ex.Message);
                Console.Write(ex.InnerException);

                return new Response<Transaction?>(null, 500, "There is not possible update the transaction");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest modelRequest)
        {
            try
            {
                var transactionDelete = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == modelRequest.Id && x.UserId == modelRequest.UserId);

                if (transactionDelete == null)
                    return new Response<Transaction?>(null, 404, $"The transaction with Id: {modelRequest.Id} not exist");

                _dbContext.Transactions.Remove(transactionDelete);
                await _dbContext.SaveChangesAsync();

                return new Response<Transaction?>(transactionDelete, 200, "Transaction removed successfully");
            }
            catch (Exception ex)
            {

                Console.Write(ex.StackTrace);
                Console.Write(ex.Message);
                Console.Write(ex.InnerException);

                return new Response<Transaction?>(null, 500, "There is not possible remove the transaction");
            }
        }

        
    }
}
