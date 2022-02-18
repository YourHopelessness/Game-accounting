using System.Net;

namespace GameAccounting.BL.Models
{
    public class ApiResponseException : Exception
    {
        public HttpStatusCode Code { get; set; }

        public ApiResponseException(HttpStatusCode code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}
