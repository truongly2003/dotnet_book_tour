using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Exception;
using BookTour.Domain.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
namespace BookTour.Application.Service
{
    public class UserService : IUserService
    {
        private readonly string signerKey = "1TjXchw5FloESb63Kc+DFhTARvpWL4jUGCwfGWxuG5SIf/1y/LgJxHnMqaF6A/ij";
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository, IEmployeeRepository  employeeRepository, IEmployeeService employeeService)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }




        async Task<UserDTO> IUserService.Login(LoginRequestDTO request)
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

            // Generate token
            var tokenInfo = GenerateToken(user);

            // Create UserDTO with the correct token information
            var userDTO = new UserDTO
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


        TokenInfo GenerateToken(User user)
        {
            var issueTime = DateTime.UtcNow;
            var expiryTime = issueTime.AddHours(1);  // Token expires in 1 hour

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Iss, "hoangtuan.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_id", user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signerKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: "hoangtuan.com",
                audience: "hoangtuan.com",
                claims: claims,
                expires: expiryTime,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenInfo(tokenString, expiryTime);
        }


        public Task<Page<User>> GetAllUserAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

    

        public Task<List<User>> getListUser()
        {
            return _userRepository.findAllUser();
        }

        TokenInfo IUserService.GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddUser(UserCreateRequest request)
        {
            Console.WriteLine($"Username: {request.Username}");
            bool a = await _userRepository.existsByUsernameAsync(request.Username);
            Console.WriteLine("exist:", a);

            // Kiểm tra sự tồn tại của người dùng
            if (a) // Thay vì gọi lại _userRepository.existsByUsernameAsync(request.Username)
            {
                throw new AppException(ErrorCode.USER_EXISTS);
            }

            // Kiểm tra độ dài mật khẩu
            if (request.Password.Length < 6)
            {
                throw new AppException(ErrorCode.PASSWORD_TOO_SHORT);
            }

            // Tạo đối tượng User từ request
            User user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Status = 1,
                Email = $"{request.Username}@example.com"
            };

            Role role;

            if (request.Username.StartsWith("NV_"))
            {
                role = await _roleRepository.findById(2)
                       ?? throw new Exception("Default role not found");

                user.Role = role;
                user = await _userRepository.saveUser(user); // Save user và nhận đối tượng đã cập nhật, bao gồm UserId


                Console.WriteLine("username create :", user.Username);

                Employee employee = new Employee
                {
                    EmployeeId = await _employeeService.generateEmployeeIdBy(),
                    EmployeeName = $"{user.Username}",
                    EmployeeEmail = $"{user.Username}@example.com",
                    User = user
                };

                await _employeeRepository.saveEmployee(employee);
            }
            else
            {
                Console.WriteLine("customer");
                role = await _roleRepository.findById(3)
                       ?? throw new Exception("Default role not found");

                user.Role = role;
                user = await _userRepository.saveUser(user);

                Console.WriteLine("userId create customer : ", user.UserId);

                Customer customer = new Customer
                {
                    CustomerAddress = "Default",
                    CustomerEmail = $"{user.Username}@gmail.com",
                    CustomerName = user.Username,
                    CustomerPhone = "0827415586",
                    User = user
                };

                var b = await _customerRepository.saveCustomer(customer);

                Console.WriteLine("customer new : ", b);
            }

            return user;
        }

    }
}
