using Microsoft.Extensions.Configuration;

namespace AsAbstract;

public interface IIoServiceBuilder{
    string Id{ get; }
    IIoService Build(IConfiguration? configuration);
}