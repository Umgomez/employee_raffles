namespace employee_raffles.Models.Default;

public class Awards
{
    public int ID { get; set; }
    public int Sequence { get; set; }
    public string Amount { get; set; }
    public bool IsSelected { get; set; } = false;
}
