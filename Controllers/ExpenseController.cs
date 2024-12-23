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
    public class ExpenseController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly MyAppContext _context;
        public ExpenseController(UserManager<ApplicationUser> userManager, MyAppContext context)
        {
            _usermanager = userManager;
            _context = context;
        }
        [HttpGet("getAllExpenses")]
        public async Task<IActionResult> getAllExpenses()
        {
            var userId = _usermanager.GetUserId(User);
            if(userId == null)
            {
                return NotFound("User not Found");
            }
            var expense = await _context.expenses.Where(e => e.userId == userId).ToListAsync();
            return Ok(expense);
        }
        [HttpPost("addExpense")]
        public async Task<IActionResult> addExpense(Expense expense) {
            var userId = _usermanager.GetUserId(User);
            if (userId == null)
            {
                return NotFound("User not Found");
            }
            var data = new Expense
            {
                title = expense.title,
                amount = expense.amount,
                type = "expense",
                date = expense.date,
                category = expense.category,
                description = expense.description,
                created_at = DateTime.Now,
                userId = userId,
            };
            _context.expenses.Add(data);
            var insertedExpense = await _context.SaveChangesAsync();
            data.Id = insertedExpense;
            return Ok(data);
        }
        [HttpPost("DeleteExpense/{id}")] 
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expense == null)
            {
                return NotFound();
            }
            _context.expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return Ok(expense);
        }
        
    }
}
