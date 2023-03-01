//using System;
//using BankingSystemAPI.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingSystemAPI.Controllers
//{
//	[ApiController]
//	[Route("api/banking")]
//	public class BankingControllerApi : ControllerBase
//	{
//		private readonly BankingDbContext _context;

//public BankingControllerApi(BankingDbContext context)
//{
//	this._context = context;
//}

//		[HttpPost]
//		[Route("bankCreation")]
//		public async Task<IActionResult> CreateBank([FromBody] Bank bank)
//		{
//			if (bank == null)
//				return BadRequest();

//			_context.Banks.Add(bank);
//			await _context.SaveChangesAsync();

//			return Ok(bank);
//		}

//		[HttpPost]
//		[Route("employeeCreation")]
//		public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
//		{
//			if (employee == null)
//				return BadRequest();

//			_context.Employees.Add(employee);
//			await _context.SaveChangesAsync();

//			return Ok(employee);
//		}

//        [HttpPost]
//        [Route("accountCreation")]
//        public async Task<IActionResult> CreateAccount([FromBody] AccountHolder accountHolder)
//        {
//            if (accountHolder == null)
//                return BadRequest();

//            _context.AccountHolders.Add(accountHolder);
//            await _context.SaveChangesAsync();

//            return Ok(accountHolder);
//        }

//    }
//}

