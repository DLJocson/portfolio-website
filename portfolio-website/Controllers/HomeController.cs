// === HomeController.cs ===
// Purpose: Serve portfolio views and expose lightweight JSON endpoints that back the modal-driven UI.

using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    /// <summary>
    /// Manages routing for portfolio views and lightweight JSON API endpoints.
    /// No database persistence; data-driven by static ProjectModel and form validation only.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // --- Portfolio Views ---
        
        public IActionResult Index() => View();

        public IActionResult About() => View();

        public IActionResult Projects()
        {
            var projects = ProjectModel.GetAllProjects();
            return View(projects);
        }

        public IActionResult Contact()
        {
            // Initialize empty model for form binding and client-side submission.
            return View(new ContactFormModel 
            { 
                Name = string.Empty, 
                Email = string.Empty, 
                Message = string.Empty 
            });
        }

        // --- API Endpoints ---
        
        /// <summary>
        /// Accepts and validates contact form submission (frontend only).
        /// No persistence; intent is form validation and user feedback via modal.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Contact form submitted: {Name}, {Email}", model.Name, model.Email);
                return Json(new { success = true, message = $"Thank you, {model.Name}! Your message has been received." });
            }

            return Json(new { success = false, message = "Please fill out all fields correctly." });
        }

        /// <summary>
        /// Retrieves project details by key for modal display.
        /// </summary>
        [HttpGet]
        public IActionResult GetProjectDetails(string id)
        {
            var project = ProjectModel.GetProjectById(id);
            return project == null ? NotFound() : Json(project);
        }

        /// <summary>
        /// Retrieves tech stack information for tech icon tooltips.
        /// </summary>
        [HttpGet]
        public IActionResult GetTechInfo(string id)
        {
            var techInfo = ProjectModel.GetTechInfo(id);
            return techInfo == null ? NotFound() : Json(techInfo);
        }

        // --- Error Handling ---
        
        public IActionResult Error()
        {
            var traceId = HttpContext.TraceIdentifier;
            _logger.LogError("Unhandled error occurred. TraceIdentifier: {TraceId}", traceId);
            ViewBag.TraceIdentifier = traceId;
            return View("Error");
        }
    }
}
