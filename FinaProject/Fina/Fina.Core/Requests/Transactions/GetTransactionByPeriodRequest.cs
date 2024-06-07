using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Transactions
{
    public class GetTransactionByPeriodRequest : PagedRequest
    {
        //if the user not pass a date, will get all transactions from first of the mouth
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
