using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebAPI;
using WebAPI.BusinessLogicLayer.Services.LeagueService;
using WebAPI.BusinessLogicLayer.Services.ManagerService;
using WebAPI.BusinessLogicLayer.Services.PlayerService;
using WebAPI.BusinessLogicLayer.Services.RegionService;
using WebAPI.BusinessLogicLayer.Services.TeamService;
using WebAPI.Domain.Models;
using WebAPI.DataAccessLayer;
using WebAPI.DataAccessLayer.Repositories.LeagueRepository;
using WebAPI.DataAccessLayer.Repositories.ManagerRepository;
using WebAPI.DataAccessLayer.Repositories.PlayerRepository;
using WebAPI.DataAccessLayer.Repositories.RegionRepository;
using WebAPI.DataAccessLayer.Repositories.TeamRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerService, ManagerService>();

builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
builder.Services.AddScoped<ILeagueService, LeagueService>();

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IRegionService, RegionService>();

// Настройка маппера
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Настройка для JSON десериализации - в десериализации не участвуют null поля
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Игнор зацикленной десериализации
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Register IdentityDbContext
var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => 
    options.UseMySql(
        databaseConnectionString, 
        ServerVersion.AutoDetect(databaseConnectionString), 
        b => b.MigrationsAssembly("WebAPI.DataAccessLayer"))
    );

builder.Services.AddAuthorization();
    
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<ApplicationUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
