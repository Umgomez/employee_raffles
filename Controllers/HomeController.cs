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

    [Route("getwinner")]
    public IActionResult Index()
    {
        return View();
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<dynamic> GetWinner()
    {
        dynamic NextAward = await GetNextAward();
        dynamic RandomEmployee = await GetRandomEmployeeAsync();
        dynamic win = await defaultService.GetEmployeeById(RandomEmployee.Data[0]["EmpleadoID"]);

        return new JsonResult(new { winner = win, award = NextAward });
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> SetWinner(string EmpleadoID, string AwardId)
    {
        if (!string.IsNullOrEmpty(EmpleadoID) || !string.IsNullOrEmpty(AwardId))
        {
            int empId = Convert.ToInt32(EmpleadoID);
            int awardId = Convert.ToInt32(AwardId);
            var model = await context.Employees.FirstOrDefaultAsync(x => x.EmpleadoID == empId);
            var award = await context.Awards.FirstOrDefaultAsync(x => x.ID == awardId);
            if (model != null && award != null)
            {
                model.SelRifa = true;
                model.AwardsId = award.ID;

                award.IsSelected = true;

                context.Employees.Update(model);
                context.Awards.Update(award);
                await context.SaveChangesAsync();
            }  
        }

        return View("Index");
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> OutWinner(string EmpleadoID)
    {
        if (!string.IsNullOrEmpty(EmpleadoID))
        {
            int empId = Convert.ToInt32(EmpleadoID);
            var model = await context.Employees.FirstOrDefaultAsync(x => x.EmpleadoID == empId);
            if (model != null)
            {
                model.Asistencia = false;
                context.Employees.Update(model);
                await context.SaveChangesAsync();
            }
        }

        return View("Index");
    }

    [HttpGet]
    [Route("asistencia")]
    public IActionResult ConfirmarAsistencia()
    {
        return View();
    }

    [AcceptVerbs("Get", "Post")] //Metodo para Confirmar Asistencia
    public async Task<dynamic> GetEmployeeByIdentification(string IdentificationNumber)
    {
        dynamic employee = await defaultService.GetEmployeeByIdentification(IdentificationNumber);
        return new JsonResult(new { aadata = employee });
    }

    [HttpGet]
    [Route("winnerslist")]
    public async Task<IActionResult> GetListWinners()
    {
        ViewData["Winners"] = await defaultService.GetListWinners();
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
