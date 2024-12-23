using ExpenceTrackerWebApiAsp.netCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenceTrackerWebApiAsp.netCore.Data
{
    public class MyAppContext : IdentityDbContext<ApplicationUser>
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        public DbSet<Income> incomes { get; set; }
        public DbSet<Expense> expenses { get; set; }
    }
}
