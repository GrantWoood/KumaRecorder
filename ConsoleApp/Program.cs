using AsCore;
using ConsoleApp;
using DemoService;
using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(
    builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("AsConsole");

AsApplication application = new AsApplication()
{
    IoServiceFactory = new IoServiceFactory(logger),
};
application.Configure(null);

CommandManager commandManager = new CommandManager();
CommandParser commandParser = new CommandParser(commandManager);

AppContext context = new AppContext(){
    Application = application,
    commandManager = commandManager,
};

bool continueRun = true;
while(continueRun){
    var cmd = Console.ReadLine();
    if(cmd == null){
        continue;
    }
    ICommand? command = commandParser.Parse(cmd);
    if(command == null){
        Console.WriteLine("Invalid command");
    }else{
        command?.Run(context);
    }
}


application.StartSample();
var counter = 10;
while (counter > 0)
{
    Console.WriteLine($"Sample running {counter}...");
    Thread.Sleep(1000);
    --counter;
}
application.StopSample();
Console.WriteLine("Exit.");