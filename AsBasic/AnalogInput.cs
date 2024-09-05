using System.Data.Common;
using System.Runtime.CompilerServices;
using AsAbstract;
namespace AsBasic;

public class AnalogInput: IIoChannel
{
    public required IIoDevice IoDevice { get; set; }
    public required IIoPort IoPort { get; set; }
    private IDataAdapter? _rawAdapter = null;
    public required IDataAdapter? RawAdapter{
        get=>_rawAdapter;
        set{
            if(_rawAdapter != null){
                var action = _rawAdapter.OnReceiveAction();
                action -= OnReceiveRawPacket;
            }
            _rawAdapter = value;
            if(_rawAdapter != null){
            var action = _rawAdapter.OnReceiveAction();
            action += OnReceiveRawPacket;
            }
        }
    }
    public required ICalibrater Calibrater{ get; set; }
    public required IDataAdapter InputAdapter{get;set; }
    
    public string Name{get;set;} = string.Empty;

    public string Id{get;set;} = string.Empty;
    public string FullId{get{
        return $"{IoDevice.FullId}.{Id}";
    }}

    void OnReceiveRawPacket(IDataPacket packet){
        //根据格式转换为需要的格式
        double[] values = packet.AsDoubleArray();
        for (int i = 0; i < values.Length; ++i)
        {
            values[i] = Calibrater.Convert((double)values[i]);
        }
        var dataPacket = new DoubleArrayDataPacket()
        {
            Data = values,
            SyncKey = packet.SyncKey,
            TimeStamp = packet.TimeStamp,
        };
        InputAdapter.Receive(dataPacket);
    }

    public List<IDataAdapter> GetInputAdapters(){
        return [InputAdapter];
    }
    public List<IDataAdapter> GetOutputAdapters(){
        return [];
    }
    public bool LoadProfile(IBundle? configuration){
        return true;
    }
    public bool SaveProfile(IBundle configuration){
        return true;
    }
}