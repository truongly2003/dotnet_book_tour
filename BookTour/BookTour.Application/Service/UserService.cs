using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Exception;
using BookTour.Domain.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using BookTour.Application.Dto.Request;
using Microsoft.EntityFrameworkCore;
namespace BookTour.Application.Service

{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;


        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository, IEmployeeRepository employeeRepository, IEmployeeService employeeService, IAuthenticationService authenticationService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _authenticationService = authenticationService;
            _emailService = emailService;
        }

        public async Task<Page<UserDTO>> GetAllUserAsync(int page, int size)
        {
            var data = await _userRepository.FindAllByStatusAsync(1);

            var userDTO = data.Select(user => new UserDTO
            {
                id = user.UserId,
                username = user.Username,
                password = user.Password,
                email = user.Email,
                roleId = user.RoleId,
                roleName = user.Role != null ? user.Role.RoleName : null,
                status = user.Status
            })
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);
            var result = new Page<UserDTO>
            {
                Data = userDTO,
                TotalElement = totalElement,
                TotalPages = totalPage
            };

            return result;
        }


        TokenInfo IUserService.GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddUser(UserCreateRequest request)
        {
            // Log the username (for debugging purposes)
            Console.WriteLine($"Username: {request.Username}");

            // Check if the email already exists in the database (Fixing email check)
            bool emailExists = await _userRepository.ExistsByEmailAsync(request.Email);  // Use Email, not Username
            if (emailExists)
            {
                throw new AppException(ErrorCode.USER_EXISTS);
            }

            // Validate the password length
            if (request.Password.Length < 6)
            {
                throw new AppException(ErrorCode.PASSWORD_TOO_SHORT);
            }

            // Create new User entity
            User user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Status = 0, // Default to status 0 (not verified)
                Email = request.Email  // Set email based on the request
            };

            // Assign role and save user
            Role role;
            if (request.Username.StartsWith("NV_"))
            {
                role = await _roleRepository.findById(2)
                    ?? throw new Exception("Default role not found for employee");

                user.Role = role;
                user = await _userRepository.saveUser(user); // Save the user to generate the UserId

                // Create and save employee data
                string employeeId = await _employeeService.generateEmployeeIdBy();
                if (string.IsNullOrEmpty(employeeId))
                {
                    throw new Exception("Failed to generate employee ID");
                }

                Employee employee = new Employee
                {
                    EmployeeId = employeeId,
                    EmployeeName = user.Username,
                    EmployeeEmail = $"{user.Username}@gmail.com",
                    User = user // Link employee to user
                };
                await _employeeRepository.saveEmployee(employee);

                Console.WriteLine("Employee created: ", user.Username);
            }
            else
            {
                role = await _roleRepository.findById(3)
                    ?? throw new Exception("Default role not found for customer");

                user.Role = role;
                user = await _userRepository.saveUser(user); // Save the user to generate the UserId

                // Create and save customer data
                Customer customer = new Customer
                {
                    CustomerAddress = "Default", // Default address
                    CustomerEmail = $"{user.Username}@gmail.com", // Default email format
                    CustomerName = user.Username, // Use username as customer name
                    CustomerPhone = "0827415586", // Default phone number
                    User = user // Link customer to user
                };
                await _customerRepository.saveCustomer(customer);

                Console.WriteLine("Customer created: ", user.Username);
            }

            // Generate and assign verification token
            var tokenInfo = await _authenticationService.GenerateToken(user);
            if (tokenInfo == null || string.IsNullOrEmpty(tokenInfo.Token))
            {
                throw new Exception("Failed to generate token");
            }

            user.VerifyToken = tokenInfo.Token;  // Assign the token to the user
            Console.WriteLine($"Token: {tokenInfo.Token}, Expiry: {tokenInfo.ExpiryDate}");

            // Save the user with token
            await _userRepository.updateUser(user);

            // Send verification email
            await SendVerificationEmailAsync(user);

            return user;
        }


        public Task<List<User>> getListUser()
        {
            throw new NotImplementedException();
        }



        async Task<UserDTO> IUserService.UpdateUserAsync(UserUpdateRequest request, int id)
        {
            var user = await _userRepository.FindUserById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Username = request.Username;
            user.Email = request.Email;

            var role = await _roleRepository.findById(request.roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            user.Role = role;
            user.Status = 1;

            try
            {
                await _userRepository.updateUser(user);
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết từ inner exception
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while saving the entity changes.", ex);
            }


            var response = new UserDTO
            {
                id = user.UserId,
                username = user.Username,
                email = user.Email,
                roleId = user.Role.RoleId,
                roleName = user.Role.RoleName
            };

            return response;
        }

        public async Task blockUser(int userId, int status)
        {
            var user = await _userRepository.FindUserById(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update the user's status (1 -> 0)
            user.Status = status;

            // Save changes to the database
            await _userRepository.updateUser(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email) => await _userRepository.ExistsByEmailAsync(email);

        public async Task SendVerificationEmailAsync(User user)
        {
            // Giả sử có phương thức GenerateToken trong AuthenticationService
            var tokenInfo = _authenticationService.GenerateToken(user);

            // Tạo thông điệp email
            string subject = "Verify Your Account";
            string verificationUrl = $"http://localhost:3000/verify?userId={user.UserId}";
            string message = $"<h1>Verify Your Account</h1>" +
                             $"<p>Dear {user.Username},</p>" +
                             $"<p>Please verify your email by clicking the link below:</p>" +
                             $"<a href=\"{verificationUrl}\">Verify Email</a>" +
                             $"<p>This link will expire in 1 hour.</p>" +
                             "<p>Thank you!</p>";

            // Gọi phương thức bất đồng bộ gửi email và đợi kết quả
            await _emailService.sendHtmlMessage(user.Email, subject, message);
        }


        public async Task VerifyEmail(string token)
        {
            try
            {
                var signedJWT = _authenticationService.VerifyToken(token, false); // Giả sử có phương thức VerifyToken trong AuthenticationService

                // Dùng JwtSecurityToken để truy xuất thông tin claim
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadJwtToken(token);

                // Kiểm tra xem claim "user_id" có tồn tại không
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id");
                if (userIdClaim == null)
                {
                    throw new AppException(ErrorCode.INVALID_TOKEN_SIGNATURE);  // Giả sử bạn đã định nghĩa AppException và ErrorCode
                }

                var userId = Convert.ToInt32(userIdClaim.Value);

                User user = await _userRepository.FindUserById(userId);
                if (user == null)
                {
                    throw new AppException(ErrorCode.USER_NOT_EXISTED);
                }

                if (user.Status == 1)
                {
                    throw new AppException(ErrorCode.USER_EXISTS); // Đổi thông báo lỗi nếu người dùng đã xác thực
                }

                user.Status = 1;  // Đặt trạng thái thành "verified"
                user.VerifyToken = null;  // Xoá token xác thực
                await _userRepository.updateUser(user);  // Giả sử bạn đã có phương thức Update trong UserRepository
            }
            catch (AppException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AppException(ErrorCode.INVALID_TOKEN_SIGNATURE);
            }
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            // Tìm người dùng theo id
            var user = await _userRepository.FindUserById(id);

            if (user == null)
            {
                // Nếu không tìm thấy, có thể ném ngoại lệ hoặc trả về null
                throw new Exception("User not found");
            }

            // Chuyển đổi từ User entity sang UserDTO
            var userDto = new UserDTO
            {
                id = user.UserId,
                username = user.Username,
                email = user.Email,
                roleId = user.Role.RoleId,
                roleName = user.Role.RoleName,
                token = user.VerifyToken
            };


            return userDto;
        }
    }
}
