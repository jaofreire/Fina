using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        public Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest modelRequest)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest modelRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest modelRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest modelRequest)
        {
            throw new NotImplementedException();
        }

        

        

        public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest modelRequest)
        {
            throw new NotImplementedException();
        }
    }
}
