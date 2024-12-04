using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Exception;
using BookTour.Domain.Interface;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JWT.Serializers;

namespace BookTour.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _signerKey;
        private readonly string _googleClientId;
        private readonly string _googleClientSecret;
        private readonly string _redirectUriGoogle = "http://localhost:3000/oauth2/redirect";
        private readonly string _facebookClientId;
        private readonly string _facebookClientSecret;
        private readonly string _redirectUriFacebook = "http://localhost:3000/oauth2/callback/facebook";
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICustomerRepository _customerRepository;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository, IRoleRepository roleRepository, ICustomerRepository customerRepository)
        {
            _signerKey = configuration["Jwt:SignerKey"];
            _googleClientId = configuration["OAuth:Google:ClientId"];
            _googleClientSecret = configuration["OAuth:Google:ClientSecret"];
            _facebookClientId = configuration["OAuth:Facebook:ClientId"];
            _facebookClientSecret = configuration["OAuth:Facebook:ClientSecret"];
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<LoginDTO> Login(LoginRequestDTO request)
        {
            Console.WriteLine("Request username : " + request.Username);

            User user = await _userRepository.findByUsername(request.Username);

            Console.WriteLine("User: ", user);

            if (user == null)
                throw new AppException(ErrorCode.USER_NOT_EXISTED);

            bool authenticated = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!authenticated)
                throw new AppException(ErrorCode.USER_OR_PASSWORD_WRONG);

            if (user.Role == null)
            {
                throw new AppException(ErrorCode.UNAUTHENTICATED);
            }

            int roleId = user.Role.RoleId;
            string roleName = user.Role.RoleName;
            string email = user.Email;
            string userName;
            int userId = user.UserId;

            Console.WriteLine("user_id : " + userId);
            if (roleId == 3)
            {
                var customer = await _customerRepository.FindByCustomerIdAsync(userId);
                if (customer == null)
                    throw new AppException(ErrorCode.CUSTOMER_NOT_EXIST);

                userName = customer.CustomerName;
            }
            else
            {
                userName = user.Username;
            }

            TokenInfo tokenInfo = await GenerateToken(user);

            var userDTO = new LoginDTO
            {
                id = userId,
                roleId = roleId,
                roleName = roleName,
                username = userName,
                token = tokenInfo.Token,
                expiryTime = tokenInfo.ExpiryDate,
                email = email
            };

            Console.WriteLine("user response :", userDTO);

            return userDTO;
        }





        private async Task<string> GetAccessTokenFromGoogle(string code)
        {
            Console.WriteLine("get access token code :" + code);
            Console.WriteLine("client_id :" + _googleClientId);
            Console.WriteLine("secret_id :" + _googleClientSecret);
            Console.WriteLine("uri  :" + _redirectUriGoogle);
            using var client = new HttpClient();
            var tokenUrl = "https://oauth2.googleapis.com/token";

            var requestData = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _googleClientId },
                { "client_secret", _googleClientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _redirectUriGoogle }
            };

            var response = await client.PostAsync(tokenUrl, new FormUrlEncodedContent(requestData));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Log the response body for debugging
            Console.WriteLine($"Google token response: {responseBody}");

            // Kiểm tra mã trạng thái HTTP
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch Google access token: {responseBody}");
            }

            try
            {
                // Deserialize response to GoogleTokenResponse object
                var responseData = JsonSerializer.Deserialize<GoogleTokenResponse>(responseBody);

                if (responseData != null && !string.IsNullOrEmpty(responseData.access_token))
                {
                    Console.WriteLine("responseData.access_token :" + responseData.access_token);
                    return responseData.access_token;

                }
                else
                {
                    throw new Exception("Access token not found in the response");
                }
            }
            catch (JsonException jsonEx)
            {
                // Log JSON deserialization errors
                throw new Exception("Error deserializing Google OAuth response: " + jsonEx.Message);
            }
        }

        public async Task<Dictionary<string, object>> HandleGoogleLogin(string code)
        {
            try
            {
                Console.WriteLine("code service :" + code);

                // Lấy accessToken từ Google
                var accessToken = await GetAccessTokenFromGoogle(code);
                Console.WriteLine($"access token: {accessToken}");  // In giá trị accessToken để kiểm tra

                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Access token is empty or invalid.");
                }

                var userInfo = await GetUserInfoFromGoogle(accessToken);

                Console.WriteLine("userInfo: " + JsonSerializer.Serialize(userInfo));  // Log thông tin người dùng

                // Lấy giá trị email từ userInfo
                if (userInfo.TryGetValue("email", out var emailObj) && emailObj != null)
                {
                    var email = emailObj.ToString()?.Trim();

                    if (string.IsNullOrEmpty(email))
                    {
                        throw new Exception("Email is null or empty after trimming.");
                    }

                    Console.WriteLine("email: " + email);  // Log giá trị email

                    // Lấy giá trị name từ userInfo
                    if (userInfo.TryGetValue("name", out var nameObj) && nameObj != null)
                    {
                        var name = nameObj.ToString()?.Trim();

                        if (string.IsNullOrEmpty(name))
                        {
                            throw new Exception("Name is null or empty after trimming.");
                        }

                        Console.WriteLine("name: " + name);  // Log giá trị name

                        // Kiểm tra người dùng đã tồn tại hay chưa
                        var existingUser = await _userRepository.checkUserExist(email);
                        Console.WriteLine("Existing user: " + existingUser);

                        if (existingUser != null)
                        {
                            return await GenerateUserTokenResponse(existingUser, name, email);
                        }

                        User newUser = await CreateUserFromGoogleInfo(name, email);
                        await _userRepository.saveUser(newUser);

                        Console.WriteLine("User new: " + newUser);
                        Console.WriteLine("name: " + name);
                        Console.WriteLine("email: " + email);

                        return await GenerateUserTokenResponse(newUser, name, email);
                    }
                    else
                    {
                        throw new Exception("Name is missing or invalid in the user info.");
                    }
                }
                else
                {
                    throw new Exception("Email is missing or invalid in the user info.");
                }
            }
            catch (Exception ex)
            {
                // Thêm log để kiểm tra chi tiết lỗi
                Console.WriteLine($"Error during Google login: {ex.Message}");
                throw new Exception($"Error during Google login: {ex.Message}", ex);
            }
        }



        private async Task<Dictionary<string, object>> GetUserInfoFromGoogle(string accessToken)
        {
            using var client = new HttpClient();

            // Sử dụng header Authorization thay vì truyền token trong URL
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var userInfoUrl = "https://www.googleapis.com/oauth2/v3/userinfo";

            // Gửi yêu cầu GET đến Google API
            var response = await client.GetAsync(userInfoUrl);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Kiểm tra mã trạng thái của phản hồi
            if (!response.IsSuccessStatusCode)
            {
                // Thêm log chi tiết để dễ dàng kiểm tra lỗi
                Console.WriteLine($"Failed to fetch Google user info. Response Code: {response.StatusCode}, Response: {responseBody}");
                throw new Exception($"Failed to fetch Google user info: {responseBody}");
            }

            // Log phản hồi để kiểm tra kết quả
            Console.WriteLine($"Google user info response: {responseBody}");

            // Deserialize phản hồi JSON
            try
            {
                var userInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
                return userInfo;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializing user info: {jsonEx.Message}");
                throw new Exception("Error deserializing Google user info.", jsonEx);
            }
        }

        private async Task<User> CreateUserFromGoogleInfo(string name, string email)
        {
            Role role = await _roleRepository.findById(3);
            if (role == null)
                throw new Exception("Role ROLE_CUSTOMER does not exist");

            return new User
            {
                Username = name,
                Email = email,
                Password = "oauth2_default_password_google",
                Role = role,
                Status = 1
            };
        }

        private async Task<Dictionary<string, object>> GenerateUserTokenResponse(User user, string name, string email)
        {
            Console.WriteLine("Generating token response...");
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }

            TokenInfo tokenInfo = await GenerateToken(user);
            Console.WriteLine("Token info generated successfully.");

            return new Dictionary<string, object>
            {
                { "token", tokenInfo.Token },
                { "expiryTime", tokenInfo.ExpiryDate },
                { "email", email },
                { "name", name },
            };
        }

        public async Task<Dictionary<string, object>> DecodeTokenAsync(string token)
        {
            var tokenDetails = new Dictionary<string, object>();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    throw new Exception("Invalid token.");
                }

                var claims = jsonToken.Claims;

                tokenDetails.Add("user_id", claims.FirstOrDefault(c => c.Type == "user_id")?.Value);
                tokenDetails.Add("role_name", claims.FirstOrDefault(c => c.Type == "scope")?.Value); // Assuming scope holds the role name
                tokenDetails.Add("expiration_time", jsonToken.ValidTo);
                tokenDetails.Add("issued_time", jsonToken.ValidFrom);

                return tokenDetails;
            }
            catch (Exception e)
            {
                throw new Exception("Error decoding token: " + e.Message);
            }
        }


        public async Task<Dictionary<string, object>> HandleFacebookLogin(string code)
        {
            try
            {
                Console.WriteLine("code service: " + code);

                // Lấy accessToken từ Facebook
                var accessToken = await GetAccessTokenFromFacebook(code);
                Console.WriteLine($"access token: {accessToken}");  // In giá trị accessToken để kiểm tra

                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Access token is empty or invalid.");
                }

                // Lấy thông tin người dùng từ Facebook
                var userInfo = await GetUserInfoFromFacebook(accessToken);
                Console.WriteLine("userInfo: " + JsonSerializer.Serialize(userInfo));  // Log thông tin người dùng

                // Lấy email từ thông tin người dùng
                if (userInfo.TryGetValue("email", out var emailObj) && emailObj != null)
                {
                    var email = emailObj.ToString()?.Trim();

                    if (string.IsNullOrEmpty(email))
                    {
                        throw new Exception("Email is null or empty after trimming.");
                    }

                    Console.WriteLine("email: " + email);  // Log giá trị email

                    // Lấy name từ thông tin người dùng
                    if (userInfo.TryGetValue("name", out var nameObj) && nameObj != null)
                    {
                        var name = nameObj.ToString()?.Trim();

                        if (string.IsNullOrEmpty(name))
                        {
                            throw new Exception("Name is null or empty after trimming.");
                        }

                        Console.WriteLine("name: " + name);  // Log giá trị name

                        // Kiểm tra người dùng đã tồn tại hay chưa
                        var existingUser = await _userRepository.findByUsername(email);
                        Console.WriteLine("Existing user: " + existingUser);

                        if (existingUser != null)
                        {
                            return await GenerateUserTokenResponse(existingUser, name, email);
                        }

                        // Tạo người dùng mới từ thông tin Facebook
                        User newUser = await CreateUserFromFacebookInfo(name, email);
                        await _userRepository.saveUser(newUser);

                        Console.WriteLine("User new: " + newUser);
                        Console.WriteLine("name: " + name);
                        Console.WriteLine("email: " + email);

                        return await GenerateUserTokenResponse(newUser, name, email);
                    }
                    else
                    {
                        throw new Exception("Name is missing or invalid in the user info.");
                    }
                }
                else
                {
                    throw new Exception("Email is missing or invalid in the user info.");
                }
            }
            catch (Exception ex)
            {
                // Thêm log để kiểm tra chi tiết lỗi
                Console.WriteLine($"Error during Facebook login: {ex.Message}");
                throw new Exception($"Error during Facebook login: {ex.Message}", ex);
            }
        }





        Task<string> IAuthenticationService.GetAccessTokenFromGoogle(string code)
        {
            throw new NotImplementedException();
        }

        Task<Dictionary<string, object>> IAuthenticationService.GetUserInfoFromGoogle(string accessToken)
        {
            throw new NotImplementedException();
        }






        public async Task<Dictionary<string, object>> GetUserInfoFromFacebook(string accessToken)
        {
            using var client = new HttpClient();

            // Sử dụng header Authorization thay vì truyền token trong URL
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // URL để lấy thông tin người dùng từ Facebook
            var userInfoUrl = "https://graph.facebook.com/v12.0/me?fields=id,name,email";

            // Gửi yêu cầu GET đến Facebook Graph API
            var response = await client.GetAsync(userInfoUrl);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Kiểm tra mã trạng thái của phản hồi
            if (!response.IsSuccessStatusCode)
            {
                // Thêm log chi tiết để dễ dàng kiểm tra lỗi
                Console.WriteLine($"Failed to fetch Facebook user info. Response Code: {response.StatusCode}, Response: {responseBody}");
                throw new Exception($"Failed to fetch Facebook user info: {responseBody}");
            }

            // Log phản hồi để kiểm tra kết quả
            Console.WriteLine($"Facebook user info response: {responseBody}");

            // Deserialize phản hồi JSON
            try
            {
                var userInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
                return userInfo;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializing user info: {jsonEx.Message}");
                throw new Exception("Error deserializing Facebook user info.", jsonEx);
            }
        }

        Task<User> IAuthenticationService.CreateUserFromGoogleInfo(string name, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateUserFromFacebookInfo(string name, string email)
        {
            Role role = await _roleRepository.findById(3);
            if (role == null)
                throw new Exception("Role ROLE_CUSTOMER does not exist");

            return new User
            {
                Username = name,
                Email = email,
                Password = "oauth2_default_password_facebook",
                Role = role,
                Status = 1
            };
        }


        public void CreateCustomer(User user)
        {
            throw new NotImplementedException();
        }

        Task<Dictionary<string, object>> IAuthenticationService.GenerateUserTokenResponse(User user, string picture, string name, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetAccessTokenFromFacebook(string code)
        {
            Console.WriteLine("get access token code :" + code);
            Console.WriteLine("client_id :" + _facebookClientId);
            Console.WriteLine("secret_id :" + _facebookClientSecret);
            Console.WriteLine("uri  :" + _redirectUriFacebook);

            using var client = new HttpClient();

            // URL yêu cầu lấy access token từ Facebook
            var tokenUrl = "https://graph.facebook.com/v12.0/oauth/access_token";

            // Tạo một dictionary chứa các tham số yêu cầu
            var requestData = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _facebookClientId },
                { "client_secret", _facebookClientSecret },
                { "redirect_uri", _redirectUriFacebook }
            };

            // Gửi yêu cầu POST tới Facebook
            var response = await client.PostAsync(tokenUrl, new FormUrlEncodedContent(requestData));

            // Đọc phản hồi từ Facebook
            var responseBody = await response.Content.ReadAsStringAsync();

            // Log phản hồi để dễ dàng debug
            Console.WriteLine($"Facebook token response: {responseBody}");

            // Kiểm tra mã trạng thái HTTP của phản hồi
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch Facebook access token: {responseBody}");
            }

            try
            {
                // Deserialize phản hồi JSON thành một đối tượng AccessTokenResponse
                var tokenResponse = JsonSerializer.Deserialize<AccessTokenResponse>(responseBody);

                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    // Log access token để dễ dàng kiểm tra
                    Console.WriteLine("Facebook Access Token: " + tokenResponse.AccessToken);
                    return tokenResponse.AccessToken;
                }
                else
                {
                    throw new Exception("Access token not found in the response");
                }
            }
            catch (JsonException jsonEx)
            {
                // Log lỗi giải mã JSON
                throw new Exception("Error deserializing Facebook OAuth response: " + jsonEx.Message);
            }

        }

        public async Task<string> VerifyToken(string token, bool isRefresh)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                // Cấu hình các tham số xác minh
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,   // Bạn có thể tùy chỉnh theo nhu cầu
                    ValidateAudience = false, // Tùy chỉnh theo nhu cầu
                    ValidateLifetime = true,  // Kiểm tra thời gian hết hạn
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signerKey)) // Chìa khóa bí mật
                };

                SecurityToken validatedToken;

                // Xác minh token với các tham số đã cấu hình
                var principal = handler.ValidateToken(token, validationParameters, out validatedToken);

                // Kiểm tra các claim (thông tin của token)
                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null)
                {
                    throw new AppException(ErrorCode.EXPIRED_TOKEN);
                }

                // Log thông tin token để debug
                Console.WriteLine("Decoded token: " + string.Join(", ", jwtToken.Claims.Select(kv => $"{kv.Type}: {kv.Value}")));

                // Kiểm tra thời gian hết hạn (expiration time)
                var expiryTime = jwtToken.ValidTo;
                if (expiryTime <= DateTime.UtcNow)
                {
                    throw new AppException(ErrorCode.EXPIRED_TOKEN);  // Nếu token đã hết hạn
                }

                // Trả về token hợp lệ nếu tất cả các điều kiện đều đúng
                return token;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new AppException(ErrorCode.EXPIRED_TOKEN);  // Nếu token hết hạn
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                throw new AppException(ErrorCode.INVALID_TOKEN_SIGNATURE);  // Nếu chữ ký không hợp lệ
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
                throw new AppException(ErrorCode.UNAUTHENTICATED);  // Lỗi không xác định
            }
        }

        public async Task<TokenInfo> GenerateToken(User user)
        {
            // Kiểm tra giá trị đầu vào
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ArgumentNullException(nameof(user.Username), "Username cannot be null or empty.");
            }

            if (user.UserId == 0)
            {
                throw new ArgumentException("UserId cannot be zero or null.", nameof(user.UserId));
            }

            if (string.IsNullOrEmpty(_signerKey))
            {
                throw new ArgumentNullException(nameof(_signerKey), "Signer key cannot be null or empty.");
            }

            // Tạo JWT
            var issueTime = DateTime.UtcNow;
            var expiryTime = issueTime.AddHours(1); // Token expires in 1 hour

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Iss, "hoangtuan.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_id", user.UserId.ToString()),
                new Claim("scope", user.Role.RoleName.Trim()),
                new Claim("username", user.Username),
                new Claim("email", user.Email)
            };

            Console.WriteLine("Claims created.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signerKey));
            Console.WriteLine($"Key generated: {_signerKey}");

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            Console.WriteLine($"Signing credentials created.");

            var token = new JwtSecurityToken(
                issuer: "hoangtuan.com",
                audience: "hoangtuan.com",
                claims: claims,
                expires: expiryTime,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Token generated: {tokenString}");

            return new TokenInfo(tokenString, expiryTime);
        }
    }


}
