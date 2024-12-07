using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookTour.Application.Interface;
using BookTour.Application.Dto;
using BookTour.Application.Service;
using BookTour.Domain.Entity;
using BookTour.Domain.Exception;

namespace BookTour.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;



        public AuthController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            LoginDTO result;
            try
            {
                result = await _authService.Login(request);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<LoginDTO>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<LoginDTO>
            {
                code = 1000,
                message = "Login successful",
                result = result
            };

            Console.WriteLine($"code: {response.code}, message: {response.message}, result: {response.result}");


            return Ok(response);
        }


        [HttpPost("oauth2/callback/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] Dictionary<string, string> payload)
        {
            try
            {
                // In payload để kiểm tra nội dung
                Console.WriteLine("Received payload: " + string.Join(", ", payload.Select(kv => kv.Key + "=" + kv.Value)));

                if (!payload.ContainsKey("code"))
                {
                    return BadRequest(new { message = "Missing authorization code." });
                }

                string code = payload["code"];
                Console.WriteLine("Received code : " + code);

                var response = await _authService.HandleGoogleLogin(code);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error during Google login: {ex.Message}" });
            }
        }


        // Endpoint cho login Facebook
        [HttpPost("oauth2/callback/facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody] Dictionary<string, string> payload)
        {
            try
            {
                if (!payload.ContainsKey("code"))
                {
                    return BadRequest(new { message = "Missing authorization code." });
                }

                string code = payload["code"];
                var response = await _authService.HandleFacebookLogin(code);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error during Facebook login: {ex.Message}" });
            }
        }

        [HttpPost("decode-token")]
        public async Task<IActionResult> DecodeToken([FromHeader] string Authorization)
        {
            try
            {
                var token = Authorization.Replace("Bearer ", "");
                var decodedToken = await _authService.DecodeTokenAsync(token);
                return Ok(new ApiResponse<Dictionary<string, object>>
                {
                    result = decodedToken
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Dictionary<string, object>>
                {
                    message = $"Invalid token: {ex.Message}"
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> createUser([FromBody] UserCreateRequest request)
        {
            try
            {
                // Kiểm tra xem email đã tồn tại hay chưa
                if (await _userService.ExistsByEmailAsync(request.Email))
                {
                    // Trả về phản hồi nếu email đã tồn tại
                    var errorResponse = new ApiResponse<UserDTO>
                    {
                        code = 1001,  // Mã lỗi email đã tồn tại
                        message = "Email is already registered",  // Thông báo lỗi
                        result = null  // Không có kết quả trả về
                    };
                    return BadRequest(errorResponse);  // Trả về mã lỗi 400
                }

                // Nếu email chưa tồn tại, tạo mới user
                var result = await _userService.AddUser(request);

                // Trả về thông báo thành công
                var response = new ApiResponse<User>
                {
                    code = 1000,  // Mã thành công
                    message = "Registration successful",  // Thông báo thành công
                    result = result
                };

                return Ok(response);  // Trả về kết quả thành công
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra trong quá trình tạo người dùng, trả về lỗi
                var errorResponse = new ApiResponse<UserDTO>
                {
                    code = 1002,  // Mã lỗi chung
                    message = ex.Message,  // Thông báo lỗi từ exception
                    result = null  // Không có kết quả trả về
                };
                return BadRequest(errorResponse);  // Trả về mã lỗi 400
            }
        }


        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAccount([FromBody] Dictionary<string, string> payload)
        {
            if (!payload.ContainsKey("token"))
            {
                return BadRequest(new ApiResponse<string>
                {
                    code = 400,
                    message = "Token is missing.",
                    result = null
                });
            }

            var token = payload["token"];
            Console.WriteLine($"Token verify: {token}");

            try
            {
                await _userService.VerifyEmail(token);

                return Ok(new ApiResponse<string>
                {
                    code = 200,
                    message = "Account verified successfully! You can now log in.",
                    result = null
                });
            }
            catch (AppException ex)
            {
                Console.WriteLine($"Error during email verification: {ex.Message}");

                return BadRequest(new ApiResponse<string>
                {
                    code = 400,
                    message = ex.Message,
                    result = null
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during email verification: {ex.Message}");

                return StatusCode(500, new ApiResponse<string>
                {
                    code = 500,
                    message = "An unexpected error occurred during email verification.",
                    result = null
                });
            }
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                // Lấy thông tin người dùng từ service
                var userResponse = await _userService.GetUserById(userId);

                // Trả về kết quả với mã thành công 200 OK
                var response = new ApiResponse<UserDTO>
                {
                    code = 200,
                    message = "User retrieved successfully",
                    result = userResponse
                };

                return Ok(response);  // Trả về thông tin người dùng
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra, trả về lỗi 500 (Lỗi máy chủ)
                var errorResponse = new ApiResponse<UserDTO>
                {
                    code = 500,
                    message = ex.Message,
                    result = null
                };

                return StatusCode(500, errorResponse);
            }
        }



    }
}
