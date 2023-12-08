using Microsoft.AspNetCore.Mvc;
using App.Filters;
using App.Context;
namespace Produtos_Com_Admin.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _BancoDados;
        
        
        public AdminController(AppDbContext BancoDados)
        {
            _BancoDados = BancoDados;
        }
        
        [Area("Admin")]
        [AdminAuthorize]
        
        public IActionResult Index()
        {
            return View(_BancoDados.Banners.ToList());
        }

    }
}