using InterfazdeAdministración_SistemadeLibrería.Data;
using InterfazdeAdministración_SistemadeLibrería.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterfazdeAdministración_SistemadeLibrería.Controllers
{
    public class LoanController : Controller
    {
        private readonly DataContext _context;

        public LoanController(DataContext context)
        {
            _context = context;
        }

        // LISTADO
        public IActionResult Index()
        {
            var loans = _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .ToList();

            return View("~/Views/prestamo/Loan.cshtml", loans);
        }

        // FORM
        public IActionResult Create()
        {
            LoadData();
            return View("~/Views/prestamo/Create.cshtml");
        }

        // GUARDAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Loan loan)
        {
            Console.WriteLine($"UserId: {loan.UserId}");
            Console.WriteLine($"BookId: {loan.BookId}");

            if (!ModelState.IsValid)
            {
                LoadData();
                return View("~/Views/prestamo/Create.cshtml", loan);
            }

            var book = _context.Books.Find(loan.BookId);

            if (book == null || book.stock <= 0)
            {
                ModelState.AddModelError("BookId", "No hay stock disponible");
                LoadData();
                return View("~/Views/prestamo/Create.cshtml", loan);
            }

            loan.StartDate = DateTime.Now;
            loan.estado = "Activo";

            book.stock--;

            _context.Loans.Add(loan);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DETALLE
        public IActionResult Show(int id)
        {
            var loan = _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .FirstOrDefault(l => l.Id == id);

            if (loan == null) return NotFound();

            return View("~/Views/prestamo/Show.cshtml", loan);
        }

        // DEVOLVER
        [HttpPost]
        public IActionResult Return(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null) return NotFound();

            loan.estado = "Devuelto";
            loan.EndDate = DateTime.Now;

            var book = _context.Books.Find(loan.BookId);
            if (book != null) book.stock++;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ELIMINAR
        [HttpPost]
        public IActionResult Destroy(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null) return NotFound();

            _context.Loans.Remove(loan);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void LoadData()
        {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Books = _context.Books.ToList();
        }
    }
}