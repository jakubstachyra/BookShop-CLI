namespace Bajtpik.Data.Interfaces
{
    public interface IBoardGame : IEntity
    {
        string Title { get; }
        int? MinPlayers { get; }
        int? MaxPlayers { get; }
        int? Difficulty { get; }
        List<IAuthor> Authors { get; }
    }
}
