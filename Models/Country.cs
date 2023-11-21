using System.ComponentModel.DataAnnotations.Schema;

namespace Detailing.Models;

public class Country
{
    [Column("CountryId")]
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public IEnumerable<State>? States { get; set; }
}