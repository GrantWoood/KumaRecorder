using Spectre.Console.Cli;

class CliSampleCommandSettings:CommandSettings{

}

class CliSampleStartCommandSettings: CliSampleCommandSettings{
    
}

class CliSampleStopCommandSettings: CliSampleCommandSettings{
    
}

class CliAnalysisCommandSettings:CommandSettings{

}

class CliAnalysisStartCommandSettings: CliAnalysisCommandSettings{
    
}

class CliAnalysisStopCommandSettings: CliAnalysisCommandSettings{
    
}

class CliAnalysisPauseCommandSettings: CliAnalysisCommandSettings{
    
}

class CliAnalysisResumeCommandSettings: CliAnalysisCommandSettings{
    
}
class CliSampleStartCommand : Command<CliSampleStartCommandSettings>
{
    public override int Execute(CommandContext context, CliSampleStartCommandSettings settings)
    {
        (context.Data as AppContext).Application.StartSample();
        return 0;
    }
}

class CliSampleStopCommand : Command<CliSampleStopCommandSettings>
{
    public override int Execute(CommandContext context, CliSampleStopCommandSettings settings)
    {
        (context.Data as AppContext).Application.StopSample();
        return 0;
    }
}

class CliAnalysisStartCommand : Command<CliAnalysisStartCommandSettings>
{
    public override int Execute(CommandContext context, CliAnalysisStartCommandSettings settings)
    {
        (context.Data as AppContext).Application.StartRecordAndAnalysis();
        return 0;
    }
}

class CliAnalysisStopCommand : Command<CliAnalysisStopCommandSettings>
{
    public override int Execute(CommandContext context, CliAnalysisStopCommandSettings settings)
    {
        (context.Data as AppContext).Application.StopRecordAndAnalysis();
        return 0;
    }
}

class CliAnalysisPauseCommand : Command<CliAnalysisPauseCommandSettings>
{
    public override int Execute(CommandContext context, CliAnalysisPauseCommandSettings settings)
    {
        (context.Data as AppContext).Application.PauseRecordAndAnalysis();
        return 0;
    }
}

class CliAnalysisResumeCommand : Command<CliAnalysisResumeCommandSettings>
{
    public override int Execute(CommandContext context, CliAnalysisResumeCommandSettings settings)
    {
        (context.Data as AppContext).Application.ResumeRecordAndAnalysis();
        return 0;
    }
}