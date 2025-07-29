using EmployeeProject;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Employee}/{action=Index}/{id?}");
    //pattern: "{controller=Accounts}/{action=Login}/{id?}");
    pattern: "{controller=Company}/{action=Login}/{id?}");
app.Run();
