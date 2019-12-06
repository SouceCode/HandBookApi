using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandBookApi.Models;

namespace HandBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Job_SettingsController : ControllerBase
    {
        private readonly HandBookSqlServerContext _context;

        public Job_SettingsController(HandBookSqlServerContext context)
        {
            _context = context;
        }

        // GET: api/Job_Settings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job_Setting>>> GetJob_Settings()
        {
            return await _context.Job_Settings.ToListAsync();
        }

        // GET: api/Job_Settings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job_Setting>> GetJob_Setting(long id)
        {
            var job_Setting = await _context.Job_Settings.FindAsync(id);

            if (job_Setting == null)
            {
                return NotFound();
            }

            return job_Setting;
        }

        // PUT: api/Job_Settings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob_Setting(long id, Job_Setting job_Setting)
        {
            if (id != job_Setting.Id)
            {
                return BadRequest();
            }

            _context.Entry(job_Setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Job_SettingExists(id))
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

        // POST: api/Job_Settings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Job_Setting>> PostJob_Setting(Job_Setting job_Setting)
        {
            _context.Job_Settings.Add(job_Setting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob_Setting", new { id = job_Setting.Id }, job_Setting);
        }

        // DELETE: api/Job_Settings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Job_Setting>> DeleteJob_Setting(long id)
        {
            var job_Setting = await _context.Job_Settings.FindAsync(id);
            if (job_Setting == null)
            {
                return NotFound();
            }

            _context.Job_Settings.Remove(job_Setting);
            await _context.SaveChangesAsync();

            return job_Setting;
        }

        private bool Job_SettingExists(long id)
        {
            return _context.Job_Settings.Any(e => e.Id == id);
        }
    }
}
