using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

using Serilog;

using YFCore.Api.Middlewares.Global;
using YFCore.Application.Category.Queries.GetAllCategories;
using YFCore.Application.Contracts;
using YFCore.Application.ProcedureTypes.Contracts;
using YFCore.Domain.Categories.Repository;
using YFCore.Domain.ProcedureTypes.Repository;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Categories;
using YFCore.Infraestructure.Repository.ProcedureTypes;

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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProcedureTypeRepository, ProcedureTypeRepository>();
builder.Services.AddScoped<IProcedureTypeRead, ProcedureTypeRead>();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();
await app.RunAsync();

