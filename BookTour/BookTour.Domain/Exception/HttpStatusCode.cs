using System;
using System.Net;  

namespace BookTour.Domain.Exception
{
    // Đổi tên lớp này để tránh trùng với HttpStatusCode của .NET
    public static class CustomHttpStatusCode
    {
        // Sử dụng System.Net.HttpStatusCode thay vì HttpStatusCode của bạn
        public static HttpStatusCode GetStatusCode(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.TOUR_NOT_EXISTED => HttpStatusCode.NotFound,
                ErrorCode.UNAUTHENTICATED => HttpStatusCode.Forbidden,
                ErrorCode.USER_NOT_EXISTED => HttpStatusCode.NotFound,
                ErrorCode.USER_EXISTS => HttpStatusCode.BadRequest,
                ErrorCode.PASSWORD_TOO_SHORT => HttpStatusCode.BadRequest,
                ErrorCode.CUSTOMER_NOT_EXIST => HttpStatusCode.BadRequest,
                ErrorCode.USER_OR_PASSWORD_WRONG => HttpStatusCode.BadRequest,
                ErrorCode.OAUTH_ERROR => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
