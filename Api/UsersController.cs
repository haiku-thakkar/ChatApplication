using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SlackClone.ChatDB;
using SlackClone.Api.Resources;
using SlackClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace SlackClone.Api
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly ChatDbContext context;
        public UsersController(IMapper mapper, ChatDbContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }


        [HttpGet]
        public async Task<IEnumerable<LoginModel>> GetUsers(LoginModel usersResource)
        {
            var users = await context.Login.ToListAsync();
            return mapper.Map<IEnumerable<Login>, IEnumerable<LoginModel>>(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] LoginModel usersResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = mapper.Map<LoginModel, Login>(usersResource);
            context.Login.Add(users);
            await context.SaveChangesAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(int id, [FromBody] LoginModel usersResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = await context.Login.FindAsync(id);
            if (users == null)
                return NotFound();
            mapper.Map<LoginModel, Login>(usersResource, users);
            await context.SaveChangesAsync();
            return Ok(users);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await context.Login.FindAsync(id);
            if (users == null)
                return NotFound();
            var usersResource = mapper.Map<Login, LoginModel>(users);
            return Ok(usersResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await context.Login.FindAsync(id);
            if (users == null)
                return NotFound();
            context.Remove(users);
            await context.SaveChangesAsync();
            return Ok(id);
        }
    }
}