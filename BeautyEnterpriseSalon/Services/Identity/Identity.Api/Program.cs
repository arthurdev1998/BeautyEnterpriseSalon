using System.Reflection;
using Identity.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddServicesInjection(builder.Configuration);


builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

app.AddSwaggerConfiguration();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ProxyMiddleware>(); // Registrar o middleware
app.Run();