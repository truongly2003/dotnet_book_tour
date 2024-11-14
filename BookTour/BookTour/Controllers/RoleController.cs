using BookTour.Application.Interface;
using BookTour.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        //[HttpGet]
        //public async Task<IActionResult> getAllRole()
        //{
        //    var users = await _userService.getAllRole();
        //    return Ok(users);
        //}
    }
}
