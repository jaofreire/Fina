using Fina.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : Request
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ETransactionType Type { get; set; }
        public double Value { get; set; }
        public long CategoryId { get; set; }

    }
}
