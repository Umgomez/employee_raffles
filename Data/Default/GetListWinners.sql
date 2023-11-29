SELECT e.Tarjeta 
    , e.Cedula    
    , e.Nombres + ' ' + e.Apellidos AS NombreEmpleado
    , a.ID 
    , a.Amount 
FROM dbo.Employees e 
INNER JOIN dbo.Awards a ON e.AwardsId = a.ID 
ORDER BY a.ID 