using AsAbstract;
using AsBasic;
using DemoService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class IoServiceFactory(ILogger logger, SyncManager syncManager): IIoServiceFactory
{
    private IConfiguration? _configuration = null;
    private readonly List<IIoServiceBuilder> _builders = [
        new DemoIoServiceBuilder(logger,syncManager),
    ];
    public IIoService? Create(string id)
    {
        foreach (var builder in _builders){
            if(builder.Id == id){
                return builder.Build(_configuration?.GetSection(id));
            }
        }
        if(id.Length == 0){
            //return the first by default
            return _builders.First().Build(null);
        }
        return null;
    }

    public bool Configure(IConfiguration? configuration){
        //Load default setting from configuration
        _configuration = configuration;
        return true;
    }
}