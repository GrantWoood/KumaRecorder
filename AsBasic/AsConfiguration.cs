using AsAbstract;

namespace AsBasic;

public class AsConfiguration: IConfiguration{
    public IConfiguration? GetConfiguration(string key){
        return null;
    }
    public dynamic? Get(string key){
        return null;
    }

    public void Put(string key, dynamic value){
        
    }
}