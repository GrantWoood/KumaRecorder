using AsAbstract;
namespace AsBasic;

public class AnalogInput: IIoChannel
{
    public ContinuousValueStream<double> InputStream = new ContinuousValueStream<double>();
    public required ICalibrater Calibrater{ get; set; }
    public required IIoPort IoPort { get; set; }
    public required IIoDevice IoDevice { get; set; }
    public string Name{get;set;} = string.Empty;
    public List<IDataStream> GetInputStreams(){
        return [InputStream];
    }

}