using Microsoft.EntityFrameworkCore;
using SisMot.Data;
using SisMot.Repositories;
using SisMot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbsisMotContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMotelRepository, MotelService>();
builder.Services.AddScoped<IAccess, AccessService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Motel}/{action=Index}");

app.Run();
