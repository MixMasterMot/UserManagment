﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Models;
using UserManagment.Services;

namespace UserManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _userService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetByID(string id)
        {
            var user = await _userService.GetAsync(id);
            if (user != null)
            {
                return user;
            }
            return NotFound();
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<User>>> Search(string search)
        {
            return await _userService.FindAsync(search);
        }

        [HttpPatch("{id:length(24)}")]
        public async Task<ActionResult<User>> Patch(string id, [FromBody]User user)
        {
            var oldUser = await _userService.GetAsync(id);
            if(oldUser is null)
            {
                return NotFound();
            }
            user.Id = oldUser.Id;
            await _userService.UpdateAsync(id, user);
            return Ok(oldUser);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody]User user)
        {
            await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetByID), new { id = user.Id }, user);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var oldUser = _userService.GetAsync(id);
            if(oldUser is null)
            {
                return NotFound();
            }
            await _userService.RemoveAsync(id);
            return Ok();
        }
    }
}