using AsAbstract;

namespace AsBasic;

public class DiscontinuousStream<T>: IDataStream{
    public string Usage { get; set; } = String.Empty;
    public void Add(T value){
        
    }
}