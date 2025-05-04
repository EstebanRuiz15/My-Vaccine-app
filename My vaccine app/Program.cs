
using FluentValidation.AspNetCore;
using My_vaccine_app.Configurations;
using My_vaccine_app.Configurations.Inyections;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(8080);
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.MaxConcurrentUpgradedConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
});
// Add services to the container.
builder.Services.SetRecordInyections();
builder.Services.SetFamilyGroupInyections();
builder.Services.SetUsersInyections();
builder.Services.SetCategoriesInyections();
builder.Services.SetVaccineInyections();
builder.Services.SetAllergyInyections();
builder.Services.SetDependenceInyections();
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())) ;
builder.Services.SetDatabaseConfig(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetMyVaccineAuthConfig();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
       builder =>
       {
           builder
               .SetIsOriginAllowed(_ => true)
               .AllowAnyMethod()
               .AllowAnyHeader();
       });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
