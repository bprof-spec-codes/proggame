using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace proggame.BackEnd;

public class BackEndWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<BackEndWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
