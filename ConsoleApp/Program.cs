using AsCore;
using ConsoleApp;
using DemoService;
using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(
    builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("AsConsole");

AsApplication application = new AsApplication()
{
    IoServiceFactory = new IoServiceFactory(logger),
};
application.Configure(null);


application.StartSample();
var counter = 10;
while (counter > 0)
{
    Console.WriteLine($"Sample running {counter}...");
    Thread.Sleep(1000);
    --counter;
}
application.StopSample();
Console.WriteLine("Exit.");