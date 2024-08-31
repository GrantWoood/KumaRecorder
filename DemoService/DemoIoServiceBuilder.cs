using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoService;

public class DemoIoServiceBuilder(ILogger logger, SyncManager syncManager): IIoServiceBuilder{
    public string Id{ get{return DemoIoService.Guid;}}
    public IIoService Build(IConfiguration? configuration){
        var ioService = new DemoIoService(logger, syncManager);
        if(!ioService.Configure(configuration)){
            logger.LogError($"Configure failed for io servie {ioService.Name}!");
        }
        return ioService;
    }
}