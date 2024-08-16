using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService
{
    bool Configure(IConfigurationRoot? configurationRoot);
    bool StartSample();
    bool StopSample();
}