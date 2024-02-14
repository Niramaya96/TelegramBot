using Microsoft.EntityFrameworkCore;
using TelegramBot.Models;

namespace TelegramBot.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        public AppDbContext()
        {
            
        }

    }
}
