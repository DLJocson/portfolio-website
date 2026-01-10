// ASP.NET Core Portfolio Website - Application Entry Point
// Minimal MVC configuration for frontend-focused portfolio

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.Services.AddResponseCaching();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseResponseCompression();
app.UseResponseCaching();

app.UseRouting();

// Serve static files with environment-specific caching
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if (app.Environment.IsDevelopment())
        {
            // No caching in development for easy CSS/JS updates
            ctx.Context.Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate");
        }
        else
        {
            // Aggressive caching in production
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000,immutable");
        }
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
