using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
<<<<<<< HEAD

=======
>>>>>>> 0f4116f8f5f784e210a9fdb167874b3aa19f78f6
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLyTourContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConet")));
<<<<<<< HEAD


=======
>>>>>>> 0f4116f8f5f784e210a9fdb167874b3aa19f78f6

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
