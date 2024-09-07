using AsAbstract;

namespace AsBasic;
public class DataAdapter: IDataAdapter{
    public string CustomName{get;set;} = string.Empty;
    public string TypeName{get;set;} = String.Empty;
    public string Name{
        get{
            if(CustomName.Length == 0){
                return $"{TypeName}";
            }else{
                return CustomName;
            }
        }
    }
    private readonly Action<IDataPacket> _onRecvAction;
    public bool FixSampleFrequency{get;set;} = false;
    public required System.Type DataType{get;set;}
    public Action<IDataPacket> OnReceiveAction(){
        return _onRecvAction;
    }
    public void Receive(IDataPacket packet){
        _onRecvAction(packet);
    }
   
}
