using InterfazdeAdministración_SistemadeLibrería.Data;
using InterfazdeAdministración_SistemadeLibrería.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterfazdeAdministración_SistemadeLibrería.Controllers;

public class BookController : Controller
{
    private readonly DataContext _context;

    public BookController(DataContext context)
    {
        _context = context;
    }

    // /Book — inicio sin botones
    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View("~/Views/book/libros.cshtml", books);
    }

    // /Book/Libros — gestión con botones
    public IActionResult Libros()
    {
        var books = _context.Books.ToList();
        return View("~/Views/book/libros.cshtml", books);
    }

    // /Book/Show/1 — ver detalle
    public IActionResult Show(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();
        return View("~/Views/book/Show.cshtml", book);
    }

    // /Book/Create — formulario crear
    public IActionResult Create()
    {
        return View("~/Views/book/create.cshtml");
    }

    // /Book/Store — guardar nuevo
    [HttpPost]
    public IActionResult Store(book book)
    {
        if (!ModelState.IsValid) return View("~/Views/book/create.cshtml", book);

        _context.Books.Add(book);
        _context.SaveChanges();

        TempData["message"] = "Libro creado correctamente.";
        return RedirectToAction("Libros");
    }

    // /Book/Edit/1 — formulario editar
    public IActionResult Edit(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();
        return View("~/Views/book/Edit.cshtml", book);
    }

    // /Book/Update — guardar cambios
    [HttpPost]
    public IActionResult Update(book book)
    {
        if (!ModelState.IsValid) return View("~/Views/book/Edit.cshtml", book);

        _context.Books.Update(book);
        _context.SaveChanges();

        TempData["message"] = "Libro actualizado correctamente.";
        return RedirectToAction("Libros");
    }

    // /Book/Destroy — eliminar
    [HttpPost]
    public IActionResult Destroy(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        _context.SaveChanges();

        TempData["message"] = "Libro eliminado correctamente.";
        return RedirectToAction("Libros");
    }
}