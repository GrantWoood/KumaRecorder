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

public class ListPorts : ICommand{
    public string Key=>"ls";
    public string Description=>"-p: Show io ports; -t: Show device tree; -s: Show io streams.";
    public Arguments Arguments{get;set;} = new Arguments();

    public void Run(AppContext context, ref bool continueRun){
        StringBuilder stringBuilder= new StringBuilder();
        foreach(var arg in Arguments.ArgumentList){
            switch(arg.Key){
                case "-p":
                stringBuilder.AppendLine(GetIoPortList(context));
                break;
                case "-s":
                stringBuilder.AppendLine(GetInputStreamList(context));
                break;
                case "-t":
                stringBuilder.AppendLine(GetIoDeviceTree(context));
                break;
                default:
                break;
            }
        }
        Console.WriteLine(stringBuilder.ToString());    
    }

    public string GetIoPortList(AppContext context){
        StringBuilder builder = new StringBuilder();
        var services = context.Application!.IoServices;
        foreach(var service in services){
            var inputs = service.GetIoChannels();
            foreach(var input in inputs){
                builder.AppendLine(PrintInput(input));
            }
        }
        return builder.ToString();
    }

    public string GetInputStreamList(AppContext context){
        StringBuilder builder = new StringBuilder();
        var services = context.Application!.IoServices;
        foreach(var service in services){
            var streams = service.GetInputAdapters();
            foreach(var stream in streams){
                builder.AppendLine(PrintStream(stream));
            }
        }
        return builder.ToString();
    }

    public string PrintInput(IIoChannel channel){
        StringBuilder stringBuilder= new StringBuilder();
        var inputStreams = channel.GetInputAdapters();
        stringBuilder.Append(channel.IoDevice.Name);
        stringBuilder.Append("-");
        stringBuilder.Append(channel.IoPort);
        /* Relative with hardware directly
        Analog: Connection, Range, Mode, Gain, Offset, Sensitivity/Other Calibrater, ...
        Can: Connection, CAN1.0/Can2.0, BitRate, Filters, CanDB...
        Gps: Connection, BitRate, Filters, ...
        NetCamera: Connection, FPS, Protocol

        Connect: DeviceName - PortId
            As-Demo-DE001-A01
            As-Demo-DE001-CAN01
            As-Demo-DE001-GPS01
            Rtsp://192.168.1.35:502/01
        */
        stringBuilder.Append("");

        /*Streams in this channel, show the value, other properties may by setable, such as default color, usage, ....
        Analog: ----
            A: v
        Can: ----
            V1: v
            V2: v
        Gps: ----
            Location: la,lo,al
            Speed: speed
        NetCamera: --- 
            Video: 
            Audio-Left:
            Audio-Right:
        */

        //stringBuilder.Append("  ");
        //stringBuilder.Append(channel.Name);
        //stringBuilder.Append(channel.ToString());
        return stringBuilder.ToString();
    }

    public string PrintStream(IDataAdapter stream){
        StringBuilder stringBuilder= new StringBuilder();
        stringBuilder.Append($"{stream.ToString()}, {stream.FixSampleFrequency}, {stream.DataType}");
        return stringBuilder.ToString();
    }

    private string GetIoDeviceTree(AppContext context){
        StringBuilder stringBuilder= new StringBuilder();
        var services = context.Application!.IoServices;
        foreach (var service in services){
            stringBuilder.AppendLine(service.ToString());
            foreach(var dev in service.GetIoDevices())
            {
                stringBuilder.Append(" | --");
                stringBuilder.AppendLine(dev.ToString());
                foreach(var input in dev.GetIoChannels()){
                    stringBuilder.Append("     | --");
                    stringBuilder.AppendLine(input.ToString());
                    foreach(var ad in input.GetInputAdapters()){
                        stringBuilder.Append("          | --");
                        stringBuilder.AppendLine($"{ad.ToString()}, {ad.DataType}");
                    }
                }
            }
        }
        return stringBuilder.ToString();
    }
}