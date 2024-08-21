using AsAbstract;

namespace AsBasic;

public class DataPacket<T> : IDataPacket{
    public T Data{get;set;}
    public string SyncKey{get;set; } = string.Empty;
    public long TimeStamp{get;set;} = 0;
}