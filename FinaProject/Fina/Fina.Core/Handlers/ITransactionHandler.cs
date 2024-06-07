using Fina.Core.Responses;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;

namespace Fina.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<List<Transaction>>> GetAllAsync(GetAllTransactionRequest modelRequest);
        Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest modelRequest);
        Task<Response<List<Transaction>>> GetByPeriodAsync(GetTransactionByPeriodRequest modelRequest);
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest modelRequest);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest modelRequest);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest modelRequest);
    }
}
