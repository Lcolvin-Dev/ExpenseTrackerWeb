using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        decimal totalExpenses = 0;
        int totalCategories = 0;
        decimal monthlyBudget = 5000;

        try
        {
            totalExpenses = await _context.Expenses.SumAsync(e => e.Amount);

            totalCategories = await _context.Expenses
                .Select(e => e.Category)
                .Distinct()
                .CountAsync();
        }
        catch
        {
            totalExpenses = 0;
            totalCategories = 0;
        }

        ViewBag.TotalExpenses = totalExpenses;
        ViewBag.TotalCategories = totalCategories;
        ViewBag.MonthlyBudget = monthlyBudget;
        ViewBag.RemainingBudget = monthlyBudget - totalExpenses;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
