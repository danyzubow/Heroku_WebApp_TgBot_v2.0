using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp_tg_bot2.Models.Account
{
    public class AccountContext:DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public AccountContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TestTelgram.mssql.somee.com;Database=TestTelgram;user=seadogs4_SQLLogin_1;password=cfcqfp1xdp;");
        }
    }
}
