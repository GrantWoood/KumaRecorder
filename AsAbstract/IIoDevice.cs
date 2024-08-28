namespace AsAbstract;

public interface IIoDevice
{
    string Name { get; set;}
    bool Configure(IConfiguration? configurationSection);
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}