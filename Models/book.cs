namespace InterfazdeAdministración_SistemadeLibrería.Models;

public class book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
    public required string genero { get; set; }
    public int? publicationYear { get; set; }
    public int quantity { get; set; }
    public int stock { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}