using AsAbstract;
namespace AsBasic;

public class AnalogInput: IIoChannel
{
    public ContinuousValueStream<double> InputStream = new ContinuousValueStream<double>();
    public required ICalibrater Calibrater{ get; set; }
    public required AnalogPort IoPort { get; set; }

}