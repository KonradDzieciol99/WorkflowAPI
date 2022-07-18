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
using WorkflowApi.Services;

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamService _teamService;

        public TeamsController(ApplicationDbContext context, ILogger<TeamsController> logger, ITeamService teamService  )
        {
            this._context = context;
            this._logger = logger;
            this._teamService = teamService;
        }

        // GET: api/Teams
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
        {

            List<Claim> claimList = HttpContext.User.Claims.ToList();
            List<TeamDto> teamDboList = this._teamService.GetAll(claimList);

            return teamDboList;
        }

        // GET: api/Teams/5
        [HttpGet("GetOne/{id}")]
        public async Task<ActionResult<TeamDto>> GetOne(int id)
        {

            List<Claim> claimsList=HttpContext.User.Claims.ToList();
            var team = this._teamService.GetOne(id, claimsList);

            //var team = await _context.Teams.FindAsync(id);

            //if (_context.Teams == null)
            //{
            //    return NotFound();
            //}

            //if (team == null)
            //{
            //    return NotFound();

            //}

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpGet("Test")]
        [HttpPost("CreateTeam")]
        public async Task<ActionResult<TeamDto>> CreateTeam(TeamDto teamDbo)
        {

            List<Claim> claimList = HttpContext.User.Claims.ToList();
            var team = this._teamService.CreateTeam(teamDbo, claimList);

            //if (_context.Teams == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
            //}

            ////_context.Teams.Add(team);
            ////await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTeam", new { id = team.Id }, team);
            return team;
        }

        // DELETE: api/Teams/5
        [HttpDelete("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
