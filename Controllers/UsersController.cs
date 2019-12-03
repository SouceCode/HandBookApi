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
    public class UsersController : ControllerBase
    {
        private readonly HandBookSqlServerContext _context;

        public UsersController(HandBookSqlServerContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }
#region 自定义 Action
           // GET: api/User/GetUsersPageData/
        [HttpGet]
         [Route("GetUsersPageData")]
        public IEnumerable<Users> GetUsersPageData(int? pageindex,string sqlstr, int? size)
        {
            int pageSize = size??10;//页面记录数
            int pageNumber =  pageindex ?? 1;//页码
            List<Users> game_Settings = _context.Users.FromSqlRaw(sqlstr).AsNoTracking()
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToList<Users>();
            return game_Settings;
        }
      // GET: api/User/GetUsersPageCount/
        [HttpGet]
         [Route("GetUsersPageCount")]
         public int GetUsersPageCount(string sqlstr, int? size)
        {
            int pageSize = size??10;//页面记录数
            int TotolRecord = _context.Users.FromSqlRaw(sqlstr).AsNoTracking().ToList<Users>().Count;
           //总页码
           int TotalPage=(TotolRecord-1)/pageSize +1;
           return  TotalPage ;
        }
  
        #endregion
        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(long id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
