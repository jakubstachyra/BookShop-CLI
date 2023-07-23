using Bajtpik.Data;
using Bajtpik.Data.Interfaces;

namespace ConsoleApp
{
    public class ObjectFactory
    {
        public static List<IEntity> CreateAuthors()
        {
            List<IEntity> authors = new List<IEntity>();

            authors.Add(new Author("Douglas", "Adams", 1952, ""));
            authors.Add(new Author("Tom", "Wolfe", 1930));
            authors.Add(new Author("Elmar", "Eisemann", 1978));
            authors.Add(new Author("Michael", "Schwarz", 1970));
            authors.Add(new Author("Ulf", "Assarsson", 1975));
            authors.Add(new Author("Michael", "Wimmer", 1980));
            authors.Add(new Author("Frank", "Herbert", 1920));
            authors.Add(new Author("Terry", "Pratchett", 1948));
            authors.Add(new Author("Neil", "Gaiman", 1960));
            authors.Add(new Author("Jamey", "Stegmaier", 1975));
            authors.Add(new Author("Jakub", "Różalski", 1981, "Mr. Werewolf"));
            authors.Add(new Author("Klaus", "Teuber", 1952));
            authors.Add(new Author("Alfred", "Butts", 1899));
            authors.Add(new Author("James", "Brunot", 1902));
            authors.Add(new Author("Christian T.", "Petersen", 1970));

            return authors;
        }

        public static List<IEntity> CreateBooks(List<IEntity> authors_list)
        {
            List<IAuthor> authors = new List<IAuthor>();
            foreach (IEntity author in authors_list)
            {
                authors.Add((IAuthor)author);
            }
            List<IEntity> books = new List<IEntity>();

            books.Add(new Book("The Hitchhiker's Guide to the Galaxy", 1987, 320, new List<IAuthor> { authors[0] }));
            books.Add(new Book("The Right Stuff", 1993, 512, new List<IAuthor> { authors[1] }));
            books.Add(new Book("Real-Time Shadows", 2011, 383, new List<IAuthor> { authors[2], authors[3], authors[4], authors[5] }));
            books.Add(new Book("Mesjasz Diuny", 1972, 272, new List<IAuthor> { authors[6] }));
            books.Add(new Book("Dobry Omen", 1990, 416, new List<IAuthor>()));

            return books;
        }

        public static List<IEntity> CreateNewspapers()
        {
            List<IEntity> newspapers = new List<IEntity>();

            newspapers.Add(new Newspaper("International Journal of Human-Computer Studies", int.MaxValue, 300));
            newspapers.Add(new Newspaper("Nature", 1869, 200));
            newspapers.Add(new Newspaper("National Geographic", 2001, 106));
            newspapers.Add(new Newspaper("Pixel", 2015, 115));

            return newspapers;
        }

        public static List<IEntity> CreateBoardGames(List<IEntity> authors_list)
        {
            List<IEntity> boardGames = new List<IEntity>();
            List<IAuthor> authors = new List<IAuthor>();
            foreach (var author in authors_list)
            {
                authors.Add((IAuthor)author);
            }
            boardGames.Add(new BoardGame("Scythe", 1, 5, 7, new List<IAuthor> { authors[9], authors[10] }));
            boardGames.Add(new BoardGame("Catan", 3, 4, 6, new List<IAuthor> { authors[11] }));
            boardGames.Add(new BoardGame("Scrabble", 2, 4, 5, new List<IAuthor> { authors[12], authors[13] }));
            boardGames.Add(new BoardGame("Twilight Imperium", 3, 8, 9, new List<IAuthor> { authors[14] }));

            return boardGames;
        }
    }
}
