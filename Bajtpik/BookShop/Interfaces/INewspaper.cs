namespace Bajtpik.Data.Interfaces
{
    public interface INewspaper : IEntity
    {
        string Title { get; }
        int? Year { get; }
        int? PageCount { get; }
    }
}
