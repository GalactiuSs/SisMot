using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SisMot.Data;
using SisMot.Helpers;
using SisMot.Repositories;
using SisMot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbsisMotContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cookies configuration
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(cookie =>
    {
        cookie.LoginPath = "/Access/Login";
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMotelRepository, MotelService>();
builder.Services.AddScoped<IAccess, AccessService>();
builder.Services.AddScoped<IRequestRepository, RequestService>();
builder.Services.AddScoped<IRequestMotelRepository, RequestMotelService>();
builder.Services.AddScoped<IPhotoRepository, PhotoService>();   

var bingKey = builder.Configuration["BingKey"];
builder.Services.AddSingleton(new BingMap(bingKey));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}");

app.Run();
