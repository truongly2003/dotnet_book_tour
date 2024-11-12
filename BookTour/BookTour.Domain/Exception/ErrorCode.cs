using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Exception
{
    public enum ErrorCode
    {
        TOUR_NOT_EXISTED = 1001,  // Tour không tồn tại
        UNAUTHENTICATED = 1002,   // Chưa xác thực
        USER_NOT_EXISTED = 1003,  // Người dùng không tồn tại
        USER_EXISTS = 1004,       // Người dùng đã tồn tại
        PASSWORD_TOO_SHORT = 1005, // Mật khẩu phải ít nhất 6 ký tự
        CUSTOMER_NOT_EXIST = 1006, // Khách hàng không tồn tại
        USER_OR_PASSWORD_WRONG = 1007, // Người dùng hoặc mật khẩu sai
        OAUTH_ERROR = 1008,       // Lỗi OAuth2

    }
}
