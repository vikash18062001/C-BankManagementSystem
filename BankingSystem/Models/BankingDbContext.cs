using System;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models
{
	public class BankingDbContext : DbContext
	{
		// why this works have to find
		//public BankingDbContext() { }
		//public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Constants.ConnectionString);
		}

		public DbSet<Bank> Banks { get; set; }

		public DbSet<AccountHolder> AccountHolders { get; set; }

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Transaction> Transactions { get; set; }
	}
}

