using Asp.Versioning;
using AspNetCoreRateLimit;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SearchEngine.Web.Application.Authentication;
using SearchEngine.Web.Application.Configurations;
using SearchEngine.Web.Application.Extensions;
using SearchEngine.Web.Application.MappingProfiles;
using SearchEngine.Web.Application.Services.Implementations;
using SearchEngine.Web.Application.Services.Interfaces;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Domain.Interfaces.Services;
using SearchEngine.Web.Infrastructure.Persistance;
using SearchEngine.Web.Infrastructure.Repositories;
using SearchEngine.Web.Infrastructure.Services.SearchService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHosts", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://https://searchenginewebapi-fd-assessment-baa0fmf6c4ccdre5.eastus2-01.azurewebsites.net")  // Allow requests from Angular app
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers();

builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights")
});

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Add DbContext
builder.Services.AddDbContextFactory<FlightsDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightsDB"));
});

var azureSearchConfig = new AzureSearchConfiguation();
builder.Configuration.GetSection(AzureSearchConfiguation.ConfigurationKey).Bind(azureSearchConfig);

builder.Services.AddScoped<IAirportSearchService>(provider =>
    new AirportSearchService(azureSearchConfig.AirportsIndexName, azureSearchConfig.Endpoint, azureSearchConfig.AdminKey));

builder.Services.AddScoped<IAirlineSearchService>(provider =>
    new AirlineSearchService(azureSearchConfig.AirlinesIndexName, azureSearchConfig.Endpoint, azureSearchConfig.AdminKey));

// Add repositories
builder.Services.AddScoped<IAirportsRepository, AirportsRepository>();
builder.Services.AddScoped<IAirlinesRepository, AirlinesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();

// Add services
builder.Services.AddScoped<IAirportsService, AirportsService>();
builder.Services.AddScoped<IAirlinesService, AirlinesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ISearchHistoryService, SearchHistoryService>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AirportProfile), typeof(AirlineProfile));
builder.Services.AddAutoMapper(typeof(AirlineProfile), typeof(AirlineProfile));
builder.Services.AddAutoMapper(typeof(SearchHistoryProfile), typeof(SearchHistoryProfile));
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(UserProfile));

// Add JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole(Role.Admin.ToString()))
    .AddPolicy("UserOrAdmin", policy => policy.RequireRole(Role.User.ToString(), Role.Admin.ToString()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Search Engine", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddOpenApi();


var app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchEngine v1");
    options.OAuthAppName("Search Engine");
    options.OAuthUsePkce();
});

app.MapOpenApi();

app.UseStaticFiles();

// Configure SPA fallback
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        // Use Angular CLI proxy during development
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

// Configure the HTTP request pipeline
app.UseCors("AllowHosts");

app.UseHttpsRedirection();

app.InitializeDatabase();

app.UseIpRateLimiting();

app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();