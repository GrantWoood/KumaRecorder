using AsAbstract;

namespace AsBasic;

public class SyncManager: ISyncManager{
    private readonly ISynchronize _master;
    public ISynchronize Master=>_master;

    Dictionary<string, ISynchronize> _synchronizers = new Dictionary<string, ISynchronize>();
    private string GetAValidKey(string name){
        if(!_synchronizers.ContainsKey(name)){
            return name;
        }
        var index = 1;
        var newKey = $"{name}_{index}";
        while(_synchronizers.ContainsKey(newKey)){
            index++;
            newKey = $"{name}_{index}";
        }
        return newKey;
    }
    public SyncManager(ISynchronize master){
        this._master = master;
    }

    public string Register(ISynchronize synchronize){
        var key = GetAValidKey(synchronize.Name);
        _synchronizers[key] = synchronize;
        return key;
    }

}