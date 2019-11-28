using Microsoft.EntityFrameworkCore;
using HandBookApi.Models;

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
        public DbSet<HandBookApi.Models.Users> Users { get; set; }
         public DbSet<HandBookApi.Models.Job_Setting> Job_Settings { get; set; }
    }
}