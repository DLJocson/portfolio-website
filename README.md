## Portfolio Website - ASP.NET Core MVC

A responsive, modern portfolio website built with ASP.NET Core MVC 8.0, Tailwind CSS, and interactive JavaScript. Frontend-focused with minimal backend logic.

## Project Structure

```
/Controllers
    └─ HomeController.cs        # Routing and lightweight API endpoints

/Models
    ├─ ContactFormModel.cs      # Contact form validation model
    └─ ProjectModel.cs          # Project data and tech stack info

/Views
    /Home
        ├─ Index.cshtml         # Hero & core technologies section
        ├─ About.cshtml         # Full professional profile & skills
        ├─ Projects.cshtml      # Portfolio grid with modal interactions
        ├─ Contact.cshtml       # Contact form with submission
        └─ Error.cshtml         # Error page with trace ID
    /Shared
        ├─ _Layout.cshtml       # Main layout with nav, footer, modals
        ├─ _Modal.cshtml        # Reusable modal component
        └─ _ViewStart.cshtml    # Layout routing

/wwwroot
    /css
        └─ site.css             # Custom styles: nav effects, utilities, focus states
    /js
        └─ site.js              # Client-side handlers: theme, modal, forms, API calls
    /files                       # Reserved for downloadable assets

/Properties
    └─ launchSettings.json       # Development/production profiles

/bin & /obj                      # Build outputs (git-ignored)

Program.cs                        # App configuration & middleware pipeline
portfolio-website.csproj          # Project file (.NET 8.0)
appsettings.json                 # Production logging & allowed hosts
appsettings.Development.json      # Development-specific settings
```

## Key Features

- **Responsive Design**: Mobile-first Tailwind CSS with dark mode support
- **Interactive Modals**: View project details, tech stack, and download resume
- **Smooth Animations**: Fade-in, slide-up, and pop-in effects
- **Contact Form**: Client-side validation with feedback modals
- **Icon Integration**: Lucide icons for modern, crisp UI
- **Accessibility**: Semantic HTML, ARIA attributes, keyboard navigation

## Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Frontend**: Tailwind CSS (CDN), Vanilla JavaScript
- **Styling**: Custom CSS utilities, animations, dark mode
- **Icons**: Lucide UI (CDN)
- **Fonts**: Google Fonts (Roboto)
- **Form Validation**: Data Annotations + Client-side checks

## Running the Application

### Development
```bash
dotnet run
# Open http://localhost:5288 or https://localhost:7190
```

### Production Build
```bash
dotnet publish -c Release
# Serves with aggressive static file caching (1 year, immutable)
```

## Code Quality Practices

- **Intent-focused comments**: Only where logic or design decisions are non-obvious
- **Separation of concerns**: Controllers, Models, Views properly segregated
- **DRY principles**: Reusable modal system, shared layouts
- **Consistent naming**: Clear, descriptive identifiers throughout
- **Performance**: Response caching, static file optimization, CDN resources

## Development Notes

### Frontend-Only Features
- Contact form: Validates and displays feedback modals (no persistence)
- Resume download: Simulated modal states (no actual file)
- Project modals: Static data from ProjectModel.GetAllProjects()

### Future Enhancements
- Database integration for project data persistence
- Email service for contact form submissions
- Actual resume PDF download
- Blog or case study pages
- Search functionality

## Browser Support

- Chrome/Edge (latest)
- Firefox (latest)
- Safari (latest)
- Mobile browsers (responsive)

---

**Last Updated**: January 2024

