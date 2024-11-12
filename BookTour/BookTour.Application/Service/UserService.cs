using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Exception;
using BookTour.Domain.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookTour.Application.Service
{
    public class UserService : IUserService
    {
        private readonly string signerKey = "1TjXchw5FloESb63Kc+DFhTARvpWL4jUGCwfGWxuG5SIf/1y/LgJxHnMqaF6A/ij";
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
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
            UserDTO userDTO = new UserDTO
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

        public Task<User> AddUser()
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


    }
}
