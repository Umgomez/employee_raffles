namespace employee_raffles.Models.ViewModels;

public class EmployeeViewModel
{
    public int EmpleadoID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Cedula { get; set; }
    public bool Asistencia { get; set; }
    public int NumeroRifa { get; set; }
    public bool SelRifa { get; set; }
}
