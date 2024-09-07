using Spectre.Console.Cli;

class CliQuitCommandSettings:CommandSettings{

}

class CliQuitCommand : Command<CliQuitCommandSettings>
{
    public override int Execute(CommandContext context, CliQuitCommandSettings settings)
    {
        (context.Data as AppContext).Running = false;
        return 0;
    }
}