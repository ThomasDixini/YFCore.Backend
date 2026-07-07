using System;
using System.Text;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Scalar.AspNetCore;

using Serilog;

using YFCore.Api.Middlewares.Global;
using YFCore.Api.Services;
using YFCore.Application.Categories.Queries.GetAllCategories;
using YFCore.Application.Contracts;
using YFCore.Application.ProcedureTypes.Commands.CreateProcedureType;
using YFCore.Application.ProcedureTypes.Contracts;
using YFCore.Application.Shared.Validators;
using YFCore.Domain.AppointmentRepository;
using YFCore.Domain.Categories.Repository;
using YFCore.Domain.ProcedureTypes.Repository;
using YFCore.Domain.ProductRepository;
using YFCore.Domain.Users.Entity;
using YFCore.Domain.Users.Repository;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Appointments;
using YFCore.Infraestructure.Repository.Categories;
using YFCore.Infraestructure.Repository.ProcedureTypes;
using YFCore.Infraestructure.Repository.Products;
using YFCore.Infraestructure.Repository.Users;
using YFCore.Infraestructure.Services.Notification;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.RegisterServicesFromAssemblyContaining<GetAllCategoriesQuery>();
});
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("YFCore.Infraestructure")
    );
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateProcedureTypeCommandValidator>();
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProcedureTypeRepository, ProcedureTypeRepository>();
builder.Services.AddScoped<IProcedureTypeRead, ProcedureTypeRead>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "YFCore";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "YFCore";
var jwtExpiresInMinutes = builder.Configuration.GetValue<int?>("Jwt:ExpiresInMinutes") ?? 60;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();
await app.RunAsync();

