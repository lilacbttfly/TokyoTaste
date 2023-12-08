using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TokyoTasteCrud.Models;
using App.Context;

namespace TokyoTasteCrud.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Carrinho()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Produtos()
    {
        return View(_context.Produtos.ToList());
    }
}