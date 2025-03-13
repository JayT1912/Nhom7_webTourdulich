using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(); // Thêm session
builder.Services.AddAuthentication(); // Thêm xác thực

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLyTourContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseSession(); // Bật session


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
