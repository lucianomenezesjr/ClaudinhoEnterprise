using Microsoft.EntityFrameworkCore;
using ClaudinhoEnterpriseApp;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(); // antes do builder.Build()
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ClaudinhoEnterpriseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

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

app.UseSession(); // logo após app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
