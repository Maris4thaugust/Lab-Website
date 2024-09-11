using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Repository.ProjectRepository;
using WebSEnR.Models;
using WebSEnR.Interface.ProjectInterface;
using WebSEnR.Helper;
using WebSEnR.Interface;
using WebSEnR.Services;
using WebSEnR.Interface.AboutLabInterface;
using WebSEnR.Repository.AboutLabRepository;
using WebSEnR.Interface.ActivityInterface;
using WebSEnR.Repository.ActivityRepository;
using WebSEnR.Interface.NewsInterface;
using WebSEnR.Repository.NewsRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUniProjectRepository, UniProjectRepository>();
builder.Services.AddScoped<IMinisProjectRepository, MinisProjectRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IActivityRepository,ActivityRepository>();
builder.Services.AddScoped<INewsRepository,NewsRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));


builder.Services.AddDbContext<SErNDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
