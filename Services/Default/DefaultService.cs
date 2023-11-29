using employee_raffles.Data;
using employee_raffles.Structs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace employee_raffles.Services;

public interface IDefaultService
{
    Task<Return> GetRandomEmployee();
    Task<Return> GetAttendance();
    Task<Return> GetEmployeeById(int id);
    Task<Return> GetEmployeeByIdentification(string identification);
    Task<Return> GetNextAward();
    Task<Return> GetListWinners();
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

    public async Task<Return> GetAttendance()
    {
        var sql = new Sql(this.context);
        var file = "GetAttendance";
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

    public async Task<Return> GetEmployeeByIdentification(string identification)
    {
        var sql = new Sql(this.context);
        var file = "GetEmployeeByIdentification";
        var query = File.ReadAllText($"Data/Default/{file}.sql");
        var data = new Dictionary<string, object> { { "IdentificationNumber", identification } };
        query = await sql.QueryFormat(query, data);
        var entityData = await sql.OneQuery(query);

        return new Return($"File '{file}' data").SetData(entityData);
    }

    public async Task<Return> GetNextAward()
    {
        var sql = new Sql(this.context);
        var file = "GetNextAward";
        var query = File.ReadAllText($"Data/Default/{file}.sql");
        var entityData = await sql.OneQuery(query);

        return new Return($"File '{file}' data").SetData(entityData);
    }

    public async Task<Return> GetListWinners()
    {
        var sql = new Sql(this.context);
        var file = "GetListWinners";
        var query = File.ReadAllText($"Data/Default/{file}.sql");
        var entityData = await sql.OneQuery(query);

        return new Return($"File '{file}' data").SetData(entityData);
    }
}
