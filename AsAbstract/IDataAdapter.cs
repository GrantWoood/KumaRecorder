namespace AsAbstract;


public interface IDataAdapter{
    Action<IDataPacket> OnReceiveAction();
    void Receive(IDataPacket packet);
    bool FixSampleFrequency{get;}
    System.Type DataType{get;}
}