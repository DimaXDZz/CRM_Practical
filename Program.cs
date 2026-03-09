using Microsoft.AspNetCore.Builder;
using WebApplication1.Classes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;

    });
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapGet("/", () => "Hello, World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=GetAllUsers}/{id?}");

using (DBContext db = new DBContext())
{
    
    var users = db.User.ToList();
    Console.WriteLine("Users list:");
    foreach (User u in users)
    {
        Console.WriteLine($"{u.ID}.{u.User_Name}.{u.User_MidName}.{u.User_LastName}.{u.Hire_date}");
    }
}

app.Run();
