using AsCore;
using ConsoleApp;
using DemoService;
using Microsoft.Extensions.Logging;
using AsBasic;
using Microsoft.Extensions.Configuration;

//Create all context, and load default settings
//Such as IOSevices Available, Analyzers Available, and so on.
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("AsConsole");

var servicesConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ioservices.json", optional:true, reloadOnChange:false).Build();


var appConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional:true, reloadOnChange:false).Build();

SyncManager syncManager= new SyncManager(new SystemSynchronizer());
var serviceFacotry = new IoServiceFactory(logger, syncManager);
serviceFacotry.Configure(servicesConfiguration);


AsApplication application = new AsApplication()
{
    IoServiceFactory = serviceFacotry,
    SyncManager = syncManager,
    Logger = logger,
};

application.Configure(appConfiguration);

//Load Settings for this project
//Such as IOSevices used, Channel Settings, Analyzer and it's parameters, and so on.
var appProfile = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("defaultProfile.json", optional:true, reloadOnChange:false).Build();

application.LoadProfile(appProfile);

//Initialize Command System for Console
CommandManager commandManager = new CommandManager();
CommandParser commandParser = new CommandParser(commandManager);

AppContext context = new AppContext(){
    Application = application,
    commandManager = commandManager,
};

Console.WriteLine("Welcome to KumaRecorder.");
Console.WriteLine("");
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
        command?.Run(context, ref continueRun);
    }
}

//Save configuration


// application.StartSample();
// var counter = 10;
// while (counter > 0)
// {
//     Console.WriteLine($"Sample running {counter}...");
//     Thread.Sleep(1000);
//     --counter;
// }
// application.StopSample();
// Console.WriteLine("Exit.");