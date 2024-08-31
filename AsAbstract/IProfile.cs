namespace AsAbstract;

//Test profiles interface
//保存每个对象的具体参数信息
public interface ITestProfile{
    ITestProfileSection GetSection(string key);
    IEnumerable<ITestProfileSection> GetChildren();
    dynamic GetValue();
}

public interface ITestProfileSection: ITestProfile{

}