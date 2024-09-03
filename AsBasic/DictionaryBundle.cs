using AsAbstract;

namespace AsBasic;

public class DictionaryBundle : IBundle
{
    public Dictionary<string, int> ValueIndexes{get; set;}=new Dictionary<string, int>();
    public List<int> IntValues{get; set;}=[];
    public List<long> LongValues{get; set;}=[];
    public List<double> DoubleValues{get; set;}=[];
    public List<string> StringValues{get; set;}=[];
    public List<DictionaryBundle> BundleValues{get; set;}=[];
    public List<List<DictionaryBundle>> BundleListValues{get; set;}=[];

    public IBundle? GetBundle(string key)
    {
        if (ValueIndexes.ContainsKey(key))
        {
            var index = ValueIndexes[key];
            if (index >= 0 && index < BundleValues.Count)
            {
                return BundleValues[index];
            }
        }
        return null;
    }

    public List<IBundle>? GetBundleList(string key)
    {
        List<IBundle> bundles = [];
        if (ValueIndexes.ContainsKey(key))
        {
            var index = ValueIndexes[key];
            if (index >= 0 && index < BundleListValues.Count)
            {
                foreach (var bundle in BundleListValues[index])
                {
                    bundles.Add(bundle);
                }
            }
        }
        return bundles;
    }

    public double? GetDouble(string key)
    {
        if (ValueIndexes.ContainsKey(key))
        {
            var index = ValueIndexes[key];
            if (index >= 0 && index < DoubleValues.Count)
            {
                return DoubleValues[index];
            }
        }
        return null;
    }

    public int? GetInt(string key)
    {
        if (ValueIndexes.ContainsKey(key))
        {
            var index = ValueIndexes[key];
            if (index >= 0 && index < IntValues.Count)
            {
                return IntValues[index];
            }
        }
        return null;
    }

    public long? GetLong(string key)
    {
        if (ValueIndexes.ContainsKey(key))
        {
            var index = ValueIndexes[key];
            if (index >= 0 && index < LongValues.Count)
            {
                return LongValues[index];
            }
        }
        return null;
    }

    public string? GetString(string key)
    {
        if (ValueIndexes.ContainsKey(key))
        {
            var index = ValueIndexes[key];
            if (index >= 0 && index < StringValues.Count)
            {
                return StringValues[index];
            }
        }
        return null;
    }

    public void PutBundle(string key, IBundle bundle)
    {
        ValueIndexes[key] = BundleValues.Count;
        BundleValues.Add((bundle as DictionaryBundle)!);
    }

    public void PutBundleList(string key, List<IBundle> bundles)
    {
        List<DictionaryBundle> dbundles = [];
        foreach (var bundle in bundles){
            dbundles.Add((bundle as DictionaryBundle)!);
        }
        ValueIndexes[key] = BundleListValues.Count;
        BundleListValues.Add(dbundles);
    }

    public void PutDouble(string key, double value)
    {
        ValueIndexes[key] = DoubleValues.Count;
        DoubleValues.Add(value);
    }

    public void PutInt(string key, int value)
    {
        ValueIndexes[key] = IntValues.Count;
        IntValues.Add(value);
    }

    public void PutLong(string key, long value)
    {
        ValueIndexes[key] = LongValues.Count;
        LongValues.Add(value);
    }

    public void PutString(string key, string value)
    {
        ValueIndexes[key] = StringValues.Count;
        StringValues.Add(value);
    }

    public IBundle CreateBundle(){
        return new DictionaryBundle();
    }
}