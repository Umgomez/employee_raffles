using System.ComponentModel.DataAnnotations;

namespace employee_raffles.Models.Default;

public class Awards
{
    [Key]
    public int ID { get; set; }
    public int Sequence { get; set; }
    public string Amount { get; set; }
    public bool IsSelected { get; set; } = false;
}
