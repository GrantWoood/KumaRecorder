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
    [DisplayName("Range")]
    public RangeLevel Range { get; set;}
    [DisplayName("Input")]
    public InputMode Input { get; set;}
    [DisplayName("Couple")]
    public SignalMode Couple{get;set;}
    [DisplayName("Gain")]
    public double Gain{get; set;}
    [DisplayName("Offset")]
    public double Offset{get; set;} 
}