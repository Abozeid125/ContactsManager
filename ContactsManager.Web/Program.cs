using Entities;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Repositeries;
using RepositeryContracts;
using Rotativa.AspNetCore;
using ServiceContracts;
using Services;
using Serilog.AspNetCore;
using Serilog;
using CRUDexample.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);



builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider provider, LoggerConfiguration loggerConfiguration) =>
{
	loggerConfiguration.ReadFrom.Configuration(context.Configuration)
	//readconfiguration settings from built-in iIConfiguration like appsettings .jsoon
	.ReadFrom.Services(provider);
//make services available to serilog

});

var app = builder.Build();


Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.UseStaticFiles();
app.MapControllers();



app.UseAuthentication();
app.UseAuthorization();

if(builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.Run();
