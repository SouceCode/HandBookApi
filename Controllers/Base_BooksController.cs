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
    public class Base_BooksController : ControllerBase
    {
        private readonly HandBookContext _context;

        public Base_BooksController(HandBookContext context)
        {
            _context = context;
        }

        // GET: api/Base_Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Base_Book>>> GetBase_Books()
        {
            return await _context.Base_Books.ToListAsync();
        }

        // GET: api/Base_Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Base_Book>> GetBase_Book(long id)
        {
            var base_Book = await _context.Base_Books.FindAsync(id);

            if (base_Book == null)
            {
                return NotFound();
            }

            return base_Book;
        }




        #region 自定义 Action
           // GET: api/Base_Books/GetBase_BooksPageData/
        [HttpGet]
         [Route("GetBase_BooksPageData")]
        public IEnumerable<Base_Book> GetBase_BooksPageData(int? pageindex,string sqlstr, int? size)
        {
            int pageSize = size??10;//页面记录数
            int pageNumber =  pageindex ?? 1;//页码
            List<Base_Book> base_Books = _context.Base_Books.FromSqlRaw(sqlstr).AsNoTracking()
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToList<Base_Book>();
            return base_Books;
        }
      // GET: api/Base_Books/GetBase_BooksPageCount/
        [HttpGet]
         [Route("GetBase_BooksPageCount")]
         public int GetBase_BooksPageCount(string sqlstr, int? size)
        {
            int pageSize = size??10;//页面记录数
            int TotolRecord = _context.Base_Books.FromSqlRaw(sqlstr).AsNoTracking().ToList<Base_Book>().Count;
           //总页码
           int TotalPage=TotolRecord/pageSize;
           return  TotalPage ;
        }
  
        #endregion

        // PUT: api/Base_Books/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBase_Book(long id, Base_Book base_Book)
        {
            if (id != base_Book.Id)
            {
                return BadRequest();
            }

            _context.Entry(base_Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Base_BookExists(id))
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

        // POST: api/Base_Books
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Base_Book>> PostBase_Book(Base_Book base_Book)
        {
            _context.Base_Books.Add(base_Book);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetBase_Book", new { id = base_Book.Id }, base_Book);
            return CreatedAtAction( nameof(GetBase_Book), new { id = base_Book.Id }, base_Book);
        }

        // DELETE: api/Base_Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Base_Book>> DeleteBase_Book(long id)
        {
            var base_Book = await _context.Base_Books.FindAsync(id);
            if (base_Book == null)
            {
                return NotFound();
            }

            _context.Base_Books.Remove(base_Book);
            await _context.SaveChangesAsync();

            return base_Book;
        }

        private bool Base_BookExists(long id)
        {
            return _context.Base_Books.Any(e => e.Id == id);
        }
    }
}
