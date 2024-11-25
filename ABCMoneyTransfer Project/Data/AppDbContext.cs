using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ABCMoneyTransfer_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace ABCMoneyTransfer_Project.Data
{
    // ----------------------------------------------------------------------------------------------------------------------------------
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
    // ----------------------------------------------------------------------------------------------------------------------------------
}
