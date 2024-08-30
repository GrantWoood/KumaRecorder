using System.ComponentModel;
using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoService;

public class DemoIoDevice: IIoDevice
{
    private readonly ILogger _logger;
    private readonly DemoSynchroizer _synchroizer;
    private readonly SyncManager _syncManager;
    private readonly List<AnalogInput> _analogInputs = [];
    
    private GpsInput? _gpsInput = null;
    private string _name = string.Empty;
    public string Name{
        get { 
            if(_name.Length == 0){
                return $"{Manufacture}-{Model}-{SN}";
            } else {
                return _name;
            }
        }
        set { _name = value; }
    }

    public string Manufacture => "As";
    public string Model => "Demo";
    public string SN=>"DE001";

    #region Parameters for input simulation

    private int _sampleFrequency = 4096;
    private Int64 _sampleCounter = 0;
    private float _sineFrequency = 10.60f;

    #endregion

    #region Running Control

    private Task? _sampleTask = null;
    private bool _sampling = false;
    
    #endregion

    public DemoIoDevice(ILogger logger, SyncManager syncManager){
        _logger = logger;
        _syncManager = syncManager;
        _synchroizer = new DemoSynchroizer();
    }
    
    public bool Configure(IConfiguration? configuration)
    {
        for (int i = 0; i < 4; ++i)
        {
            _analogInputs.Add(new AnalogInput(){
                IoDevice = this,
                IoPort = new AnalogPort(),
                RawAdapter = new DataAdapter(){
                    FixSampleFrequency = true,
                    DataType = typeof(float[])
                },
                Calibrater = new TransducerCalibrater() ,
                InputAdapter = new DataAdapter(){
                    FixSampleFrequency = true,
                    DataType = typeof(double[]),
                },
            });
        }
        //2Vibration and 2 Sound for analog inputs
        (_analogInputs[0].Calibrater as TransducerCalibrater)!.UnitPhysical = "G";
        (_analogInputs[0].Calibrater as TransducerCalibrater)!.UnitMeasure = "mV";
        (_analogInputs[1].Calibrater as TransducerCalibrater)!.UnitPhysical = "G";
        (_analogInputs[1].Calibrater as TransducerCalibrater)!.UnitMeasure = "mV";
        (_analogInputs[2].Calibrater as TransducerCalibrater)!.UnitPhysical = "Pa";
        (_analogInputs[2].Calibrater as TransducerCalibrater)!.UnitMeasure = "mV";
        (_analogInputs[3].Calibrater as TransducerCalibrater)!.UnitPhysical = "Pa";
        (_analogInputs[3].Calibrater as TransducerCalibrater)!.UnitMeasure = "mV";
        _gpsInput =  new GpsInput(){
            IoDevice = this,
            IoPort = new GpsPort(),
            Raw = new DataAdapter(){
                FixSampleFrequency = false,
                DataType = typeof(double[])
            },
        };
        return true;
    }

    public List<IIoChannel> GetIoChannels(){
        List<IIoChannel> channels = [];
        channels.AddRange(_analogInputs);
        if(_gpsInput!=null)
            channels.Add(_gpsInput);
        return channels;
    }

    public List<IDataAdapter> GetInputAdapters(){
        var channels = GetIoChannels();
        List<IDataAdapter> streams = [];
        foreach(var channel in channels){
            streams.AddRange(channel.GetInputAdapters());
        }
        return streams;
    }

    public bool StartSample()
    {
        //Use task for generating data, It may be better to use Thread here
        _synchroizer.StartAt(_sampleFrequency, _syncManager.Master.Now());
        _sampling = true;
        _sampleTask = Task.Run(
            () =>
            {
                long counter = 0;
                while (_sampling)
                {
                    //Generate sine wave
                    var raw = new float[_sampleFrequency];
                    for (int i = 0; i < _sampleFrequency; ++i)
                    {
                        raw[i] = (float)Math.Sin(2 * Math.PI * _sineFrequency * (i + _sampleCounter) / _sampleFrequency);
                    }
                    _sampleCounter += _sampleFrequency;
                    counter += _sampleFrequency;

                    (_synchroizer).Tick(counter, _syncManager.Master.Now());
                    var rawPacket = new FloatArrayDataPacket();
                    rawPacket.Data = raw;
                    rawPacket.SyncKey = _synchroizer.Key;
                    rawPacket.TimeStamp = counter;
                    
                    //Same data for every analog input
                    foreach (var analogInput in _analogInputs)
                    {
                        analogInput!.RawAdapter!.Receive(rawPacket);
                    }
                    _logger.LogInformation($"Demo {_sampleFrequency} generated");

                    //Generate GPS 
                    var gpsvs = new double[4]{
                        0,  //latitude
                        0,  //longitude
                        0,  //altitude
                        0   //speed
                    };
                    var gpsRaw = new DoubleArrayDataPacket();
                    _gpsInput!.Raw!.Receive(gpsRaw);
                    _logger.LogInformation($"Demo gps generated");

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