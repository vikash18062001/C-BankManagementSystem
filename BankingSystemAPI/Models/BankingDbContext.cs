using System;
using Microsoft.EntityFrameworkCore;

namespace BankingSystemAPI.Models
{
	public class BankingDbContext : DbContext
	{
        public BankingDbContext(DbContextOptions options):base(options)
		{
		}
		public BankingDbContext() { }

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<AccountHolder> AccountHolders { get; set; }

		public DbSet<BankModel> Banks { get; set; }
	}
}

