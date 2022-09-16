
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net;


string ConnectoinString = "Data Source=.;Initial Catalog = MohammadiEShop ; Integrated Security=True ;Connect Timeout=10";
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Listen(IPAddress.Parse("192.168.1.39"), 3000, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

/// کانتکس
builder.Services.AddDbContext<EShop.Models.Data.ShopContext>(options =>
             options.UseSqlServer(ConnectoinString));
// repository
builder.Services.AddScoped<EShop.Models.Data.Repository.UsersRepository>(); // کاربران
builder.Services.AddScoped<EShop.Models.Data.Repository.RolesRepository>(); // نقش ها
builder.Services.AddScoped<EShop.Models.Data.Repository.UserRoleRepository>(); // نقش کاربران
builder.Services.AddScoped<EShop.Models.Data.Repository.GroupRepository>(); // دسته بندی محصولات
/// <summary>
/// Cookie Autorize
/// </summary>
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/Login";
    });
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
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

app.UseRouting();// should be before UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();




