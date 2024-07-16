using WebA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Housemaster_page()
        {
            return View();
        }
        public IActionResult Studentform_page()
        {
            return View();
        }
        public IActionResult Student_related_information()
        {
            return View();
        }
        public IActionResult Student_service_status()
        {
            return View();
        }
        public IActionResult Student_page()
        {
            return View();
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}