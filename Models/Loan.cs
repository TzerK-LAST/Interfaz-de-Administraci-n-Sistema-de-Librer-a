using System.ComponentModel.DataAnnotations;

namespace InterfazdeAdministración_SistemadeLibrería.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un usuario")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un libro")]
        public int BookId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string estado { get; set; } = string.Empty;

        public User? User { get; set; }   // ✅ nullable
        public book? Book { get; set; }   // ✅ nullable
    }
}