using System.Security.Cryptography.X509Certificates;
using AsCore;

public interface ICommand{
    string Key{get;}
    void Run(AppContext application);
}

public class ListPorts : ICommand{
    public string Key{get{return "ls";}}
    public void Run(AppContext application){

    }
}