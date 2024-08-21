using AsAbstract;

namespace AsBasic;

public class SystemSynchronizer: ISynchronize{
    public string Name=> "system";
    public string Key{get;set;} = string.Empty;

    //自公历 0001年1月1日午夜 00:00:00以来的100ns数，最大可到9999年
    public long Now(){
        return DateTime.Now.Ticks;
    }
}