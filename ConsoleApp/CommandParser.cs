class CommandParser(CommandManager commandManager){
    CommandManager _commandManager = commandManager;
    public ICommand? Parse(string cmdline){
        var words = SplitCommand(cmdline);
        if(words.Length > 0 ){
            foreach(var cmd in _commandManager.Commands){
                if(cmd.Key == words[0]){
                    ICommand command = cmd;
                    Arguments arguments = new Arguments();
                    for(int i=1; i<words.Length;++i){
                        if(words[i].StartsWith('-')){
                            Argument arg = new Argument(){
                                Key = words[i],
                            };
                            arguments.ArgumentList.Add(arg);
                        }
                        else if(arguments.ArgumentList.Count > 0){
                            arguments.ArgumentList.Last().values = words[i];
                        }
                    }
                    command.Arguments = arguments;
                    return command;
                }
            }
        }
        return null;
    }

    public string[] SplitCommand(string command){
        //Ignore ""
        return command.Split(' ',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}