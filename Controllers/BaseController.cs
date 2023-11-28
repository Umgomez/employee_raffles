using employee_raffles.Data;
using employee_raffles.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace employee_raffles.Controllers;

public class BaseController : Controller
{
    internal readonly ApplicationDbContext context;
    private readonly IDefaultService defaultService;

    public BaseController(ApplicationDbContext context, IDefaultService defaultService)
    {
        this.context = context;
        this.defaultService = defaultService;
    }

    public async Task<dynamic> GetRandomEmployeeAsync()
    {
        return await defaultService.GetRandomEmployee();
    }

    public async Task<dynamic> GetNextAward()
    {
        return await defaultService.GetNextAward();
    }
}
