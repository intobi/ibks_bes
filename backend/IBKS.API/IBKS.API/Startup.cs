using IBKS.Core;
using IBKS.Core.Settings;
using IBKS.DataAccess;
using IBKS.Shared;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IBKS.API;

public class Startup
{
    private ConfigurationManager configuration;
    private const string _defaultCorsPolicyName = "CorsPolicy";
    private readonly IWebHostEnvironment _env;

    public Startup(ConfigurationManager configuration, IWebHostEnvironment env)
    {
        this.configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders |
                                    HttpLoggingFields.RequestBody |
                                    HttpLoggingFields.ResponseBody |
                                    HttpLoggingFields.ResponsePropertiesAndHeaders;
        });

        services.AddOptions().Configure<AppOptions>(configuration.GetSection("App"));

        services.AddHealthChecks();
        services.AddHttpContextAccessor();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //services.AddCors(options =>
        //{
        //    options.AddPolicy(name: _defaultCorsPolicyName,
        //                      builder =>
        //                      {
        //                          builder.SetIsOriginAllowed(origin => true)
        //                            .AllowAnyHeader()
        //                            .AllowAnyMethod()
        //                            .AllowCredentials();
        //                      });
        //});

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()  // Allows all origins
                           .AllowAnyHeader()  // Allows any headers
                           .AllowAnyMethod(); // Allows any HTTP method (GET, POST, etc.)
                });
        });

        CoreModule.ConfigureServices(services);
        DataAccessModule.ConfigureServices(services, configuration);
        SharedModule.ConfigureServices(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpLogging();

        app.UseCors("AllowAnyOrigin");
        //app.UseCors(_defaultCorsPolicyName);

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}
