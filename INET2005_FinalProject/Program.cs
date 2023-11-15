using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using INET2005_FinalProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<INET2005_FinalProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("INET2005_FinalProjectContext") ?? throw new InvalidOperationException("Connection string 'INET2005_FinalProjectContext' not found.")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.LoginPath = "/Admin/Login";
    options.LogoutPath = "/Admin/Logout";
});

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

// Page authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
