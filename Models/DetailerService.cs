using System.ComponentModel.DataAnnotations.Schema;
using Detailing.Interfaces;

namespace Detailing.Models;

public class DetailerService : IModel
{
    [Column("ServiceId")]
    public int Id { get; set; }

    [Column("UserId")]
    public int DetailerId { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Cost")]
    public double Cost { get; set; }

    [Column("Time")]
    public int ETA { get; set; }

    [Column("CreationDate")]
    public DateTime AddedOn { get; set; }

    [Column("IsMobile")]
    public bool IsMobile { get; set; }
}