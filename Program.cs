using Microsoft.EntityFrameworkCore;
using Educational_platform.Data;

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

var app = builder.Build();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
