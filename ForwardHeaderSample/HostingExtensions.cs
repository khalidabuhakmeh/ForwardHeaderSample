using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Duende.IdentityServer.Services;

namespace ForwardHeaderSample;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                // Protocol that will be forwarded (http or https)
                ForwardedHeaders.XForwardedProto | 
                // The original host requested by the client.
                ForwardedHeaders.XForwardedHost;
            
            // limit this to known hosts
            options.AllowedHosts = ["*"]; // Only allow trusted hosts
            
            options.ForwardLimit = 1; // Adjust based on your proxy setup

            // (Optional) Specify trusted proxy network ranges
            // get rid of localhost and 127.0.0.1 variants
            options.KnownNetworks.Clear();
            //options.KnownProxies.Add(IPAddress.Parse("192.168.0.1")); // Example trusted proxy IP
        });

        var isBuilder = builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddTestUsers(TestUsers.Users)
            .AddLicenseSummary();

        // in-memory, code config
        isBuilder.AddInMemoryIdentityResources(Config.IdentityResources);
        isBuilder.AddInMemoryApiScopes(Config.ApiScopes);
        isBuilder.AddInMemoryClients(Config.Clients);

        builder.Services.AddAuthentication();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseForwardedHeaders();
        
        app.Use((context, next) =>
        {
            var serverUrls = context.RequestServices.GetRequiredService<IServerUrls>();
            return next(context);
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}