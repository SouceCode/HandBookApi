using Microsoft.EntityFrameworkCore;

namespace HandBookApi.Models
{
    public class HandBookContext : DbContext
    {
        public HandBookContext(DbContextOptions<HandBookContext> options)
            : base(options)
        {
        }

        public DbSet<Base_Book> Base_Books { get; set; }
        public DbSet<Game_Setting> Game_Settings { get; set; }
    }
}