using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moor.API.Filters;
using Moor.API.Middlewares;
using Moor.API.Modules;
using Moor.Core.Services.MoorService;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Moor.Repository;
using Moor.Service.Mapping;
using Moor.Service.Services.MoorService;
using Moor.Service.Utilities.AppSettings;
using Moor.Service.Utilities.AuthorizeHelpers;
using Moor.Service.Validations;
using Sieve.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CarDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));
builder.Services.AddScoped<BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm>>();

builder.Services.AddDbContext<AppDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(builder.Configuration.GetConnectionString("SqlConnection"), serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());





//Defaul AppSettings
var appSettingsSection = builder.Configuration.GetSection("MoorSettings");
builder.Services.Configure<MoorSettings>(appSettingsSection);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Default
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containersBuilder => containersBuilder.RegisterModule(new RepoServiceModule()));


builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<SessionManager>();
builder.Services.AddScoped(typeof(TokenHelper));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();


app.UseCustomException();
app.UseCustomAuthMiddleware();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(options => 
 options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.MapControllers();

app.Run();
