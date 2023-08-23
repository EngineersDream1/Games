using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Projects\\Games\\Games\\TicTacToe_Server\\TicTacToeDB.db");
        }

    }
}
