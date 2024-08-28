namespace AsAbstract;

public interface IIoService
{
    bool Configure(IConfiguration? configuratio);
    List<IIoDevice> GetIoDevices();
    List<IIoChannel> GetIoChannels();
    List<IDataAdapter> GetInputAdapters();
    bool StartSample();
    bool StopSample();
}