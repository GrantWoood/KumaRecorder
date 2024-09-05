using Spectre.Console;

public class ListCommand: ICommand{
    public string Key=>"ls";
    public string Description=>"-t: Show device tree;-s Show streams; -c Show channel settings;";
    public Arguments Arguments{get;set;} = new Arguments();

    public void Run(AppContext context, ref bool continueRun)
    {
        foreach (var arg in Arguments.ArgumentList)
        {
            switch (arg.Key)
            {
                case "-t":
                    ListIoTree(context);
                    break;
                case "-s":
                    ListStreams(context);
                    break;
                case "-c":
                    ListChannels(context);
                    break;
                default:
                    break;
            }
        }
    }

    private void ListIoTree(AppContext context)
    {
        var root = new Tree("IoServices");
        var ioServices = context.Application!.IoServices;
        foreach(var ioService in ioServices){
            var service = root.AddNode(ioService.Name);
            var inputs = ioService.GetIoChannels();
            foreach(var input in inputs){
                var inNode = service.AddNode(input.Name);
                var streams = input.GetInputAdapters();
                foreach(var s in streams){
                    inNode.AddNode(new Text($"{s.Name}, {s.FixSampleFrequency}, {s.DataType}"));
                }
            }
        }
        AnsiConsole.Write(root);
    }

    private void ListStreams(AppContext context){
        var root = new Tree("Streams");
        var streamManager = context.Application!.StreamManager;
        foreach(var classification in streamManager.Streams){
            var clsNode = root.AddNode(classification.Key);
            foreach (var stream in classification.Value){
                clsNode.AddNode(stream.Name);
            }
        }
        AnsiConsole.Write(root);
    }

    private void ListChannels(AppContext context){
        var channelTable = new Table();
        var ioServices = context.Application!.IoServices;
        List<string> columns = [
            "Id",
            "Name",
            "Status",
            "Type",
            "Calibration",
            "Level Meter"
        ];
        foreach(var ioService in ioServices){
            var inputs = ioService.GetIoChannels();
            foreach(var input in inputs){
                //Get input channels settings
                //Is need show each value in channel, show something under channel settings
            }
        }
        foreach(var col in columns){
            channelTable.AddColumn(col);
        }
        AnsiConsole.Write(channelTable);
    }
}