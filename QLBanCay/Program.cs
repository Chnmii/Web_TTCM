using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using QLBanCay.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionstring = builder.Configuration.GetConnectionString("QlbanCayContext");

builder.Services.AddDbContext<QlbanCayContext>(x => x.UseSqlServer(connectionstring));

builder.Services.AddScoped<ILoaiCayRepository, LoaiCayRepository>();

builder.Services.AddSession();


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

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
