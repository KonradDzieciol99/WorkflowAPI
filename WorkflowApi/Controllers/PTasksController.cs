using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using WorkflowApi.Models;
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
            this._pTaskService.
            if (_dbcontext.PTasks == null) { return NotFound(); }

            //HttpContext.Request.Headers.
            //return await _dbcontext.PTasks.ToListAsync();
            return new List<PTaskDto>();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPTask(int id, PTaskDto pTaskDto)
        {
            if (id != pTaskDto.Id)
            {
                return BadRequest();
            }

            _dbcontext.Entry(pTaskDto).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PTaskExists(id))
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

        // POST: api/PTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PTaskDto>> PostPTask(PTaskDto pTaskDto)
        {
          if (_dbcontext.PTasks == null)
          {
              return Problem("Entity set 'ApplicationDbContext.PTasks'  is null.");
          }
            //_dbcontext.PTasks.Add(pTaskDto);
            //await _dbcontext.SaveChangesAsync();

            //return CreatedAtAction("GetPTask", new { id = pTask.Id }, pTask);
            return Ok();
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
