using Spectre.Console.Cli;

class CliAppBuilder{
    public CommandApp Builder(AppContext context1){
        CommandApp app = new CommandApp();
        app.Configure(config=>{
            config.AddBranch<ListCommandSettings>("ls",
                ls=>{
                    ls.AddCommand<ListIoServiceTreeCommand>("tree").WithData(context1);
                    ls.AddCommand<ListInputListCommand>("inputs").WithData(context1);;
                    ls.AddCommand<ListIoStreamTreeCommand>("streams").WithData(context1);;
                });
            config.AddBranch<CliSetCommandSettings>("set",
                set=>{
                    set.AddCommand<CliSetInputCommand>("input").WithData(context1);
                });
            config.AddBranch<CliSampleCommandSettings>("sample",
                sample=>{
                    sample.AddCommand<CliSampleStartCommand>("start").WithData(context1);
                    sample.AddCommand<CliSampleStopCommand>("stop").WithData(context1);
                }
            );
            config.AddBranch<CliAnalysisCommandSettings>("analysis", 
            ana=>{
                ana.AddCommand<CliAnalysisStartCommand>("start").WithData(context1);
                ana.AddCommand<CliAnalysisStopCommand>("stop").WithData(context1);
                ana.AddCommand<CliAnalysisPauseCommand>("pause").WithData(context1);
                ana.AddCommand<CliAnalysisResumeCommand>("resume").WithData(context1);
            });
        });
        
        return app;
    }
}