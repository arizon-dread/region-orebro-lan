using clientside.backend.DIHelper;
using clientside.backend.Service;
using clientside.backend.Settings;
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
builder.Services.AddConfiguration<ServiceSettings>(builder.Configuration);
builder.Services.AddHostedService<PollService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "cors",
                      policy =>
                      {
                          policy.WithOrigins(["http://localhost:4200", "https://localhost:7089"]).WithMethods([HttpMethods.Get, HttpMethods.Post]);
                      });
});
builder.Services.AddControllers();
builder.Services.AddServices();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();
app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");

});

app.UseHttpsRedirection();
app.UseCors("cors");
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
