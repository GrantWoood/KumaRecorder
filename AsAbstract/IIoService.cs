using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService
{
    string Name{get;}
    string Id{get;}
    bool Configure(IConfiguration? configuration);
    bool LoadProfile(IConfiguration? configuration);
    List<IIoDevice> GetIoDevices();
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}