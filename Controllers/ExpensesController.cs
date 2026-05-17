using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ExpensesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        try
        {
            List<Expense> expenses = _context.Expenses
                .OrderByDescending(e => e.Date)
                .ToList();

            return View(expenses);
        }
        catch (Exception ex)
        {
            return Content("Database Error: " + ex.Message);
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Expense expense)
    {
        try
        {
            _context.Expenses.Add(expense);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Expenses");
        }
        catch (Exception ex)
        {
            return Content("Save Error: " + ex.Message);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Expense expense)
    {
        _context.Expenses.Update(expense);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Expenses");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense != null)
        {
            _context.Expenses.Remove(expense);

            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Expenses");
    }
}