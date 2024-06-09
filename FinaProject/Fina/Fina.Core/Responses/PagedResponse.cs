using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fina.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(TData? data, int totalDataCount, int currentPage = 1, int pageSize = 25) : base(data)
        {
            Data = data;
            TotalDataCount = totalDataCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(TData? data, int code = 200, string? message = null) : base(data, code, message)
        {
        }

        public int PageSize { get; set; } = 25;
        public int CurrentPage { get; set; }
        public int TotalDataCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalDataCount / (double)PageSize);
    }
}
