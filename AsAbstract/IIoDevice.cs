using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoDevice
{
    string Name { get; set;}
    bool Configure(IConfiguration? configuration);
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}