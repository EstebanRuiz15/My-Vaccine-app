
using FluentValidation.AspNetCore;
using My_vaccine_app.Configurations;
using My_vaccine_app.Configurations.Inyections;
using My_vaccine_app.Services.Contracts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SetRecordInyections();
builder.Services.SetFamilyGroupInyections();
builder.Services.SetUsersInyections();
builder.Services.SetCategoriesInyections();
builder.Services.SetVaccineInyections();
builder.Services.SetAllergyInyections();
builder.Services.SetDependenceInyections();
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())) ;
builder.Services.SetDatabaseConfig();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetMyVaccineAuthConfig();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
