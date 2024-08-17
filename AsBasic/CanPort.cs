using AsAbstract;

namespace AsBasic;

public class CanPort: IIoPort{
    private string _Id = string.Empty;
    public string Id { 
        get { return _Id; } 
        set{ _Id = value;}
    }
}