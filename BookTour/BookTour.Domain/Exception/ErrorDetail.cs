using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Exception
{
    public class ErrorDetail
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ErrorDetail(ErrorCode errorCode)
        {
            this.Code = (int)errorCode;
            this.Message = errorCode.GetMessage(); // Lấy message từ Enum
            this.StatusCode = errorCode.GetStatusCode(); // Lấy StatusCode từ Enum
        }
    }
}
