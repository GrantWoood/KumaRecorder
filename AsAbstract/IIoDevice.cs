using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoDevice
{
    bool Configure(IConfigurationSection? configurationSection);
    bool StartSample();
    bool StopSample();
}