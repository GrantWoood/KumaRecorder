using AsAbstract;

namespace AsBasic;
public class AnalogPort: IIoPort{
    private string _Id = string.Empty;
    public string Id { 
        get { return _Id; } 
        set{ _Id = value;}
    }
}