﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;
using SignalRSample.Models;
using System.Security.Claims;

namespace SignalRSample.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatRooms
        [HttpGet]
        [Route("/[controller]/GetChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoom()
        {
          if (_context.ChatRoom == null)
          {
              return NotFound();
          }
            return await _context.ChatRoom.ToListAsync();
        }

        [HttpGet]
        [Route("/[controller]/GetChatUser")]
        public async Task<ActionResult<object>> GetChatUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Return current Logged in User
            var users = await _context.Users.ToListAsync(); //Get All User

            if (users == null)
            {
                return NotFound();
            }
            else
            {
                return users.Where(x => x.Id != userId).Select(x => new { x.Id, x.UserName }).ToList();
            }
        }

        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("/[controller]/PostChatRoom")]
        public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
          if (_context.ChatRoom == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ChatRoom'  is null.");
          }
            _context.ChatRoom.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

		// DELETE: api/ChatRooms/5
		[HttpDelete("{id}")]
		[Route("/[controller]/DeleteChatRoom/{id}")]
		public async Task<IActionResult> DeleteChatRoom(int id)
		{
			if (_context.ChatRoom == null)
			{
				return NotFound();
			}
			var chatRoom = await _context.ChatRoom.FindAsync(id);
			if (chatRoom == null)
			{
				return NotFound();
			}

			_context.ChatRoom.Remove(chatRoom);
			await _context.SaveChangesAsync();

			var room = await _context.ChatRoom.FirstOrDefaultAsync();

			return Ok(new { deleted = id, selected = (room == null ? 0 : room.Id) });
		}
	}
}
