using System.Diagnostics;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GemManagment.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnlyticService anlyticService;

        public HomeController(IAnlyticService anlyticService)
        {
            this.anlyticService = anlyticService;
        }

        public IActionResult Index()
        {
            var analyticsData = anlyticService.GetAnalyticsData();
            return View(analyticsData);
        }

     
    }
}
