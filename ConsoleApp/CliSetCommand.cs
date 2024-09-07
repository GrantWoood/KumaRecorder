using AsCore;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

class CliSetCommandSettings: CommandSettings{

}

class CliSetAnalogCommandSettings: CommandSettings{
    [CommandArgument(0, "[Analog Id]")]
    public string AnalogId { get; set; } = string.Empty;

    [CommandArgument(1, "[Property Name]")]
    public string PropertyName { get; set; } = string.Empty ;
    [CommandArgument(2, "Value")]
    public string Value { get; set; } = string.Empty;
}

class CliSetAnalogCommand : Command<CliSetAnalogCommandSettings>
{
    public override int Execute(CommandContext context, 
        CliSetAnalogCommandSettings settings)
    {
        IoServiceManager serviceManager = (context.Data as AppContext).Application.IoServiceManager;
        var analog = serviceManager.GetIoChannels(settings.AnalogId);
        if(analog != null){
            var property = analog.GetType().GetProperty(settings.PropertyName);
            if(property != null){
                property.SetValue(analog, settings.Value);
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