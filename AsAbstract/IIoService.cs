using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService
{
    string Name{get;}
    string Id{get;}
    bool Configure(IConfiguration? configuration);
    bool LoadProfile(ITestProfile? configuration);
    bool SaveProfile(ITestProfile configuration);
    List<IIoDevice> GetIoDevices();
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}