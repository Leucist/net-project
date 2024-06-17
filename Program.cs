using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Educational_platform.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// - Added so the server will be visible in a local network - (c) leucist
// builder.WebHost.UseUrls("http://0.0.0.0:5000");


// AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "Database"));
// Set up the data directory
AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(builder.Environment.ContentRootPath, "Database"));


// Add services to the container.
builder.Services.AddRazorPages();


// Add the database context configuration here
builder.Services.AddDbContext<UsersContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("EducationalPlatformDB"), 
    new MySqlServerVersion(new Version(8, 0, 2))));

// // Configure cookies
// builder.Services.ConfigureApplicationCookie(options =>
// {
//     options.LoginPath = "/Account/Login";
//     options.AccessDeniedPath = "/Account/AccessDenied";
// });

// Add the Identity service
builder.Services.AddIdentity<Educational_platform.Models.Users, Educational_platform.Models.Role>()
    .AddEntityFrameworkStores<UsersContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Revo.Cookie";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.LoginPath = "/SignIn";
    options.LogoutPath = "/Logout";
    // options.AccessDeniedPath = "/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});


var app = builder.Build();

// Set data seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
