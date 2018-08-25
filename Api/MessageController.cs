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
    [Route("/api/message")]
    public class MessageController : Controller
    {
        private readonly IMapper mapper;
        private readonly ChatDbContext context;
        public MessageController(IMapper mapper, ChatDbContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }


        [HttpGet]
        public async Task<IEnumerable<MsgModel>> GetMessage(MsgModel messageResource)
        {
            var message = await context.MessageModel.ToListAsync();
            return mapper.Map<IEnumerable<MessageModel>, IEnumerable<MsgModel>>(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MsgModel messageResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = mapper.Map<MsgModel, MessageModel>(messageResource);
            context.MessageModel.Add(message);
            await context.SaveChangesAsync();
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] MsgModel messageResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = await context.MessageModel.FindAsync(id);
            if (message == null)
                return NotFound();
            mapper.Map<MsgModel, MessageModel>(messageResource, message);
            await context.SaveChangesAsync();
            return Ok(message);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await context.MessageModel.FindAsync(id);
            if (message == null)
                return NotFound();
            var messageResource = mapper.Map<MessageModel, MsgModel>(message);
            return Ok(messageResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await context.MessageModel.FindAsync(id);
            if (message == null)
                return NotFound();
            context.Remove(message);
            await context.SaveChangesAsync();
            return Ok(id);
        }
    }
}