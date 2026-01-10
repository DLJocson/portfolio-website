using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly PortfolioDbContext _context;

        public HomeController(PortfolioDbContext context)
        {
            _context = context;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Save to database
                _context.ContactForms.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = $"Thank you, {model.Name}! Your message has been sent successfully." });
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

        public IActionResult DownloadResume()
        {
            // Path to your resume file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "Dan_Resume.pdf");
            
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/pdf", "Dan_Resume.pdf");
            }

            return NotFound();
        }
    }
}