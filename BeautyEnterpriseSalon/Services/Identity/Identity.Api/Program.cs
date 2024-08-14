using Identity.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();