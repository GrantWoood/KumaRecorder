namespace AsAbstract;

public interface IIoServiceFactory
{
    IIoService? Create(string id);
}