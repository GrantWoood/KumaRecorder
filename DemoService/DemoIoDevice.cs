using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoService;

public class DemoIoDevice(ILogger logger): IIoDevice
{
    private readonly List<AnalogInput> _analogInputs = [];
    private readonly GpsInput _gasInput = new GpsInput();

    #region Parameters

    private int _sampleFrequency = 4096;
    private Int64 _sampleCounter = 0;
    private float _sineFrequency = 10.60f;

    #endregion

    #region Running Control

    private Task? _sampleTask = null;
    private bool _sampling = false;
    
    #endregion
    
    public bool Configure(IConfigurationSection? configurationSection)
    {
        for (int i = 0; i < 4; ++i)
        {
            _analogInputs.Add(new AnalogInput());
        }

        return true;
    }

    public bool StartSample()
    {
        //临时用Task使用线程池执行，因为这里使用了Sleep，所以似乎不应该使用Task,而使用独立的Thread
        _sampling = true;
        _sampleTask = Task.Run(
            () =>
            {
                while (_sampling)
                {
                    //模拟生成数据
                    var raw = new float[_sampleFrequency];
                    for (int i = 0; i < _sampleFrequency; ++i)
                    {
                        raw[i] = (float)Math.Sin(2 * Math.PI * _sineFrequency * (i + _sampleCounter) / _sampleFrequency);
                    }
                    _sampleCounter += _sampleFrequency;
                    
                    //每个通道生成相同的数据
                    foreach (var analogInput in _analogInputs)
                    {
                        //analogInput.ioPort.Add(raw);
                    }
                    logger.LogInformation($"Demo {_sampleFrequency} generated");
                    Thread.Sleep(1000);
                }
            });
        return true;
    }

    public bool StopSample()
    {
        _sampling = false;
        _sampleTask?.Wait();
        _sampleTask = null;
        return false;
    }
}