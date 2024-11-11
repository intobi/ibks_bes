using IBKS.Core.Interfaces;
using IBKS.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IBKS.Core;

public static class CoreModule
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<ITicketService, TicketService>();
        services.AddTransient<IReplyService, ReplyService>();
    }
}