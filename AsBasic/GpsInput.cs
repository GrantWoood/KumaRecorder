using System.Security.Cryptography.X509Certificates;
using AsAbstract;

namespace AsBasic;

public class GpsInput: IIoChannel
{
    public DiscontinuousStream<Location> Location = new DiscontinuousStream<Location>();
    public DiscontinuousStream<double> Speed = new DiscontinuousStream<double>();

    public required GpsPort IoPort{get;set;  }
}