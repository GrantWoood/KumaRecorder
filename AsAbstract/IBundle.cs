namespace AsAbstract;

public interface IBundle{
    void PutInt(string key, int value);
    void PutLong(string key,long value);
    void PutDouble(string key,double value);
    void PutString(string key,string value);
    void PutBundle(string key, IBundle bundle);
    void PutBundleList(string key, List<IBundle> bundles);
    int? GetInt(string key);
    long? GetLong(string key);
    double? GetDouble(string key);
    string? GetString(string key);
    IBundle? GetBundle(string key);
    List<IBundle>? GetBundleList(string key);
    IBundle CreateBundle();
}