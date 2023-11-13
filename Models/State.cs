using System.ComponentModel.DataAnnotations.Schema;

namespace Detailing.Models;

public class State
{
    [Column("StateId")]
    public int Id { get; set; }

    public string Name { get; set; }
}