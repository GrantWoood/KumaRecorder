using System.Text;
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
    Arguments? Arguments{get;set;}
    void Run(AppContext application, ref bool continueRun);
}

public class Quit:ICommand{
    public string Key=>"quit";
    public string Description=>"Exit this application.";
    public Arguments? Arguments{get;set;}

    public void Run(AppContext application, ref bool continueRun){
        Console.WriteLine("Exit.")    ;
        continueRun = false;
    }
}

public class ListPorts : ICommand{
    public string Key=>"ls";
    public string Description=>"-p: Show io ports; -t: Show device tree; -s: Show io streams.";
    public Arguments? Arguments{get;set;}

    public void Run(AppContext application, ref bool continueRun){
        StringBuilder stringBuilder= new StringBuilder();
        Console.WriteLine(stringBuilder.ToString());    
    }
}