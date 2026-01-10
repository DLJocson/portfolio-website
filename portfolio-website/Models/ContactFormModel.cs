using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class ContactFormModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200)]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000)]
        public required string Message { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class ProjectModel
    {
        public int Id { get; set; }
        
        [Required]
        public required string Key { get; set; }
        
        [Required]
        public required string Title { get; set; }
        
        [Required]
        public required string Description { get; set; }
        
        public required string Icon { get; set; }
        public required string ColorClass { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();

        // Static data methods (can be replaced with database queries later)
        public static List<ProjectModel> GetAllProjects()
        {
            return new List<ProjectModel>
            {
                new ProjectModel
                {
                    Key = "ecomm",
                    Title = "E-Commerce Dashboard",
                    Description = "Full-featured MVC admin panel for managing products, real-time orders, and customer analytics.",
                    Icon = "shopping-bag",
                    ColorClass = "purple",
                    Technologies = new List<string> { "MVC", "EF Core", "ASP.NET MVC", "Entity Framework Core", "SQL Server", "Chart.js", "Bootstrap" }
                },
                new ProjectModel
                {
                    Key = "task",
                    Title = "TaskMaster Pro",
                    Description = "Productivity application enabling teams to assign real-time tickets with AJAX updates.",
                    Icon = "check-square",
                    ColorClass = "emerald",
                    Technologies = new List<string> { "SignalR", "C#", "Razor Pages", "jQuery", "AJAX" }
                },
                new ProjectModel
                {
                    Key = "finance",
                    Title = "Finance Tracker",
                    Description = "Personal budgeting tool with interactive JS charts, secure authentication, and Excel reporting.",
                    Icon = "pie-chart",
                    ColorClass = "orange",
                    Technologies = new List<string> { "Chart.js", ".NET Core", "Identity", "Web API" }
                },
                new ProjectModel
                {
                    Key = "ai",
                    Title = "AI Image Generator",
                    Description = "Generates art using OpenAI API and .NET backend.",
                    Icon = "bot",
                    ColorClass = "blue",
                    Technologies = new List<string> { "OpenAI", ".NET 8", "OpenAI API", "React", "Azure Blob Storage" }
                },
                new ProjectModel
                {
                    Key = "chat",
                    Title = "Real-time Live Chat",
                    Description = "Instant messaging platform using SignalR hubs.",
                    Icon = "message-square",
                    ColorClass = "pink",
                    Technologies = new List<string> { "SignalR", "ASP.NET Core", "WebSockets", "Redis" }
                }
            };
        }

        public static ProjectModel? GetProjectById(string key)
        {
            return GetAllProjects().FirstOrDefault(p => p.Key == key);
        }

        public static object? GetTechInfo(string key)
        {
            var techData = new Dictionary<string, object>
            {
                { "net", new { title = ".NET Core", desc = "A cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected apps." } },
                { "mvc", new { title = "ASP.NET MVC", desc = "A rich framework for building web apps using the Model-View-Controller design pattern." } },
                { "sql", new { title = "SQL Server", desc = "A relational database management system developed by Microsoft, supporting a wide variety of transaction processing and analytics applications." } },
                { "csharp", new { title = "C#", desc = "A modern, object-oriented, and type-safe programming language. C# enables developers to build many types of secure and robust applications." } }
            };

            return techData.ContainsKey(key) ? techData[key] : null;
        }
    }
}