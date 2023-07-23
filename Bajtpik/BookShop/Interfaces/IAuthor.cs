namespace Bajtpik.Data.Interfaces
{
    public interface IAuthor : IEntity
    {
        public string Name { get; }
        public string Surname { get; }
        public int? BirthYear { get; }
        public string? NickName { get; }
    }

}
