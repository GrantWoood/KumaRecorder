using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoDevice
{
    string Name { get; set;}
    bool Configure(IConfigurationSection? configurationSection);
    List<IIoChannel> GetInputChannels();
    List<IDataStream> GetInputStreams();
    bool StartSample();
    bool StopSample();
}