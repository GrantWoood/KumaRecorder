using AsAbstract;
using AsBasic;
namespace AsCore;

/// <summary>
/// 统一管理所有数据流，并对Id和名称进行分配
/// </summary>
public class StreamManager{
    private readonly Dictionary<string, List<IStream>> _streams = new Dictionary<string, List<IStream>>();
    public void OnIoServicesUpdated(AsApplication application){
        foreach(var service in application.IoServices){
            foreach(var adapter in service.GetInputAdapters()){
                var typename = adapter.TypeName;
                if(!_streams.ContainsKey(typename)){
                    _streams[typename] = new List<IStream>();
                }
                var list = _streams[typename]!;
                var stream = new DataStream(){
                    Classification =  typename,
                    Name = $"{typename}{list.Count+1}"
                };
                list.Add(stream);
            }
        }
    }

    public Dictionary<string, List<IStream>> Streams=>_streams;
}