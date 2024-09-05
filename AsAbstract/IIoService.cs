using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService
{
    string Name{get;}
    string Id{get;set;}
    bool Configure(IConfiguration? configuration);
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
    List<IIoDevice> GetIoDevices();
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}