namespace AsAbstract;


public interface IDataAdapter{
    string Name{get;}
    string TypeName{get;}
    Action<IDataPacket> OnReceiveAction();
    void Receive(IDataPacket packet);
    bool FixSampleFrequency{get;}
    System.Type DataType{get;}
}