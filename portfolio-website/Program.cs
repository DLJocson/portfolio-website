// === Program.cs ===
// Purpose: Bootstraps the ASP.NET Core portfolio site, registering MVC, performance middleware,
// and environment-aware static file caching for fast local tweaks and production resiliency.

var builder = WebApplication.CreateBuilder(args);

// Register MVC and lightweight performance middleware
builder.Services.AddControllersWithViews();
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure middleware pipeline per environment
if (!app.Environment.IsDevelopment())
{
    // Production error handling and HTTPS enforcement
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseResponseCompression();
app.UseResponseCaching();
app.UseRouting();

// Serve static files with environment-aware caching headers
// Development: no-cache to reflect CSS/JS changes quickly
// Production: aggressive long-term caching (1 year, immutable)
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control",
            app.Environment.IsDevelopment()
                ? "no-store, no-cache, must-revalidate"
                : "public,max-age=31536000,immutable"
        );
    }
});

// Default MVC routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
