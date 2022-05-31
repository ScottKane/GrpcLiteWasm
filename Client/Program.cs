using System.Globalization;
using System.Net;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GrpcLiteWasm.Client;
using GrpcLiteWasm.Contracts.Services;
using MudBlazor.Services;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Lite;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

// var channel = await ConnectionFactory
//     .ConnectSocket(new IPEndPoint(IPAddress.Loopback, 6000))
//     .AsStream()
//     .AsFrames()
//     .CreateChannelAsync();
// builder.Services.AddSingleton(channel);
//
// var service = channel.CreateGrpcService<ITestService>();
//
// Console.WriteLine(service.Echo("Creating subscription"));
//         
// var requests = Observable
//     .Interval(TimeSpan.FromSeconds(1))
//     .Select(_ => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
//
// requests.Subscribe(request => Console.WriteLine($"[Client]: {request}"));
//                     
// service.Subscribe(requests)
//     .Subscribe(response => Console.WriteLine($"[Server]: {response}"));
//
// Console.WriteLine(service.Echo("Subscription created"));

await builder.Build().RunAsync();