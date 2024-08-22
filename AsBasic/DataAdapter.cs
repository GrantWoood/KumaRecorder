using AsAbstract;

namespace AsBasic;
public class DataAdapter: IDataAdapter{
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
