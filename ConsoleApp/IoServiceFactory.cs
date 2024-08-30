using AsAbstract;
using AsBasic;
using DemoService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class IoServiceFactory(ILogger logger, SyncManager syncManager): IIoServiceFactory
{
    public IIoService? Create(string id)
    {
        return new DemoIoService(logger, syncManager);
    }

    public bool Configure(IConfiguration? configuration){
        //Load default setting from configuration
        return true;
    }
}