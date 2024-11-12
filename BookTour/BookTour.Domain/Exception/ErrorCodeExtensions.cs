using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Exception
{
    public static class ErrorCodeExtensions
    {
        public static string GetMessage(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.TOUR_NOT_EXISTED => "Tour not existed",
                ErrorCode.UNAUTHENTICATED => "You do not have permission",
                ErrorCode.USER_NOT_EXISTED => "User not existed",
                ErrorCode.USER_EXISTS => "User already exists",
                ErrorCode.PASSWORD_TOO_SHORT => "Password must be at least 6 characters long",
                ErrorCode.CUSTOMER_NOT_EXIST => "Customer not found",
                ErrorCode.USER_OR_PASSWORD_WRONG => "User or password is incorrect",
                ErrorCode.OAUTH_ERROR => "Oauth2 authentication failed",
                _ => "Unknown error"
            };
        }
    }
}
