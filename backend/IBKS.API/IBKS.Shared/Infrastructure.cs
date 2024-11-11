using AutoMapper;
using IBKS.Shared.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace IBKS.Shared;

public static class SharedModule
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IMapper>(serviceProvider => MyMapper.GetDefaultMapper());
    }
}
