using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

public class ListCommandSettings: CommandSettings{
    
}

public class ListIoServiceTreeCommandSettings: ListCommandSettings{

}

public class ListIoStreamTreeCommandSettings: ListCommandSettings{

}

public class ListInputListCommandSettings:ListCommandSettings{

}

public class ListIoServiceTreeCommand : Command<ListIoServiceTreeCommandSettings>
{
    public override int Execute(CommandContext context, ListIoServiceTreeCommandSettings settings)
    {
        var root = new Tree("IoServices");
        var ioServices = (context.Data as AppContext).Application!.IoServices;
        foreach(var ioService in ioServices){
            var service = root.AddNode(ioService.Name);
            var devs = ioService.GetIoDevices();
            foreach (var dev in devs)
            {
                var devNode = service.AddNode(dev.Name);
                var inputs = ioService.GetIoChannels();
                foreach (var input in inputs)
                {
                    var inNode = devNode.AddNode($"{input.Name} ({input.ToString()})");
                    var streams = input.GetInputAdapters();
                    foreach (var s in streams)
                    {
                        inNode.AddNode(new Text($"[Input] {s.Name}, {s.FixSampleFrequency}, {s.DataType}"));
                    }
                }
            }

        }
        AnsiConsole.Write(root);
        return 0;
    }
}

public class ListInputListCommand : Command<ListInputListCommandSettings>
{
    private string GetProperty(object obj, string propertyName){
        var property = obj.GetType().GetProperty(propertyName);
        if(property!= null){
            var value = property.GetValue(obj);
            return value.ToString();
        }
        return "";
    }
    private bool SetProperty(object obj, string propertyName, string value){
        var property = obj.GetType().GetProperty(propertyName);
        if(property!= null){
            //Try convert
            property.SetValue(obj, value);
        }
        return false;
    }
    public override int Execute(CommandContext context, ListInputListCommandSettings settings)
    {
        var table = new Table();
        var ioServices = (context.Data as AppContext)!.Application!.IoServiceManager;
        List<string> columns = [];
        List<string> propertyieNames = [];
        var analogInputs = ioServices.GetIoChannels("AnalogInput");
        foreach (var input in analogInputs)
        {
            //collect rows
            var inputSettings = input.GetSettings();
            var properties = inputSettings.GetType().GetProperties();
            foreach (var property in properties){
                var propName = property.Name;
                var disName = propName;
                var displayName = property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                if(displayName != null){
                    disName = displayName.Name;
                }
                if(disName != null && columns.IndexOf(disName) == -1){
                    columns.Add(disName);
                    propertyieNames.Add(propName);
                    table.AddColumn(disName);
                }
            }
        }

        foreach (var input in analogInputs)
        {
            //collect rows
            List<Text> row = [];
            foreach(var propName in propertyieNames){
                var inputSettings = input.GetSettings();
                var property = inputSettings.GetType().GetProperty(propName);
                string value = "--";
                if(property != null){
                    value = property.GetValue(inputSettings)!.ToString()!;
                }
                row.Add(new Text(value));;
            }
            table.AddRow(row);
        }

        AnsiConsole.Write(table);
        return 0;
    }
}

public class ListIoStreamTreeCommand : Command<ListIoStreamTreeCommandSettings>
{
    public override int Execute(CommandContext context, ListIoStreamTreeCommandSettings settings)
    {
        var root = new Tree("Streams");
        var streamManager = (context.Data as AppContext)!.Application!.StreamManager;
        foreach(var classification in streamManager.Streams){
            var clsNode = root.AddNode(classification.Key);
            foreach (var stream in classification.Value){
                clsNode.AddNode($"{stream.Name}");
            }
        }
        AnsiConsole.Write(root);
        return 0;
    }
}