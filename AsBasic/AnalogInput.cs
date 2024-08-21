using System.Data.Common;
using AsAbstract;
namespace AsBasic;

public class AnalogInput: IIoChannel
{
    ValueAdapter<double[]> InputAdapter = new ValueAdapter<double[]>();
    public required ICalibrater Calibrater{ get; set; }
    public required IIoPort IoPort { get; set; }
    public required IIoDevice IoDevice { get; set; }
    public string Name{get;set;} = string.Empty;

    public List<IDataAdapter> GetInputAdapters(){
        return [InputAdapter];
    }
    public List<IDataAdapter> GetOutputAdapters(){
        return [];
    }
    
}