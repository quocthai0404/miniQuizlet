using Microsoft.EntityFrameworkCore;
using miniQuizlet.Models;
using miniQuizlet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

//connect db
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DatabaseContext>(option => {
    option.UseLazyLoadingProxies().UseSqlServer(connectionString);
});
//
//khai bao service duoi ket noi db
builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
builder.Services.AddScoped<IMailService, MailServiceImpl>();
//

//cau hinh security
//



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action}"
);

app.Run();
