namespace AsAbstract;

public delegate void OnReceiveHandler(IDataAdapter sender, IDataPacket packet);
public interface IDataAdapter: IIdObject{
    string TypeName{get;}
    void SubscribeReceiveEvent(OnReceiveHandler handler);
    void UnsubscribeReceiveEvent(OnReceiveHandler handler);
    void Receive(IDataPacket packet);
    bool FixSampleFrequency{get;}
    System.Type DataType{get;}
}