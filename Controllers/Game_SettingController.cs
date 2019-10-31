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
    public class Game_SettingController : ControllerBase
    {
        private readonly HandBookContext _context;

        public Game_SettingController(HandBookContext context)
        {
            _context = context;
        }

        // GET: api/Game_Setting
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Setting>>> GetGame_Settings()
        {
            return await _context.Game_Settings.ToListAsync();
        }

        // GET: api/Game_Setting/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Setting>> GetGame_Setting(long id)
        {
            var game_Setting = await _context.Game_Settings.FindAsync(id);

            if (game_Setting == null)
            {
                return NotFound();
            }

            return game_Setting;
        }

        // PUT: api/Game_Setting/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Setting(long id, Game_Setting game_Setting)
        {
            if (id != game_Setting.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_SettingExists(id))
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

        // POST: api/Game_Setting
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Game_Setting>> PostGame_Setting(Game_Setting game_Setting)
        {
            _context.Game_Settings.Add(game_Setting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Setting", new { id = game_Setting.Id }, game_Setting);
        }

        // DELETE: api/Game_Setting/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game_Setting>> DeleteGame_Setting(long id)
        {
            var game_Setting = await _context.Game_Settings.FindAsync(id);
            if (game_Setting == null)
            {
                return NotFound();
            }

            _context.Game_Settings.Remove(game_Setting);
            await _context.SaveChangesAsync();

            return game_Setting;
        }

        private bool Game_SettingExists(long id)
        {
            return _context.Game_Settings.Any(e => e.Id == id);
        }
    }
}
