namespace AsAbstract;
public interface ISyncManager{
    ISynchronize Master{get;}
    string Register(ISynchronize synchronize);
}