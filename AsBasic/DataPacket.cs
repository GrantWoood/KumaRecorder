using AsAbstract;

namespace AsBasic;

public abstract class DataPacket<T> : IDataPacket{
    public T Data{get;set;}
    public string SyncKey{get; set; } = string.Empty;
    public long TimeStamp{get;set;} = 0;
    public abstract double[] AsDoubleArray();
}

public class FloatArrayDataPacket: DataPacket<float[]>
{
    public override double[] AsDoubleArray(){
        double[] doubles= new double[Data.Length];
        for(int i=0;i<doubles.Length;i++)
        {
            doubles[i] = Data[i];
        }
        return doubles;
    }
}

public class DoubleArrayDataPacket:DataPacket<double[]>{
    public override double[] AsDoubleArray(){
        return Data;
    }
}