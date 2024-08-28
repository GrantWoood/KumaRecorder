namespace AsAbstract;

public interface IConfiguration{
    IConfiguration? GetConfiguration(string key);
}