namespace AsAbstract;

public interface IIoChannel
{
    IIoDevice IoDevice{ get; }
    IIoPort IoPort { get; }
    string Name { get; set;}
    string Id{get;set;}
    string FullId{get;}

    public List<IDataAdapter> GetInputAdapters();
    public List<IDataAdapter> GetOutputAdapters();
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
}