using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoServiceFactory
{
    IIoService? Create(string id);
    bool Configure(IConfiguration? configuration);
}