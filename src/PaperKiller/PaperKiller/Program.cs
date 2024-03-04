using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaperKiller.Controllers;
using PaperKiller.Models.items;
using PaperKiller.Repository;
using PaperKiller.Services;
using PaperKiller.Utils;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        var app = builder.Build();
        Configure(app);
        // первый порт
        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        builder.Services.Configure<AppSettings>(configuration.GetSection("ConnectionStrings"));

        // Регистрация сервисов
        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaperKiller", Version = "v1" });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Summary.xml"));

            // Определение схемы безопасности Bearer
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // Указание, что операции требуют аутентификации
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:44380",
                ValidAudience = "https://localhost:44380",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsIlBhc3N3b3JkIjoiMSIsImV4cCI6MTY5OTgzMzQ5NCwiaXNzIjoiaHR0cHM6Ly9zZXJ2ZXIuY29tIiwiYXVkIjoiaHR0cHM6Ly9jbGllbnRhcHAuY29tIn0.mVkQcJ-xIJfZPaPJjEVyD1vEk-_tbRLOURn6QgiFGr4"))
            };
        });


        // Регистрация классов
        builder.Services.AddScoped<IItems, Items>();

        // Регистрация коннектора
        builder.Services.AddSingleton<Connector>();

        // Регистрация репозиториев
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<ILinenRepository, LinenRepository>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();

        // Регистрация контроллеров
        builder.Services.AddScoped<StudentController>();
        builder.Services.AddScoped<UserController>();
        builder.Services.AddScoped<AdminController>();

        //Регистрация мапперов
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        // Регистрация сервисов
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IRegisterService, RegisterService>();
        builder.Services.AddScoped<IExchangeService, ExchangeService>();
        builder.Services.AddScoped<IUserService, UserService>();
    }

    private static void Configure(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "PaperKiller v1");
        });

        app.UseStaticFiles();

        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
