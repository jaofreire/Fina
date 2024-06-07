using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fina.Core.Responses
{
    public class Response<TData>
    {
        [JsonConstructor]
        public Response()
            => _code = 200;
        
        public Response(TData? data, int code = 200, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        private int _code = 200;

        public string? Message { get; set; }

        public TData? Data { get; set; }

        [JsonIgnore]
        public bool IsSuccess => _code >= 200 && _code <= 299;
    }
}
