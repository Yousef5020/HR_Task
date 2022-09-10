global using HR_Task.Interfaces;
using HR_Task.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IEmployeeData, HR_Task.Services.EmployeeData>();
builder.Services.AddScoped<IDepartmentData, HR_Task.Services.DepartmentData>();
builder.Services.AddScoped<IJobRankData, HR_Task.Services.JobRankData>();
builder.Services.AddScoped<ISalaryData, HR_Task.Services.SalaryData>();

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
