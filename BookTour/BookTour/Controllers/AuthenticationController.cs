using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookTour.Application.Interface;
using BookTour.Application.Dto;
using BookTour.Application.Service;

namespace BookTour.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;


        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
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
    }
}
