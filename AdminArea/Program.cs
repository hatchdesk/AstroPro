using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.Options;
using Web.Application.Services;
using Web.Infrastructure;
using Web.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IArticleRepository , ArticleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();


builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IConsultationService, ConsultationService>();
builder.Services.AddScoped<IPageContentService, PageContentService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IArticleService , ArticleService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<EmailSettingsOption>(builder.Configuration.GetSection("EmailSettings"));

//var connectionString = builder.Configuration.GetConnectionString("WebPortal");
//builder.Services.AddDbContext<WebDbContext>(options => 
//    {
//        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//    });


builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlServer(


        builder.Configuration.GetConnectionString("astroPortal"),
        b => b.MigrationsAssembly("Web.Infrastructure")
    )
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Home/Index";
        options.AccessDeniedPath = "/Forbidden/";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
   name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
