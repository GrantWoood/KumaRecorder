namespace AsAbstract;
public interface ISynchronize{
    string Name{get;}
    string Key{get;set;}
    long Now();
}