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

    public RangeLevel Range { get; set;}
    public InputMode Mode { get; set;}

    public double Gain{get; set;}
    public double Offset{get; set;} 
}