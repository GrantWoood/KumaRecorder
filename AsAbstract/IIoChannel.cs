namespace AsAbstract;

public interface IIoChannel
{
    IIoDevice IoDevice{ get; }
    IIoPort IoPort { get; }
    string Name { get; set;}
    List<IDataStream> GetInputStreams();
}