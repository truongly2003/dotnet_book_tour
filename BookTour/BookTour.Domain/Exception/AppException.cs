using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Exception
{
    public class AppException : IOException
    {
        public ErrorCode ErrorCode { get; }
        public HttpStatusCode StatusCode { get; }

        public AppException(ErrorCode errorCode)
            : base(errorCode.GetMessage()) // Gọi constructor của base (Exception)
        {
            ErrorCode = errorCode;
            StatusCode = errorCode.GetStatusCode(); // Lấy HttpStatusCode tương ứng từ ErrorCode
        }
    }
}
