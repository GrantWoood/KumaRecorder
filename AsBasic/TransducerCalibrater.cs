using AsAbstract;

namespace AsBasic;

public class TransducerCalibrater: ICalibrater{
    public double Sensitivity{get;set;} = 1.0;
    public string UnitPhysical{get;set;} = String.Empty;//G, Pa, ...
    public string UnitMeasure{get;set;} = String.Empty;//mV, pC

    //return the physical value from measure value,measure must be in UnitMeasure Unit
    //The returend value will be in UnitPhysical Unit
    public double Convert(double measure){
        return measure / Sensitivity;
    }

    public string SensitivityUnit{
        get{
            var bm = GetBrackedUnitIfNeeded(UnitMeasure);
            var bp = GetBrackedUnitIfNeeded(UnitPhysical);
            return $"{bm}/{bp}";
        }
    }

    private string GetBrackedUnitIfNeeded(string unit){
        return ShouldBracked(unit) ? $"({unit})" : unit;
    }
    private bool ShouldBracked(string unit){
        if(unit.IndexOf("/")>= 0){
            return true;
        }
        return false;
    }
}