using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Services;

namespace WorkflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IPTaskService _pTaskService;

        public PTasksController(ApplicationDbContext context, IPTaskService pTaskService)
        {
            this._dbcontext = context;
            this._pTaskService = pTaskService;
        }

        // GET: api/PTasks
        [HttpGet("GetAllByTeamId/{id}")]
        public async Task<ActionResult<IEnumerable<PTaskDto>>> GetAll(int id)
        {
            var claimsList = HttpContext.User.Claims.ToList();
            var pTaskDtoList=_pTaskService.GetAllPtaskByTeamId(id, claimsList);

            //if (_dbcontext.PTasks == null) { return NotFound(); }
            //HttpContext.Request.Headers.ad
            //return await _dbcontext.PTasks.ToListAsync();

            return pTaskDtoList;
        }

        // GET: api/PTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PTaskDto>> GetPTask(int id)
        {
          if (_dbcontext.PTasks == null)
          {
              return NotFound();
          }
            var pTask = await _dbcontext.PTasks.FindAsync(id);

            if (pTask == null)
            {
                return NotFound();
            }

            //return pTask;
            return Ok();
        }

        // PUT: api/PTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePTask")]
        public async Task<IActionResult> updatePTask(PTaskUpdateDto pTaskDto)
        {//dodać sprawdzanie czy użytkownik należy do danego teamu

            //if (id != pTaskDto.Id)
            //{
            //    return BadRequest();
            //}
            //_dbcontext.Entry(pTaskDto).State = EntityState.Modified;

            //try
            //{
            //    await _dbcontext.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PTaskExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            _pTaskService.UpdatePTask(pTaskDto);
            return NoContent();
        }

        // POST: api/PTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreatePTask")]
        public async Task<ActionResult<PTaskDto>> CreatePTask(PTaskDto pTaskDto)
        {//dodać sprawdzanie czy użytkownik należy do danego teamu

            //var userClaims = HttpContext.User.Claims.ToList();

            var ptaskdto=_pTaskService.CreatePTask(pTaskDto.TeamId);

            //if (_dbcontext.PTasks == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.PTasks'  is null.");
            //}
            //_dbcontext.PTasks.Add(pTaskDto);
            //await _dbcontext.SaveChangesAsync();
            //return CreatedAtAction("GetPTask", new { id = pTaskDto.Id }, pTaskDto);

            return ptaskdto;
        }

        // DELETE: api/PTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePTask(int id)
        {
            if (_dbcontext.PTasks == null)
            {
                return NotFound();
            }
            var pTask = await _dbcontext.PTasks.FindAsync(id);
            if (pTask == null)
            {
                return NotFound();
            }

            _dbcontext.PTasks.Remove(pTask);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool PTaskExists(int id)
        {
            return (_dbcontext.PTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
