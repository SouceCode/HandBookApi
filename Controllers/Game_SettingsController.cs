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
    public class Game_SettingsController : ControllerBase
    {
        private readonly HandBookContext _context;

        public Game_SettingsController(HandBookContext context)
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
        public async Task<ActionResult<Game_Setting>> GetGame_Settings(long id)
        {
            var game_Setting = await _context.Game_Settings.FindAsync(id);

            if (game_Setting == null)
            {
                return NotFound();
            }

            return game_Setting;
        }
 #region 自定义 Action
        
        //GET: api/Game_Setting/GetGame_SettingsPageData/
        [HttpGet]
         [Route("GetGame_SettingsPageData")]
        public IEnumerable<Game_Setting> GetGame_SettingsPageData(int? pageindex,string table,string where ,string orderby, int? size)
        {

            if (_context.Database.IsSqlServer())
            {
                /***
                          *由于EF6.0不支持SQLSERVER 2008 R2
                          *现分页采用原生的ADO.NET方式来实现
                          *等效于
                          *
                          ***/
                int pageSize = size ?? 10;//页面记录数
                int pageNumber = pageindex ?? 1;//页码

                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by id asc";
                }
                string sqlstr = @"select * from (select row_number() over ( " + orderby + " ) as rownum,* from " + table + @"  With(NOLOCK)   " + where + ")A where rownum >" + pageSize * (pageNumber) + " and rownum <= " + pageSize * (pageNumber + 1);
                List<Game_Setting> game_Settings = _context.Game_Settings.FromSqlRaw(sqlstr).AsNoTracking()

                .ToList<Game_Setting>();
                return game_Settings;

            }
            else if(_context.Database.IsSqlite())
            {

                int pageSize = size ?? 10;//页面记录数
                int pageNumber = pageindex ?? 1;//页码
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by id asc";
                }

                string sqlstr = "select * from " + table +" "+ where + orderby;
                List<Game_Setting> game_Settings = _context.Game_Settings.FromSqlRaw(sqlstr).AsNoTracking()
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToList<Game_Setting>();
                return game_Settings;

            }else{

                  return new  List<Game_Setting>();
            }
        }
      // GET: api/Game_Setting/GetGame_SettingsPageCount/
        [HttpGet]
         [Route("GetGame_SettingsPageCount")]
         public int GetGame_SettingsPageCount(string sqlstr, int? size)
        {
            int pageSize = size??10;//页面记录数
            int TotolRecord = _context.Game_Settings.FromSqlRaw(sqlstr).AsNoTracking().ToList<Game_Setting>().Count;
           //总页码
           int TotalPage=(TotolRecord-1)/pageSize +1;
           return  TotalPage ;
        }
  
        #endregion
        // PUT: api/Game_Setting/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Settings(long id, Game_Setting game_Setting)
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
        public async Task<ActionResult<Game_Setting>> PostGame_Settings(Game_Setting game_Setting)
        {
            _context.Game_Settings.Add(game_Setting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Settings", new { id = game_Setting.Id }, game_Setting);
        }

        // DELETE: api/Game_Setting/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game_Setting>> DeleteGame_Settings(long id)
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
