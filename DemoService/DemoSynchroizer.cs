using System.ComponentModel.DataAnnotations;
using AsAbstract;

namespace DemoService;

public class DemoSynchroizer: ISynchronize{
    public class TickPair{
        public long TimeStamp{get;set;}
        public long TimeStampMaster{get;set;}   
    }
    public string Name=>"DemoSync";
    public string Key{get;set;} = string.Empty;
    public int TickFrequency = 0;
    List<TickPair> TickPairs = new List<TickPair>();
    public long Now(){
        return 0;
    }
    public void StartAt(int tickFrequency, long masterTick){
        TickFrequency = tickFrequency;
        TickPairs.Clear();
        TickPairs.Add(new TickPair{
            TimeStamp = 0,
            TimeStampMaster = masterTick
        });
    }
    public void Tick(long counter, long masterTick){
        TickPairs.Add(new TickPair(){
            TimeStamp = counter,
            TimeStampMaster = masterTick
        });
    }
}