﻿using BookTour.Application.Dto;
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
namespace BookTour.Application.Service
{
    public class UserService : IUserService
    {
       
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository, IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
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
            Console.WriteLine($"Username: {request.Username}");
            bool a = await _userRepository.existsByUsernameAsync(request.Username);
            Console.WriteLine("exist:", a);

            if (a) 
            {
                throw new AppException(ErrorCode.USER_EXISTS);
            }

            // Kiểm tra độ dài mật khẩu
            if (request.Password.Length < 6)
            {
                throw new AppException(ErrorCode.PASSWORD_TOO_SHORT);
            }

            User user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Status = 1,
                Email = $"{request.Email}@example.com"
            };

            Role role;

            if (request.Username.StartsWith("NV_"))
            {
                role = await _roleRepository.findById(2)
                       ?? throw new Exception("Default role not found");

                user.Role = role;
                user = await _userRepository.saveUser(user); 


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

       
        public Task<List<User>> getListUser()
        {
            throw new NotImplementedException();
        }
    }
}
