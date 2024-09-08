using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoDevice : IIdObject
{
    bool Configure(IConfiguration? configuration);
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
}