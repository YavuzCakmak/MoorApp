using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moor.API.Filters;
using Moor.API.Middlewares;
using Moor.API.Modules;
using Moor.Repository;
using Moor.Service.Mapping;
using FluentValidation.AspNetCore;
using Moor.Service.Validations;
using Moor.Service.Utilities.AppSettings;
using Moor.Core.Services.MoorService;
using Moor.Service.Services.MoorService;
using Moor.Service.Utilities.AuthorizeHelpers;
using Moor.Service.Utilities.Session;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

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

app.MapControllers();

app.Run();
