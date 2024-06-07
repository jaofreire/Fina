using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core.Requests
{
    public abstract class PagedRequest : Request
    {
        public int PageSize { get; set; } = 25;
        public int PageNumber { get; set; } = 1;
    }
}
