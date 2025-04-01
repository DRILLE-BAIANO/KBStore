using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GsStore.Models;
using GsStore.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GsStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        Produto produtos = _db.Produtos
           .Where(p => p.Destaque)
           .Include(p => p.Fotos)
           .Include(p => p.Categoria)
           .ToList();

        List<Produto> semelhantes = _db.Produtos
            .Where(p => p.CategoriaId == produtos.CategoriaId && p.Id != id)
            .Include(p => p.Fotos)
            .Take(4)
            .ToList();

        ProdutoVM produtoVM = new()
        {
            produto = produtos,
            Semelhantes = semelhantes
        }
        return View(produtos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
