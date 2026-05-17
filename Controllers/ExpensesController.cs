using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ExpensesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // If database has no expenses, create sample data
        if (!_context.Expenses.Any())
        {
            _context.Expenses.AddRange(

                new Expense
                {
                    Title = "Rent",
                    Amount = 2300,
                    Category = "Housing",
                    Date = DateTime.Now,
                    Notes = "Monthly rent"
                },

                new Expense
                {
                    Title = "Car Payment",
                    Amount = 825,
                    Category = "Transportation",
                    Date = DateTime.Now,
                    Notes = "Truck payment"
                },

                new Expense
                {
                    Title = "Groceries",
                    Amount = 200,
                    Category = "Food",
                    Date = DateTime.Now,
                    Notes = "Walmart trip"
                }
            );

            await _context.SaveChangesAsync();
        }

        List<Expense> expenses = await _context.Expenses.ToListAsync();

        return View(expenses);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Expense expense)
    {
        _context.Expenses.Add(expense);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        Expense expense = await _context.Expenses.FindAsync(id);

        return View(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Expense expense)
    {
        _context.Update(expense);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Expense expense = await _context.Expenses.FindAsync(id);

        return View(expense);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Expense expense = await _context.Expenses.FindAsync(id);

        _context.Expenses.Remove(expense);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
