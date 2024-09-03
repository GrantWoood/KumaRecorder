using System.Security.Cryptography.X509Certificates;
using AsAbstract;

namespace AsBasic;

public class GpsInput: IIoChannel
{
    public DataAdapter Location = new DataAdapter(){
        DataType = typeof(Location),
    };
    public DataAdapter Speed = new DataAdapter(){
        DataType = typeof(double),
    };
    public DataAdapter? _raw;
    public DataAdapter? Raw{
        get=> _raw;
        set{
            if(_raw!= null){
                var ac = _raw.OnReceiveAction();
                ac -= OnReceiveRawPacket;
            }
            _raw = value;
            if(_raw!= null){
                var ac = _raw.OnReceiveAction();
                ac += OnReceiveRawPacket;
            }
        }
    }

    public required IIoPort IoPort { get; set; }
    public required IIoDevice IoDevice { get; set; }
    public string Name{get;set;} = string.Empty;
    public List<IDataAdapter> GetInputAdapters(){
        return [Location, Speed];
    }

    public List<IDataAdapter> GetOutputAdapters(){
        return [];
    }

    private void OnReceiveRawPacket(IDataPacket packet){

    }

    public bool LoadProfile(IBundle? configuration){
        return true;
    }
    public bool SaveProfile(IBundle configuration){
        return true;
    }
}