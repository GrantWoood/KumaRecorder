using System.Security.Cryptography.X509Certificates;
using AsAbstract;

namespace AsBasic;

public class GpsInput: IIoChannel
{
    public DiscontinuousStream<Location> Location = new DiscontinuousStream<Location>(){
        Usage = Usage.Location
    };
    public DiscontinuousStream<double> Speed = new DiscontinuousStream<double>(){
        Usage = Usage.Speed
    };
    public required IIoPort IoPort { get; set; }
    public required IIoDevice IoDevice { get; set; }
    public string Name{get;set;} = string.Empty;
    public List<IDataStream> GetInputStreams(){
        return [Location, Speed];
    }
}