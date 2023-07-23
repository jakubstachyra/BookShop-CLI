namespace Bajtpik.Data.Interfaces
{
    public interface IBook : IEntity
    {
        public string Title { get; }
        public int? Year { get; }
        public List<IAuthor> GetAuthors();
        public int? PageCount { get; }

    }
}
