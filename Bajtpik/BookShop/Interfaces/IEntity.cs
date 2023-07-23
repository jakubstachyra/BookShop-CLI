namespace Bajtpik.Data.Interfaces
{
    public interface IEntity
    {
        string ToString();
        (object?, string) GetProperty(string propertyName);
        void SetProperty(string propertyName, object? value);
        //List<string> GetAllProperties();
    }

}
