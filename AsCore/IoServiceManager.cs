
using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AsCore;

public class IoServiceManager(ILogger logger){
    private readonly List<IIoService> _ioServices = [];
    public List<IIoService> IoServices => _ioServices;

    public bool StartSample()
    {
        foreach (var service in IoServices)
        {
            service.StartSample();
        }
        return false;
    }

    public bool StopSample()
    {
        foreach (var service in IoServices)
        {
            service.StopSample();
        }
        return false;
    }

    public bool LoadProfile(IBundle? profile, IIoServiceFactory ioServiceFactory){
        var servicesProfile = profile?.GetBundleList("IoServices");
        if (servicesProfile != null)
        {
            foreach (var sp in servicesProfile)
            {
                var id = sp.GetString("id");
                if (id != null)
                {
                    var service = ioServiceFactory!.Create(id);
                    if (service == null)
                    {
                        logger.LogError($"Profile is provided, but io service create failed for {id}");
                    }
                    else if (!service.LoadProfile(sp))
                    {
                        logger.LogError($"Load profile failed for io service {service.Name}!");
                    }
                    else
                    {
                        logger.LogInformation($"Profile load success for {service.Name}!");
                        _ioServices.Add(service);
                    }
                }
                else
                {
                    logger.LogError($"Service id is not specified for io service");
                }
                
            }
        }
        if(IoServices.Count == 0){
            logger.LogWarning("Load profile failed, no io service created. Create default io service by default!");
            LoadDefault(ioServiceFactory);
        }
        OnIoServiceLoaded();
        return true;
    }

    public void LoadDefault(IIoServiceFactory factory){
        var service = factory.Create("");
        if (service != null)
        {
            _ioServices.Add(service);
        }
        OnIoServiceLoaded();
    }

    public bool SaveProfile(IBundle profile)
    {
        List<IBundle> servicesProfile = [];
        foreach (var service in _ioServices)
        {
            IBundle sp = profile.CreateBundle();
            service.SaveProfile(sp);
            servicesProfile.Add(sp);
        }
        profile.PutBundleList("IoServices", servicesProfile);
        return true;
    }

    private void OnIoServiceLoaded(){
        for(int i=0; i<IoServices.Count; ++i){
            IoServices[i].Id = $"{i+1}";
        }
    }

    public List<IIoChannel> GetIoChannels(string channelType){
        List<IIoChannel> channels= [];
        foreach(var service in _ioServices){
            var chs = service.GetIoChannels();
            foreach(var ch in chs){
                if(ch.TypeName == channelType){
                    channels.Add(ch);
                }
            }
        }
        return channels;
    }
}