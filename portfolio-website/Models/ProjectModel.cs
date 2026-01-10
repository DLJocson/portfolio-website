using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    // Portfolio project representation with static in-memory data.
    // Intent: keep UI rendering fast without backend persistence.
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
        public required string RepositoryUrl { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();

        // Static data methods (can be replaced with database queries later)
        public static List<ProjectModel> GetAllProjects()
        {
            return new List<ProjectModel>
            {
                new ProjectModel
                {
                    Key = "dfa-phishing",
                    Title = "DFA-Based Phishing URL Detection",
                    Description = "Designed and implemented a hierarchical Deterministic Finite Automata (DFA) model to accurately detect and classify phishing URLs. This project demonstrates the application of automata theory in cybersecurity.",
                    Icon = "shield-alert",
                    ColorClass = "red",
                    RepositoryUrl = "https://github.com/DLJocson/dfa-phishing-detector.git",
                    Technologies = new List<string> { "Python", "JavaScript", "CSS", "Automata Theory" }
                },
                new ProjectModel
                {
                    Key = "career-rec",
                    Title = "Career Recommendation System",
                    Description = "A console-based, rule-driven career path recommendation system built in C#. It suggests suitable careers based on user-provided skills, interests, and preferences, showcasing practical applications of decision-making algorithms.",
                    Icon = "briefcase",
                    ColorClass = "blue",
                    RepositoryUrl = "https://github.com/DLJocson/career-recommendation-system.git",
                    Technologies = new List<string> { "C#", "Decision Algorithms", "Console Application" }
                },
                new ProjectModel
                {
                    Key = "awscc-ui-ux",
                    Title = "AWSCCPUP CodeQuest UI/UX",
                    Description = "Personal repository for the '30 Days of UI/UX' challenge organized by AWSCC-PUP. Focused on design principles, prototyping, and user experience improvements using Figma.",
                    Icon = "palette",
                    ColorClass = "orange",
                    RepositoryUrl = "https://github.com/DLJocson/awsccpup-codequest-uiux.git",
                    Technologies = new List<string> { "Figma", "UI/UX Design", "Prototyping" }
                },
                new ProjectModel
                {
                    Key = "energy-task-list",
                    Title = "Energy-Aware Task List",
                    Description = "A specialized C# To-Do application that integrates energy management concepts. By assigning Energy Costs to tasks and setting a Daily Energy Budget, the system helps users prevent burnout and plan their day according to actual capacity.",
                    Icon = "zap",
                    ColorClass = "emerald",
                    RepositoryUrl = "https://github.com/DLJocson/energy-aware-task-list",
                    Technologies = new List<string> { "C#", "HTML", "CSS", "JavaScript" }
                },
                new ProjectModel
                {
                    Key = "echo-compiler",
                    Title = "ECHO Compiler",
                    Description = "ECHO (Executable Code, Human Output) is a modern programming language designed for human readability. This project contains a Lexical and a Syntax Analyzer of the Programming language.",
                    Icon = "code",
                    ColorClass = "violet",
                    RepositoryUrl = "https://github.com/ntoonie/ECHO_Programming_Language.git",
                    Technologies = new List<string> { "JavaScript", "CSS", "HTML" }
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
                { "python", new { title = "Python", desc = "Skilled in building compilers, syntax analyzers, and automation scripts. I use Python for rapid prototyping, data analysis, and AI-assisted development, ensuring clean and maintainable codebases." } },
                { "csharp", new { title = "C#", desc = "Strong experience in developing rule-driven applications, console systems, and specialized tools (e.g., energy-aware task management). I apply strict MVC conventions and prioritize maintainability." } },
                { "javascript", new { title = "JavaScript", desc = "Proficient in creating interactive web applications and UI enhancements. I emphasize modularity, readability, and integration with modern frameworks while keeping performance in mind." } },
                { "htmlcss", new { title = "HTML & CSS", desc = "Deep understanding of semantic HTML and responsive CSS design. I balance technical structure with aesthetic sensibility, ensuring accessible and visually appealing interfaces." } },
                { "figma", new { title = "Figma", desc = "Skilled in UI/UX prototyping and design systems. I leverage Figma to translate technical workflows into intuitive user experiences, bridging the gap between design and development." } },
                { "net", new { title = ".NET Core", desc = "A cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected apps." } },
                { "mvc", new { title = "ASP.NET MVC", desc = "A rich framework for building web apps using the Model-View-Controller design pattern." } },
                { "sql", new { title = "SQL Server", desc = "A relational database management system developed by Microsoft, supporting a wide variety of transaction processing and analytics applications." } }
            };

            return techData.ContainsKey(key) ? techData[key] : null;
        }
    }
}
