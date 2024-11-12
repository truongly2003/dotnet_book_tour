<<<<<<< HEAD
﻿using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Exception;
using BookTour.Domain.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

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

        public async Task<UserDTO> Login(UserDTO request)
        {
            Console.WriteLine("Username: " + request.username);

            var user = await _userRepository.findByUsername(request.username);
            if (user == null)
                throw new AppException(ErrorCode.USER_NOT_EXISTED);

            bool authenticated = BCrypt.Net.BCrypt.Verify(request.password, user.Password);
            if (!authenticated)
                throw new AppException(ErrorCode.USER_OR_PASSWORD_WRONG);

            int roleId = user.Role.RoleId;
            string roleName = user.Role.RoleName;
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
            var tokenInfo = await GenerateToken(user);

            // Create UserDTO with the correct token information
            var userDTO = new UserDTO
            {
                id = userId,
                roleId = roleId,
                roleName = roleName,
                username = userName,
                token = tokenInfo.Token, // Assign only the token string
                expiryTime = tokenInfo.ExpiryDate // Use the correct expiry date
            };

            return userDTO;
        }

        public async Task<TokenInfo> GenerateToken(User user)
        {
            var issueTime = DateTime.UtcNow;
            var expiryTime = issueTime.AddHours(1);

            // Tạo claims (thông tin bổ sung cho token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Iss, "hoangtuan.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_id", user.UserId.ToString())
            };

            // Tạo đối tượng SecurityKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signerKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: "hoangtuan.com",
                audience: "hoangtuan.com", // Có thể tùy chỉnh audience
                claims: claims,
                expires: expiryTime,
                signingCredentials: creds
            );

            // Serialize token thành string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Trả về token và thời gian hết hạn
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
=======
﻿using BookTour.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class UserService: IUserService
    {
>>>>>>> 0f09373bbdf2c969964e6f7d9fd925b76a62a292
    }
}
