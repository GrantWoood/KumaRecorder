using AsAbstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoService;

public class DemoIoService(ILogger logger): IIoService
{
    private readonly DemoIoDevice _device = new DemoIoDevice(logger);
    public bool Configure(IConfigurationRoot? configurationRoot)
    {
        logger.LogInformation("Configure demo io service");
        _device.Configure(configurationRoot?.GetSection("device1"));
        return true;
    }

    public bool StartSample()
    {
        logger.LogInformation("Demo io service is running");
        _device.StartSample();
        return true;
    }

    public bool StopSample()
    {
        _device.StopSample();
        logger.LogInformation("Demo io service stopped");
        return true;
    }
}