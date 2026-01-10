// Home Controller - Minimal backend for frontend-focused portfolio
// Serves views and provides JSON endpoints for project data

using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Projects()
        {
            var projects = ProjectModel.GetAllProjects();
            return View(projects);
        }

        public IActionResult Contact()
        {
            // Return empty model for form binding
            return View(new ContactFormModel 
            { 
                Name = string.Empty, 
                Email = string.Empty, 
                Message = string.Empty 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(ContactFormModel model)
        {
            // Frontend-only simulation - no persistence
            // This endpoint is called from site.js for validation only
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Contact form submitted: {Name}, {Email}", model.Name, model.Email);
                return Json(new { success = true, message = $"Thank you, {model.Name}! Your message has been received." });
            }

            return Json(new { success = false, message = "Please fill out all fields correctly." });
        }

        [HttpGet]
        public IActionResult GetProjectDetails(string id)
        {
            var project = ProjectModel.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Json(project);
        }

        [HttpGet]
        public IActionResult GetTechInfo(string id)
        {
            var techInfo = ProjectModel.GetTechInfo(id);
            if (techInfo == null)
            {
                return NotFound();
            }

            return Json(techInfo);
        }

        public IActionResult Error()
        {
            var traceId = HttpContext.TraceIdentifier;
            _logger.LogError("Unhandled error occurred. TraceIdentifier: {TraceId}", traceId);
            ViewBag.TraceIdentifier = traceId;
            return View("Error");
        }
    }
}
