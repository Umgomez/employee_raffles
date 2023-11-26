using employee_raffles.Data;
using employee_raffles.Models;
using employee_raffles.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace employee_raffles.Controllers;

public class HomeController : BaseController
{
    private new readonly ApplicationDbContext context;
    private readonly IDefaultService defaultService;

    public HomeController(ApplicationDbContext context, IDefaultService defaultService) : base(context, defaultService)
    {
        this.context = context;
        this.defaultService = defaultService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<dynamic> GetWinner()
    {
        dynamic RandomEmployee = await GetRandomEmployeeAsync();
        dynamic winner = await defaultService.GetEmployeeById(RandomEmployee.Data[0]["EmpleadoID"]);
        return new JsonResult(new { aadata = winner });
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> SetWinner(string EmpleadoID)
    {
        if (!string.IsNullOrEmpty(EmpleadoID))
        {
            int empId = Convert.ToInt32(EmpleadoID);
            var model = await context.Employees.FirstOrDefaultAsync(x => x.EmpleadoID == empId);
            if (model != null)
            {
                model.SelRifa = true;
                context.Employees.Update(model);
                await context.SaveChangesAsync();
            }               
        }

        return View("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
