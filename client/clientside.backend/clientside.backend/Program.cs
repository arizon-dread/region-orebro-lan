using clientside.backend.DIHelper;
using Microsoft.EntityFrameworkCore;
using RolDbContext;
var builder = WebApplication.CreateBuilder(args);
var dbhelper = new DbHelper();
dbhelper.Init(builder.Configuration.GetConnectionString("Sqlite"));
// Add services to the container.
builder.Services.AddDbContext<RolDbContext.RolEfContext>(d =>
{
    var connection = builder.Configuration.GetConnectionString("Sqlite");
    d.UseSqlite(connection);
});

builder.Services.AddControllers();
builder.Services.AddServices();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
