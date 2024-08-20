namespace AsAbstract;

public interface IIoChannel
{
    IIoDevice IoDevice{ get; }
    IIoPort IoPort { get; }
    string Name { get; set;}

    public List<IDataAdapter> GetInputAdapters();
    public List<IDataAdapter> GetOutputAdapters();
}