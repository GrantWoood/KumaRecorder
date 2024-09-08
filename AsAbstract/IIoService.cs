using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoService: IIdObject
{
    bool Configure(IConfiguration? configuration);
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
    List<IIoDevice> GetIoDevices();
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    List<IDataAdapter> GetRawInputAdapters();
    bool StartSample();
    bool StopSample();
}