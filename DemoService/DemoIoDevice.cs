using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoService;

public class DemoIoDevice(ILogger logger): IIoDevice
{
    private readonly List<AnalogInput> _analogInputs = [];
    private GpsInput? _gpsInput = null;
    private string _name;
    public string Name{
        get { return _name; }
        set { _name = value; }
    }

    #region Parameters for input simulation

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
            _analogInputs.Add(new AnalogInput(){
                Calibrater = new TransducerCalibrater(),
                IoPort = new AnalogPort(),
                IoDevice = this,
            });
        }
        _gpsInput =  new GpsInput(){
            IoPort = new GpsPort(),
            IoDevice = this,
        };
        return true;
    }

    public List<IIoChannel> GetInputChannels(){
        List<IIoChannel> channels = [];
        channels.AddRange(_analogInputs);
        if(_gpsInput!=null)
            channels.Add(_gpsInput);
        return channels;
    }

    public List<IDataStream> GetInputStreams(){
        var channels = GetInputChannels();
        List<IDataStream> streams = [];
        foreach(var channel in channels){
            streams.AddRange(channel.GetInputStreams());
        }
        return streams;
    }

    public bool StartSample()
    {
        //Use task for generating data, It may be better to use Thread here
        _sampling = true;
        _sampleTask = Task.Run(
            () =>
            {
                while (_sampling)
                {
                    //Generate sine wave
                    var raw = new float[_sampleFrequency];
                    for (int i = 0; i < _sampleFrequency; ++i)
                    {
                        raw[i] = (float)Math.Sin(2 * Math.PI * _sineFrequency * (i + _sampleCounter) / _sampleFrequency);
                    }
                    _sampleCounter += _sampleFrequency;
                    
                    //Same data for every analog input
                    foreach (var analogInput in _analogInputs)
                    {
                        double[] values = new double[_sampleFrequency];
                        for(int i=0; i<_sampleFrequency; ++i){
                            values[i] = analogInput.Calibrater.Convert((double)raw[i]);
                        }
                        analogInput.InputStream.Add(values);
                    }
                    logger.LogInformation($"Demo {_sampleFrequency} generated");

                    //Generate GPS 
                    var Location = new Location(){
                        Latitude = 0,
                        Longitude = 0,
                        Altitude = 0,
                    };
                    var Speed = 0.0;
                    _gpsInput.Location.Add(Location);
                    _gpsInput.Speed.Add(Speed);
                    logger.LogInformation($"Demo gps generated");

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