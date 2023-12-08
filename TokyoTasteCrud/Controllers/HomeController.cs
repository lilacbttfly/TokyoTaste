using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TokyoTasteCrud.Models;
using App.Context;
using System.Xml;
using System.Text;
using X.PagedList;
using App.Models;

namespace TokyoTasteCrud.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? txtFiltro, string? selOrdenacao, string? cboOrdenacao, int pagina = 1)
    {
        

        IQueryable<Usuario> listaView = _context.Usuarios;
        
        if (!string.IsNullOrEmpty(txtFiltro))
        {
            ViewData["txtFiltro"] = txtFiltro;
            listaView = listaView.Where(item => item.Nome.ToLower().Contains(txtFiltro));
        }
        return View(listaView.ToList());
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

    public IActionResult Produtos(string? txtFiltro, string? selOrdenacao, string? cboOrdenacao, int pagina = 1)
    {
        int pageSize = 10; //
        IQueryable<Produto> lista = _context.Produtos;
        if (txtFiltro != null && txtFiltro != "")
        {
            ViewData["txtFiltro"] = txtFiltro;
            lista = lista.Where(item =>
            item.ProdutoNome.ToLower().Contains(txtFiltro.ToLower())
            ||
            item.ProdutoNome.ToLower().Contains(txtFiltro.ToLower()));
        }
        if (selOrdenacao == "Nome" || selOrdenacao == null)
        {
            lista = lista.OrderBy(item => item.ProdutoNome.ToLower());
        }
        else if (selOrdenacao == "Nome_Desc")
        {
            lista = lista.OrderBy(item => item.Preco);
        }
        else if (selOrdenacao == "Login")
        {
            lista = lista.OrderByDescending(item => item.Preco);
        }
        return View(lista.ToPagedList(pagina, pageSize));
    }
}