using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IBKS.DataAccess;

public static class DataAccessModule
{
    public static void ConfigureServices(IServiceCollection builder, IConfiguration configuration)
    {
        builder.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString,
                        x => x.MigrationsAssembly("IBKS.DataAccess")));
    }

    public static void ConfigureMiddleware(this IApplicationBuilder app)
    {
    }
}
