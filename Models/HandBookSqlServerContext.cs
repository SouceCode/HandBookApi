using Microsoft.EntityFrameworkCore;

namespace HandBookApi.Models
{
    public class HandBookSqlServerContext : DbContext
    {
        public HandBookSqlServerContext(DbContextOptions<HandBookSqlServerContext> options)
            : base(options)
        {
        }

        public DbSet<Base_Book> Base_Books { get; set; }
        public DbSet<Game_Setting> Game_Settings { get; set; }
    }
}