using Events.ItAcademy.Application;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.MVC.Extensions;
using Events.ItAcademy.Persistence.Context;
using Events.ItAcademy.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'EventsItAcademyContextConnection' not found.");

builder.Services.AddDbContext<EventsItAcademyContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventsItAcademyContext>();
builder.Services.AddTransient<IPasswordHasher<User>, MyHasher>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddServices();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
Events_ItAcademySeed.Initialize(app.Services);
app.Run();
