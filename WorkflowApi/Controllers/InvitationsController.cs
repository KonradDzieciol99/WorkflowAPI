using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public InvitationsController(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        // GET: api/Invitations/5
        [HttpGet("FindUser/{email}")]
        public async Task<ActionResult<List<FoundUserDto>>> FindUser(string email)
        {
            if(email==null)
            {
                return BadRequest();
            }
            var users = _userManager.Users.Where(u => u.Email.StartsWith(email)).ToList();

            List<FoundUserDto> foundUsers = new List<FoundUserDto>();
            foreach (var user in users)
            {
                foundUsers.Add(new FoundUserDto() { Email = user.Email,Id=user.Id });
            };

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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InviteUser")]
        public async Task<ActionResult> PostInviteUser([FromBody] int userInvitedId)
        {
            var sourseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            UserInvited userInvited = new UserInvited()
            {
                SourceUserId = sourseUserId,
                InvitedUserId = userInvitedId
            };

            _context.Invitations.Add(userInvited);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserInvitedExists(int id)
        {
            return (_context.Invitations?.Any(e => e.SourceUserId == id)).GetValueOrDefault();
        }
    }
}
