using System.Text;
using AsAbstract;
using AsCore;

public class Argument{
    public string Key { get; set; } = String.Empty;
    public string values { get; set; } = String.Empty;
}

public class Arguments{
    public List<Argument> ArgumentList{ get; set; } = [];
}

public interface ICommand{
    string Key{get;}
    string Description{get;}
    Arguments Arguments{get;set;}
    void Run(AppContext application, ref bool continueRun);
}

public class Quit: ICommand{
    public string Key=>"quit";
    public string Description=>"Exit this application.";
    public Arguments Arguments{get;set;} = new Arguments();

    public void Run(AppContext application, ref bool continueRun){
        Console.WriteLine("Exit.")    ;
        continueRun = false;
    }
}

