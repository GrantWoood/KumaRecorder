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
        });
        
        return app;
    }
}