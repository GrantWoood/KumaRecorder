using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService
{
    bool Configure(IConfigurationRoot? configurationRoot);
    List<IIoDevice> GetIoDevices();
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}