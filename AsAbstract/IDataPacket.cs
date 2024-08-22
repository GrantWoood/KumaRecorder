namespace AsAbstract;

public interface IDataPacket{
    string SyncKey { get; }
    long TimeStamp { get; }
    double[] AsDoubleArray();
}