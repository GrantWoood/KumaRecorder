using AsCore;
using ConsoleApp;
using DemoService;
using Microsoft.Extensions.Logging;
using AsBasic;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

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


AsApplication application = new AsApplication(logger)
{
    IoServiceFactory = serviceFacotry,
    SyncManager = syncManager
};

application.Configure(appConfiguration);

//Load Settings for this project
//Such as IOSevices used, Channel Settings, Analyzer and it's parameters, and so on.
var profileFile = Path.Join(Directory.GetCurrentDirectory(), "profile.json");
if(Path.Exists(profileFile)){
    var dictProfile = JsonSerializer.Deserialize<DictionaryBundle>(File.ReadAllText(profileFile));
    application.LoadProfile(dictProfile);
}
else{
    application.LoadProfile(null);
}




//Initialize Command System for Console
AppContext context = new AppContext(){
    Application = application,
    Running = true
};

Console.WriteLine("Welcome to KumaRecorder.");
Console.WriteLine("");

var app = new CliAppBuilder().Builder(context);
while(context.Running){
    var cmd = Console.ReadLine();
    if(cmd == null){
        continue;
    }
    var cmds = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    app.Run(cmds);
}

Console.WriteLine("Application Exit.");

//Save configuration
using(var fs = new FileStream(profileFile, FileMode.OpenOrCreate)){
    DictionaryBundle bundle = new DictionaryBundle();
    context.Application.SaveProfile(bundle);
    JsonSerializer.Serialize<DictionaryBundle>(fs, bundle, new JsonSerializerOptions{
        WriteIndented = true,
    });
}