using System.Security.Cryptography.X509Certificates;

namespace AsAbstract;

public interface IIoChannel: IIdObject
{
    IIoDevice IoDevice{ get; }
    IIoPort IoPort { get; }
    string TypeName{get;}
    bool Enabled{get;set;}

    public List<IDataAdapter> GetInputAdapters();
    public List<IDataAdapter> GetOutputAdapters();
    public List<IDataAdapter> GetRawAdapters();
    bool LoadProfile(IBundle? configuration);
    bool SaveProfile(IBundle configuration);
    ISettings GetSettings();

}