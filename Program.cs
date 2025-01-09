using ErrorHandlerApp.Data;
using Microsoft.EntityFrameworkCore;
using ErrorHandlerApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Users.Any())
    {
        var admin = new User
        {
            Username = "admin",
            PasswordHash = PasswordHasher.HashPassword("admin"), // Has³o: admin
            Role = "Admin"
        };

        var user = new User
        {
            Username = "user",
            PasswordHash = PasswordHasher.HashPassword("user"), // Has³o: user
            Role = "User"
        };

        context.Users.Add(admin);
        context.Users.Add(user);
        context.SaveChanges();
    }
}



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
