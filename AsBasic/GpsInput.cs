using System.Security.Cryptography.X509Certificates;
using AsAbstract;

namespace AsBasic;

public class GpsInput: IIoChannel
{
    public ValueAdapter<Location> Location = new ValueAdapter<Location>();
    public ValueAdapter<double> Speed = new ValueAdapter<double>();
    public required IIoPort IoPort { get; set; }
    public required IIoDevice IoDevice { get; set; }
    public string Name{get;set;} = string.Empty;
    public List<IDataAdapter> GetInputAdapters(){
        return [Location, Speed];
    }
    public List<IDataAdapter> GetOutputAdapters(){
        return [];
    }
}