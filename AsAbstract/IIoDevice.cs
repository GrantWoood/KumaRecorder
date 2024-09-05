using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoDevice
{
    string Name { get; set;}
    string Id{get;set;}
    string FullId{get;}
    bool Configure(IConfiguration? configuration);
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
}