namespace employee_raffles.Models.Default;

public class Employees
{
    public int EmpleadoID {  get; set; }
    public string Tarjeta { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Cedula { get;  set; }
    public bool Asistencia { get; set; }
    public int NumeroRifa { get; set; }
    public bool SelRifa { get; set; }

    public int? AwardsId { get; set; }
    public Awards Awards { get; set; }
}
