using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Models;

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvitationsController(ApplicationDbContext context)
        {
            _context = context;
        }




        // GET: api/Invitations/5
        [HttpGet("FindUser/{email}")]
        public async Task<ActionResult<List<FoundUserDto>>> FindUser(string email)
        {
            //if (_context.Invitations == null)
            //{
            //    return NotFound();
            //}
            var users = _context.Users.Where(u => u.Email.StartsWith(email)).ToList();

            List<FoundUserDto> foundUsers = new List<FoundUserDto>();
            foreach (var user in users)
            {
                foundUsers.Add(new FoundUserDto() { Email = user.Email,Id=user.Id });
            };
                
                //.ToList();

            //var userInvited = await _context.Invitations.FindAsync(id);

            //if (foundUsers == null)
            //{
            //    return NotFound();
            //}

            return foundUsers;
        }

        // GET: api/Invitations
        [HttpGet("GetAllInvitedUsers")]
        public async Task<ActionResult<List<FoundUserDto>>> GetAllInvitedUsers()
        {
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(u => u.Type== ClaimTypes.NameIdentifier).Value);
            
            var usersList = _context.Invitations
                .Include(i => i.InvitedUser)
                .Where(i => i.SourceUserId == userId);

            List<FoundUserDto> foundUsers = new List<FoundUserDto>();
            foreach (var user in usersList)
            {
                foundUsers.Add(new FoundUserDto() { Email = user.InvitedUser.Email, Id=user.InvitedUserId });
            };

            return foundUsers;
        }

        // GET: api/Invitations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserInvited>> GetUserInvited(int id)
        //{
        //  if (_context.Invitations == null)
        //  {
        //      return NotFound();
        //  }
        //    var userInvited = await _context.Invitations.FindAsync(id);

        //    if (userInvited == null)
        //    {
        //        return NotFound();
        //    }

        //    return userInvited;
        //}

        // PUT: api/Invitations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUserInvited(int id, UserInvited userInvited)
        //{
        //    if (id != userInvited.SourceUserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(userInvited).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserInvitedExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Invitations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<UserInvited>> PostUserInvited(UserInvited userInvited)
        //{
        //  if (_context.Invitations == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Invitations'  is null.");
        //  }
        //    _context.Invitations.Add(userInvited);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserInvitedExists(userInvited.SourceUserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUserInvited", new { id = userInvited.SourceUserId }, userInvited);
        //}

        // DELETE: api/Invitations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUserInvited(int id)
        //{
        //    if (_context.Invitations == null)
        //    {
        //        return NotFound();
        //    }
        //    var userInvited = await _context.Invitations.FindAsync(id);
        //    if (userInvited == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Invitations.Remove(userInvited);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserInvitedExists(int id)
        {
            return (_context.Invitations?.Any(e => e.SourceUserId == id)).GetValueOrDefault();
        }
    }
}
