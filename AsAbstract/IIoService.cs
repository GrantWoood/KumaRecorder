using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService
{
    bool Configure(IConfigurationRoot? configurationRoot);
    List<IIoChannel> GetInputChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}