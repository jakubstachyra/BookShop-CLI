namespace Bajtpik.Data.Interfaces
{
    public interface IBuilder
    {
        IEntity Build();
        bool SetField(string fieldName, string value);
        List<string> GetFieldNames();
    }
}
