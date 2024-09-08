using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AsAbstract;
using AsBasic;
using AsCore;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

class CliSetCommandSettings: CommandSettings{

}

class CliSetInputCommandSettings: CliSetCommandSettings{
    [CommandArgument(0, "[Analog Id]")]
    public string AnalogId { get; set; } = string.Empty;

    [CommandArgument(1, "[Property Name]")]
    public string PropertyName { get; set; } = string.Empty ;
    [CommandArgument(2, "[Value]")]
    public string Value { get; set; } = string.Empty;
    [CommandOption("-t|--type <IoChannelType>")]
    public string IoChannelType{get;set;} =string.Empty;
}

class CliSetInputCommand : Command<CliSetInputCommandSettings>
{
    public override int Execute(CommandContext context, 
        CliSetInputCommandSettings settings)
    {
        IoServiceManager serviceManager = (context.Data as AppContext)!.Application!.IoServiceManager;
        IIoChannel? analog = serviceManager.GetIoChannel(settings.IoChannelType, settings.AnalogId);
        if(analog != null){
            //Get by property name
            var analogSettings = analog.GetSettings();
            var property = analogSettings.GetType().GetProperty(settings.PropertyName);
            if(property == null){
                //Get by display name
                foreach(var prop in analogSettings.GetType().GetProperties()){
                    var attribute = prop.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    if (attribute != null && attribute.Name == settings.PropertyName)
                    {
                        property = prop;
                        break;
                    }
                }

            }
            if(property != null){
                //Run possible converter from string to value
                var attributes = property.GetCustomAttributes(false);
                try{
                    property.SetValue(analogSettings, TypeDescriptor.GetConverter(property.PropertyType).ConvertFrom(settings.Value));
                }catch(Exception e){
                    AnsiConsole.WriteLine(e.ToString());
                }
            }else{
                AnsiConsole.WriteLine($"Error: No property found for {settings.PropertyName}!");
            }
        }
        else{
            AnsiConsole.WriteLine($"Error: no channel found with Id {settings.AnalogId}!");
        }
        return 0;
    }
}