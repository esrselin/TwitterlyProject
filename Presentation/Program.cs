using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Core.Interfaces;
using Core.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TwitterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TwitterlyDb")));

builder.Services.AddDefaultIdentity<TwitterUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // account doðrulamasýný devre dýþý býrak
    options.SignIn.RequireConfirmedEmail = false; // email doðrulamasýný devre dýþý býrak
})
    .AddEntityFrameworkStores<TwitterDbContext>();

// Servisler
builder.Services.AddScoped<ITweetService, TweetService>();
builder.Services.AddScoped<ITwitterUserService, TwitterUserService>();
builder.Services.AddScoped<IUserFollowService, UserFollowService>();




builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.MapRazorPages();

app.Run();
