using employee_raffles.Data;
using employee_raffles.Structs;

namespace employee_raffles.Services;

public interface IDefaultService
{
    Task<Return> GetRandomEmployee();
    Task<Return> GetEmployeeById(int id);
}
public class DefaultService : IDefaultService
{
    private readonly ApplicationDbContext context;

    public DefaultService(ApplicationDbContext context)
    {
        this.context = context;
    }


    public async Task<Return> GetRandomEmployee()
    {
        var sql = new Sql(this.context);
        var file = "GetRandomEmployee";
        var query = File.ReadAllText($"Data/Default/{file}.sql");
        var entityData = await sql.OneQuery(query);

        return new Return($"File '{file}' data").SetData(entityData);
    }

    public async Task<Return> GetEmployeeById(int id)
    {
        var sql = new Sql(this.context);
        var file = "GetEmployeeById";
        var query = File.ReadAllText($"Data/Default/{file}.sql");
        var data = new Dictionary<string, object> { { "EmpleadoID", id } };
        query = await sql.QueryFormat(query, data);
        var entityData = await sql.OneQuery(query);

        return new Return($"File '{file}' data").SetData(entityData);
    }
}
