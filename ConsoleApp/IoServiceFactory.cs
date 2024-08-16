using AsAbstract;
using DemoService;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class IoServiceFactory(ILogger logger): IIoServiceFactory
{
    public IIoService? Create(string id)
    {
        return new DemoIoService(logger);
    }
}