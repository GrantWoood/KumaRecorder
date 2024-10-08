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
    private readonly DemoIoService _demoService;
    
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

    public string Id{get;set;}
    public string FullId{get{
        return $"{_demoService.FullId}.{Id}";
    }}

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


    public DemoIoDevice(DemoIoService parent, 
        ILogger logger, SyncManager syncManager){
        _demoService = parent;
        _logger = logger;
        _syncManager = syncManager;
        _synchroizer = new DemoSynchroizer();
    }
    
    public bool Configure(IConfiguration? configuration)
    {
        for (int i = 0; i < 4; ++i)
        {
            var input = new DemoAnalogChannel()
            {
                IoDevice = this,
                IoPort = new AnalogPort(),
                Id = $"{IoChannelType.AnalogShort}{i + 1}",
                Name = $"{IoChannelType.AnalogShort}{i + 1}",
                Calibrater = new TransducerCalibrater(),
            };
            _analogInputs.Add(input);

            input.RawAdapter = new DataAdapter()
            {
                FixSampleFrequency = true,
                DataType = typeof(float[]),
                Id = "RIn1",
                Parent = input,
            };

            input.InputAdapter = new DataAdapter()
            {
                FixSampleFrequency = true,
                DataType = typeof(double[]),
                TypeName = DataAdapterType.AnalogInput,
                Parent = input,
                Id = "In1"
            };
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
            Id = $"{IoChannelType.GpsShort}{1}",
            Name=$"{IoChannelType.GpsShort}{1}",
            IoPort = new GpsPort(),
        };
        _gpsInput.Raw = new DataAdapter(){
                FixSampleFrequency = false,
                DataType = typeof(double[]),
                Parent = _gpsInput,
                Id = "RIn1"
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

    public List<IDataAdapter> GetRawInputAdapters(){
        var channels = GetIoChannels();
        List<IDataAdapter> rawAdapters =[];
        foreach(var channel in channels){
            rawAdapters.AddRange(channel.GetRawAdapters());
        }
        return rawAdapters;
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

    public bool LoadProfile(IBundle? configuration)
    {
        if (configuration != null)
        {
            var name = configuration.GetString("name");
            if (name != null)
            {
                _name = name;
            }
            var analogsProfile = configuration.GetBundleList("analogs");
            if(analogsProfile != null && 
                analogsProfile.Count == _analogInputs.Count){
                for(int i=0; i<analogsProfile.Count;++i){
                    _analogInputs[i].LoadProfile(analogsProfile[i]);
                }
            }
            var gpsProfiles = configuration.GetBundleList("gpses");
            if(gpsProfiles != null && _gpsInput!=null && 
                gpsProfiles.Count == 1){
                _gpsInput.LoadProfile(gpsProfiles[0]);
            }
        }
        return true;
    }
    public bool SaveProfile(IBundle configuration){
        configuration.PutString("name", _name);
        List<IBundle> analogBundles = [];
        foreach (var analogInput in _analogInputs){
            var analogBundle = configuration.CreateBundle();
            analogInput.SaveProfile(analogBundle);;
            analogBundles.Add(analogBundle);
        }
        configuration.PutBundleList("analogs", analogBundles);
        List<IBundle> gpsBundles = [];
        if(_gpsInput!= null){
            var gpsBundle = configuration.CreateBundle();
            _gpsInput.SaveProfile(gpsBundle);
            gpsBundles.Add(gpsBundle);
        }
        configuration.PutBundleList("gpses", gpsBundles);

        return true;
    }
}