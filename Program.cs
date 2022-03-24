using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApiTest.Data;
using Microsoft.OpenApi.Models;

// public class Program
// {
// 	public static void Main(string[] args)
// 	{
// 		
// 	}
// }
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
														options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	   .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
	// Retrieve project name dynamically look in properties of project and the xmldocumentation path
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xPath   = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xPath, true);

	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title          = "ApiTest",
		Version        = "v1",
		Description    = "ApiTest",
		TermsOfService = new Uri("https://example.com/terms"),
		Contact = new OpenApiContact
		{
			Name  = "ApiTest",
			Email = "qkl@Senia.dk",
			Url   = new Uri("https://google.com/terms"),
		},
		License = new OpenApiLicense
		{
			Name = "Use under LICX",
			Url  = new Uri("https://example.com/license"),
		},
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");

	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	c.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
					   name : "default",
					   pattern : "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();