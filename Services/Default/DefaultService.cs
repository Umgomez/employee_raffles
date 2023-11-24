using employee_raffles.Data;
using employee_raffles.Structs;

namespace employee_raffles.Services;

public interface IDefaultService
{
    Task<Return> GetEmployees();
}
public class DefaultService : IDefaultService
{
    private readonly ApplicationDbContext context;

    public DefaultService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Return> GetEmployees()
    {
        var sql = new Sql(this.context);
        var file = "GetEmployees";
        var query = File.ReadAllText($"Data/Default/{file}.sql");
        var entityData = await sql.OneQuery(query);

        return new Return($"File '{file}' data").SetData(entityData);
    }
}
