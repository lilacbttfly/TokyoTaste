using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Context;
using App.Models;
using App.Filters;
using System.Xml;
using System.Text;

namespace TokyoTasteCrud.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index(string botao, string? txtFiltro)
        {
            int pageSize = 5;
            IQueryable<Produto> lista = _context.Produtos.Include(p => p.Categoria);
            if (botao == "Relatorio")
            {
                pageSize = lista.Count();
            }


            if (botao == "XML")
            {
                //Chamando o método para exportar o XML enviando como parâmentro a lista já filtrada
                return ExportarXML(lista.ToList());
            }
            else if (botao == "Json")
            {
                //Chamando o método para exportar o Json enviando como parâmentro a lista já filtrada
                return ExportarJson(lista.ToList());
            }
            if (!string.IsNullOrEmpty(txtFiltro))
            {
                ViewData["txtFiltro"] = txtFiltro;
                lista = lista.Where(item => item.ProdutoNome.ToLower().Contains(txtFiltro));
            }
            return View(await lista.ToListAsync());


        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "ProdutoId", "ProdutoNome");
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,ProdutoNome,Descricao,Preco,Foto,CategoriaId")] Produto produto)
        {
            if (true)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "ProdutoId", "ProdutoId", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "ProdutoId", "ProdutoNome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,ProdutoNome,Descricao,Preco,Foto,CategoriaId")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "ProdutoId", "ProdutoId", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos?.Any(e => e.ProdutoId == id)).GetValueOrDefault();
        }
        private IActionResult ExportarXML(List<Produto> lista)
        {
            var stream = new StringWriter();
            var xml = new XmlTextWriter(stream);

            xml.Formatting = System.Xml.Formatting.Indented;

            xml.WriteStartDocument();
            xml.WriteStartElement("Dados");

            xml.WriteStartElement("Produtos");
            foreach (var produto in lista)
            {
                xml.WriteStartElement("País");
                xml.WriteElementString("Id", produto.ProdutoId.ToString());
                xml.WriteElementString("Nome", produto.ProdutoNome);
                xml.WriteElementString("Capital", produto.Descricao);
                xml.WriteElementString("População", produto.Preco.ToString());
                xml.WriteElementString("Continente", produto.Foto);
                xml.WriteEndElement(); // </Pais>
            }
            xml.WriteEndElement(); // </Paises>

            xml.WriteEndElement(); // </Data>
            return File(Encoding.UTF8.GetBytes(stream.ToString()), "application/xml", "dados_paises.xml");
        }

        private IActionResult ExportarJson(List<Produto> lista)
        {
            var json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine("  \"Produtos\": [");
            int total = 0;
            foreach (var produto in lista)
            {
                json.AppendLine("    {");
                json.AppendLine($"      \"Id\": {produto.ProdutoId},");
                json.AppendLine($"      \"Nome\": \"{produto.ProdutoNome}\",");
                json.AppendLine($"      \"Capital\": \"{produto.Descricao}\",");
                json.AppendLine($"      \"Continente\": \"{produto.Preco}\",");
                json.AppendLine($"      \"População\": {produto.Foto}");
                json.AppendLine("    }");
                total++;
                if (total < lista.Count())
                {
                    json.AppendLine("    ,");
                }
            }
            json.AppendLine("  ]");
            json.AppendLine("}");

            return File(Encoding.UTF8.GetBytes(json.ToString()), "application/json", "dados_paises.json");
        }



    }
}
