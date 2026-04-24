using System.Diagnostics;
using InterfazdeAdministración_SistemadeLibrería.Data;
using InterfazdeAdministración_SistemadeLibrería.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterfazdeAdministración_SistemadeLibrería.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;

    public HomeController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View("~/Views/book/libros.cshtml", books); // ← book minúscula ✅
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult User()
    {
        var users = _context.Users.ToList();
        return View("~/Views/users/User.cshtml", users); // ← users minúscula y plural ✅
    }
}