using InterfazdeAdministración_SistemadeLibrería.Data;
using InterfazdeAdministración_SistemadeLibrería.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterfazdeAdministración_SistemadeLibrería.Controllers;

public class UserController : Controller
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }

    // /User
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View("~/Views/users/User.cshtml", users);
    }

    // /User/Users
    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View("~/Views/users/User.cshtml", users);
    }

    // /User/Create
    public IActionResult Create()
    {
        return View("~/Views/users/Create.cshtml");
    }

    // /User/Store
    [HttpPost]
    public IActionResult Store(User user)
    {
        if (!ModelState.IsValid)
            return View("~/Views/users/Create.cshtml", user);

        user.dateRegistered = DateTime.Now;
        _context.Users.Add(user);
        _context.SaveChanges();

        TempData["message"] = "Usuario creado correctamente.";
        return RedirectToAction("Users");
    }

    // /User/Show/1
    public IActionResult Show(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();
        return View("~/Views/users/Show.cshtml", user);
    }

    // /User/Edit/1
    public IActionResult Edit(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();
        return View("~/Views/users/Edit.cshtml", user);
    }

    // /User/Update
    [HttpPost]
    public IActionResult Update(User user)
    {
        if (!ModelState.IsValid)
            return View("~/Views/users/Edit.cshtml", user);

        _context.Users.Update(user);
        _context.SaveChanges();

        TempData["message"] = "Usuario actualizado correctamente.";
        return RedirectToAction("Users");
    }

    // /User/Destroy
    [HttpPost]
    public IActionResult Destroy(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();

        TempData["message"] = "Usuario eliminado correctamente.";
        return RedirectToAction("Users");
    }
}