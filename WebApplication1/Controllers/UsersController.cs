using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modals.Entities;
using WebApplication1.Models.Entities;
using WebApplication1.Repository.Implement;
using WebApplication1.Repository.Interface;
using WebApplication1.Services.Implement;
using WebApplication1.Services.Interface;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IUserRepository _userRepository;
        private IUserService _userService;

        public UsersController(ApplicationDbContext context, IUserRepository userRepository, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userRepository.GetUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = _userRepository.GetUserById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult InsertUser(UserModel user)
        {
            var result = _userRepository.InsertUser(user);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult InsertUser(int id, UserModel user)
        {
            var result = _userRepository.UpdateUser(id, user.Name);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveUserById(int id)
        {
            var result = _userRepository.RemoveUserById(id);
            return Ok(result);
        }
        [HttpGet("GetToken/{id}")]
        public IActionResult GetToken(int id)
        {
            var user = _userRepository.GetUserById(id);
            var result = _userService.GenerateToken(user.Username);
            return Ok(result);
        }
    }
}