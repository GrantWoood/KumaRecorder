namespace AsAbstract;

public interface IIoChannel
{
    IIoDevice IoDevice{ get; }
    IIoPort IoPort { get; }
    string Name { get; set;}
    string Id{get;set;}
    string FullId{get;}
    string TypeName{get;}
    bool Enabled{get;set;}

    public List<IDataAdapter> GetInputAdapters();
    public List<IDataAdapter> GetOutputAdapters();
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
}