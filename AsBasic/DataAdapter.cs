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
    private event OnReceiveHandler ReceiveData;
    public bool FixSampleFrequency{get;set;} = false;
    public required System.Type DataType{get;set;}

    public void SubscribeReceiveEvent(OnReceiveHandler handler){
        ReceiveData+=handler;
    }
    public void UnsubscribeReceiveEvent(OnReceiveHandler handler){
        ReceiveData-=handler;
    }
   
    public void Receive(IDataPacket packet){
        if(ReceiveData!=null)
        {
            ReceiveData(this, packet);
        }
    }
   
}
