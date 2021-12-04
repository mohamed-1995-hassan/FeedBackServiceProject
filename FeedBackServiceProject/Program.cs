using FeedBackServiceProject.Core.Interfaces.Repositories;
using FeedBackServiceProject.Core.Interfaces.Services;
using FeedBackServiceProject.Core.Services;
using FeedBackServiceProject.Infrastructure.Context;
using FeedBackServiceProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FeedBackServiceProject.Api;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using FeedBackServiceProject.Api.Middlewares;

string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json",false,reloadOnChange: true);

if(currentEnvironment?.Equals("Development",StringComparison.OrdinalIgnoreCase)==true)
{
    configurationBuilder.AddJsonFile($"appsettings.{currentEnvironment}.json",optional:false);
}
IConfigurationRoot configurationRoot = configurationBuilder.Build();
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging => {
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
}).UseNLog();
LogManager.Configuration = new NLogLoggingConfiguration(configurationRoot.GetSection("NLog"));
Logger logger = LogManager.GetCurrentClassLogger();
try
{
    logger.Info($"{ApiConstants.FriendlyServiceName} start running");
    logger.Info($"{ApiConstants.FriendlyServiceName} stop running");


    // Add services to the container.
    builder.Services.AddDbContext<FeedBackServiceDbContext>(x => x.UseSqlServer(configurationRoot.GetConnectionString("DefaultConn")));

    builder.Services.ConfigureCors();
    builder.Services.ConfigureDependencyInjection();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddDbContext<FeedBackServiceDbContext>(options =>
           options.UseSqlServer("DefaultConn"));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.configreSwager();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddRouting(c => c.LowercaseUrls = true);
    

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        
        app.configreSwager();
        app.UseHttpCodeAndLogMiddleWare();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    
}
catch (Exception ex)
{
    logger.Error(ex);
}

