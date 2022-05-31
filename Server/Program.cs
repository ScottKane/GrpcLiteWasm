using System.Net;
using GrpcLiteWasm.Contracts.Services;
using GrpcLiteWasm.Server.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Lite;
using ProtoBuf.Grpc.Server;

namespace GrpcLiteWasm.Server;

internal static class Program
{
    private static async Task Main() =>
        await WebHost.CreateDefaultBuilder()
            .UseStaticWebAssets()
            .ConfigureServices(services =>
            {
                // services.AddCors();
                services.AddRazorPages();
                // services.AddControllers();
                services.AddCodeFirstGrpc();
                services.AddSingleton<ITestService, TestService>();
                
                services.AddSingleton(provider =>
                {
                    var server = new LiteServer();
                    server.ServiceBinder.AddCodeFirst(provider.GetRequiredService<ITestService>());
                    
                    return server;
                });
            })
            .UseKestrel(options =>
            {
                options.Listen(new IPEndPoint(IPAddress.Loopback, 5000));
                options.Listen(new IPEndPoint(IPAddress.Loopback, 5001), o => o.UseHttps());
                options.Listen(new IPEndPoint(IPAddress.Loopback, 6000), o => o.UseConnectionHandler<GrpcTcpHandler>());
            })
            .Configure((_, app) =>
            {
                // app.UseCors();
                app.UseBlazorFrameworkFiles();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapRazorPages();
                        endpoints.MapGrpcService<TestService>();
                        // endpoints.MapControllers();
                        endpoints.MapFallbackToFile("index.html");
                    });
                // app.UseEndpoints(endpoints =>
                // {
                //     endpoints.MapGrpcService<TestService>();
                // });
            })
            .Build()
            .RunAsync();
}