using employee_raffles.Data;
using employee_raffles.Models;
using employee_raffles.Models.ViewModels;
using employee_raffles.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace employee_raffles.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IDefaultService defaultService;

        public HomeController(ApplicationDbContext context, IDefaultService defaultService) : base(context, defaultService)
        {
            this.context = context;
            this.defaultService = defaultService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewData["Employees"] = await GetAllEmployeesAsync();

            return View("Index");
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
