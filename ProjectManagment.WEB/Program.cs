using Microsoft.EntityFrameworkCore;
using ProjectManagment.BLL.Interfaces;
using ProjectManagment.BLL.Services;
using ProjectManagment.DAL.Contexts;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;
using ProjectManagment.DAL.Repositories;
using ProjectManagment.WEB.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
});

builder.Services.AddScoped<IEmployeeRepository,EmployeesRepository>();
builder.Services.AddScoped<ICompanyRepository,CompaniesRepository>();
builder.Services.AddScoped<IProjectRepository,ProjectsRepository>();

builder.Services.AddScoped<IService<Employee>, EmployeesService>();
builder.Services.AddScoped<IService<Company>, CompaniesService>();
builder.Services.AddScoped<IService<Project>, ProjectsService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(c => c.AddProfile(new ProjectManagmentProfile()));

builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Project}/{action=Index}/{id?}");

app.Run();
