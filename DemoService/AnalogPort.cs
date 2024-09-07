using System.ComponentModel;
using AsAbstract;

namespace DemoService;

public class AnalogPort: IIoPort
{
    public enum RangeLevel{
        R10V,
        R1V,
        R0D1V,
    };

    public enum InputMode{
        Direct,
        Charge,
        IEPE,
    }

    public enum SignalMode{
        Single,
        Differential,
    }

    public RangeLevel Range { get; set;} = RangeLevel.R1V;

    public InputMode Input { get; set;} = InputMode.Direct;

    public SignalMode Couple{get;set;} = SignalMode.Single;

    public double Gain{get; set;} = 1.0;

    public double Offset{get; set;}  = 0.0;
}