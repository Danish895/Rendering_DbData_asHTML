using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.Formatters;
using static StudentAPI.Controllers.StudentDetailsController;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // using Microsoft.AspNetCore.Mvc.Formatters;
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
});

// Add services to the container.
builder.Services.AddMvc(options => options.OutputFormatters.Add(new HtmlOutputFormatter()));
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StudentContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDirectoryBrowser();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "HtmlRender")),
    RequestPath = "/StaticFiles",
    EnableDirectoryBrowsing = true
});

app.UseAuthorization();

app.MapControllers();

app.Run();
