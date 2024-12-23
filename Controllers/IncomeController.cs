using ExpenceTrackerWebApiAsp.netCore.Data;
using ExpenceTrackerWebApiAsp.netCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenceTrackerWebApiAsp.netCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly MyAppContext _context;
        public IncomeController(UserManager<ApplicationUser> userManager, MyAppContext context)
        {
            _usermanager = userManager;
            _context = context;
        }

        [HttpGet("getAllIncome")]
        public async Task<IActionResult> getAllIncome()
        {
            var userId = _usermanager.GetUserId(User);
            if (userId == null)
            {
                return NotFound("User not found");
            }
            var income = await _context.incomes.Where(i => i.userId == userId).ToListAsync();
            return Ok(income);
        }
        [HttpPost("addIncome")]
        public async Task<IActionResult> addIncome(Income income)
        {
           var userId = _usermanager.GetUserId(User);
           if(userId != null)
            {
                var incomeData = new Income
                {
                    title = income.title,
                    amount = income.amount,
                    type = "income",
                    date = income.date,
                    category = income.category,
                    description = income.description,
                    created_at = DateTime.Now,
                    userId = userId
                };
                _context.incomes.Add(incomeData);
                await _context.SaveChangesAsync();
                return Ok(incomeData);
            }
            return NotFound("User not found");
        }
        [HttpPost("DeleteIncome/{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _context.incomes.FirstOrDefaultAsync(i => i.Id == id);
            if(income == null)
            {
                return NotFound("Income not found");
            }
            _context.incomes.Remove(income);
            await _context.SaveChangesAsync();
            return Ok(income);
        }
    }
}
