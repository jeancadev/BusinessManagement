using BusinessManagement.Application;
using BusinessManagement.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using BusinessManagement.Infrastructure.Persistence;
using BusinessManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.WebApi.Filters;
using BusinessManagement.WebApi.Middlewares;
using Microsoft.OpenApi.Models;
using Serilog;
using BusinessManagement.Application.Common.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// 1) Se obtiene la clave secreta de prueba, desde la config
var jwtKey = builder.Configuration["JwtSettings:Secret"] ?? "ClaveUltraSecreta12345";

// 2) Registra el esquema de autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Solo en desarrollo
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        // Convertir la clave a bytes y la uso para firmar
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true // para  manejar expiraciones
    };
});

// 3) Se agrega la política de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ITDepartmentOnly", policy =>
    {
        policy.RequireClaim("Department", "IT");
    });
});

// Configurar Serilog
builder.Host.UseSerilog((ctx, lc) =>
{
    lc
      .ReadFrom.Configuration(ctx.Configuration)
      .WriteTo.Console()
      .Enrich.FromLogContext();
});

// Registrar DbContext con SQL Server
builder.Services.AddDbContext<BusinessManagementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Registrar Repositorios
builder.Services.AddScoped<IProductRepository, ProductRepository>();    
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registro de MediatR
builder.Services.AddMediatR(typeof(AssemblyMarker).Assembly);
    
// Registrar los Validators
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);
// Registrar el Pipeline behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingresa 'Bearer {tu token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BusinessManagement API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Registro del middleware global de excepciones
app.UseMiddleware<GlobalExceptionMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BusinessManagementDbContext>();
    dbContext.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BusinessManagementDbContext>();

    // Aplica migraciones
    context.Database.Migrate();

    // Llama al seeding
    DbInitializer.Seed(context);
}

app.Run();
