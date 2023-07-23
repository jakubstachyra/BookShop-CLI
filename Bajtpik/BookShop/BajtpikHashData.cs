using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bajtpik.Data.Interfaces;
// w konstruktorze book wywolywac Author dla Authora i w konstruktorze autor mapowac
//                           
//List<int> AuthorIds = new List<int>();
//AuthorHashData newauthor = new AuthorHashData(author.Name, author.Surname, author.BirthYear, author.NickName);
namespace Bajtpik.Data
{
    static class Hash
    {
        public static Dictionary<int, string> Map = new Dictionary<int, string>();
        public static int? ID = 0;
        public static Dictionary<int, IAuthor> AuthorsHash = new Dictionary<int, IAuthor>();
    }

    public interface IBookHashData
    {
        int? ID { get; }
        public int Title { get; }
        public int Year { get; }
        public int PageCount { get; }
        public List<int> GetAuthors();
    }
    public interface INewspaperHashData
    {
        public int? ID { get; }
        public int Title { get; }
        public int Year { get; }
        public int PageCount { get; }
    }
    public interface IBoardGameHashData
    {
        int? ID { get; }
        int Title { get; }
        int GetMinPlayers { get; }
        int GetMaxPlayers { get; }
        int GetDifficulty { get; }
        List<int> GetAuthorList();
    }
    public interface IAuthorHashData
    {
        public int? ID { get; }
        public int Name { get; }
        public int Surname { get; }
        public int? BirthYear { get; }
        public int NickName { get; }
    }
    public class BookHashDataAdapter 
    {
        private BookHashData Adaptee;

        public BookHashDataAdapter(BookHashData Adaptee)
        {
            this.Adaptee = Adaptee;
            //Dodanie do listy?
        }
        public int? ID
        {
            get
            {
                return Adaptee.ID;
            }
        }
        public string Title
        {
            get
            {
                return Hash.Map[Adaptee.TitleHash];
            }
        }
        public int? Year
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.YearHash]);
            }
        }
        public int? PageCount
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.PageCountHash]);
            }
        }
        public List<IAuthor> GetAuthors()
        {
            List<IAuthor> result = new List<IAuthor>();
            foreach (var authorID in Adaptee.GetAuthors())
            {
                result.Add(Hash.AuthorsHash[authorID]);
            }
            return result;

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).Append(" ").Append(Year).Append(" ").Append(PageCount);
            return stringBuilder.ToString();
        }
    }
    public class BoardgameHashDataAdapter
    {
        private BoardgameHashData Adaptee;

        public BoardgameHashDataAdapter(BoardgameHashData Adaptee)
        {
            this.Adaptee = Adaptee;
            //Dodanie do listy?
        }
        public int? ID
        {
            get
            {
                return Adaptee.Id;
            }
        }
        public string Title
        {
            get
            {
                return Hash.Map[Adaptee.TitleHash];
            }
        }
        public int? MinPlayers
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.MinPlayersHash]);
            }
        }
        public int? MaxPlayers
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.MaxPlayersHash]);
            }
        }
        public int? Difficulty
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.DifficultyHash]);
            }
        }
        public List<IAuthor> Authors
        {
            get
            {
                List<IAuthor> list = new List<IAuthor>();
                foreach (var author in Adaptee.GetAuthorList())
                {
                    list.Add(Hash.AuthorsHash[author]);
                }
                return list;
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).Append(" ").Append(MinPlayers).Append(" ").Append(MaxPlayers).Append("").Append(Difficulty);
            foreach (var author in Authors)
            {
                stringBuilder.Append(author.ToString());
            }
            return stringBuilder.ToString();
        }

    }
    public class NewspaperHashDataAdapter 
    {
        private NewspaperHashData Adaptee;

        public NewspaperHashDataAdapter(NewspaperHashData Adaptee)
        {
            this.Adaptee = Adaptee;
            //Dodanie do listy?
        }
        public int? ID
        {
            get
            {
                return Adaptee.ID;
            }
        }
        public string Title
        {
            get
            {
                return Hash.Map[Adaptee.Title];
            }
        }
        public int? Year
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.Year]);
            }
        }
        public int? PageCount
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.PageCount]);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).Append(" ").Append(Year).Append(" ").Append(PageCount);
            return stringBuilder.ToString();
        }

    }
    public class AuthorHashDataAdapter
    {
        private AuthorHashData Adaptee;

        public AuthorHashDataAdapter(AuthorHashData Adaptee)
        {
            this.Adaptee = Adaptee;
            //Dodanie do listy?
        }
        public int? ID
        {
            get
            {
                return Adaptee.Id;
            }
        }
        public string Name
        {
            get
            {
                return Hash.Map[Adaptee.NameHash];
            }
        }
        public string Surname
        {
            get
            {
                return Hash.Map[Adaptee.SurnameHash];
            }
        }
        public int? BirthYear
        {
            get
            {
                return int.Parse(Hash.Map[Adaptee.BirthYearHash]);
            }
        }
        public string NickName
        {
            get
            {
                if (Adaptee.NickName != 0)
                    return Hash.Map[Adaptee.NicknameHash];
                return "";
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Name).Append(" ").Append(Surname).Append(" ").Append(BirthYear).Append(" ").Append(NickName);
            return stringBuilder.ToString();
        }
    }

    public class BajtpikHashData
    {
        public static Dictionary<int, BookHashData> Books = new Dictionary<int, BookHashData>();
        public static Dictionary<int, NewspaperHashData> Newspapers = new Dictionary<int, NewspaperHashData>();
        public static Dictionary<int, BoardgameHashData> BoardGames = new Dictionary<int, BoardgameHashData>();
        public static Dictionary<int, IAuthorHashData> Authors = new Dictionary<int, IAuthorHashData>();

    }
    public class BookHashData : IBookHashData
    {
        public int? Id { get; }
        public int TitleHash { get; set; }
        public List<int> AuthorIds { get; set; }
        public int YearHash { get; set; }
        public int PageCountHash { get; set; }
        public BookHashData(string title, List<IAuthor> authors, int year, int pageCount)
        {
            Id = Hash.ID++;
            TitleHash = HashCode.Combine(title);
            Hash.Map.Add(TitleHash, title);
            YearHash = HashCode.Combine(year);
            Hash.Map.Add(YearHash, year.ToString());
            PageCountHash = HashCode.Combine(pageCount);
            Hash.Map.Add(PageCountHash, pageCount.ToString());
            AuthorIds = new List<int>();

            foreach (var author in authors)
            {
                int AuthorID = HashCode.Combine(author);
                Hash.AuthorsHash.Add(AuthorID, author);
                List<int> AuthorIds = new List<int>();

            }
        }
        public int? ID
        {
            get
            {
                return ID;
            }
        }
        public int Title
        {
            get
            {
                return Title;
            }
        }

        public int Year
        {
            get
            {
                return Year;
            }
        }
        public int PageCount
        {
            get
            {
                return PageCount;
            }
        }
        public List<int> GetAuthors()
        {
            List<int> result = new List<int>();
            foreach (var authorID in AuthorIds)
            {
                result.Add(authorID);
            }
            return result;
        }
    }

    public class NewspaperHashData : INewspaperHashData
    {
        public int? Id { get; set; }
        public int TitleHash { get; set; }
        public int YearHash { get; set; }
        public int PageCountHash { get; set; }
        public NewspaperHashData(string title, int year, int pageCount)
        {
            Id = Hash.ID++;
            TitleHash = HashCode.Combine(title);
            Hash.Map.Add(TitleHash, title);
            YearHash = HashCode.Combine(year);
            Hash.Map.Add(YearHash, year.ToString());
            PageCountHash = HashCode.Combine(pageCount);
            Hash.Map.Add(PageCountHash, pageCount.ToString());

        }
        public int? ID
        {
            get
            {
                return Id;
            }
        }
        public int Title
        {
            get
            {
                return TitleHash;
            }
        }
        public int Year
        {
            get
            {
                return YearHash;
            }
        }
        public int PageCount
        {
            get
            {
                return PageCountHash;
            }
        }
    }

    public class BoardgameHashData : IBoardGameHashData
    {
        public int? Id { get; set; }
        public int TitleHash { get; set; }
        public int MinPlayersHash { get; set; }
        public int MaxPlayersHash { get; set; }
        public int DifficultyHash { get; set; }
        public List<int> AuthorIds { get; set; }
        public BoardgameHashData(int titleHash, int minPlayers, int maxPlayers, int difficulty, List<IAuthor> authors)
        {
            Id = Hash.ID++;
            TitleHash = HashCode.Combine(titleHash);
            Hash.Map.Add(TitleHash, titleHash.ToString());
            MinPlayersHash = HashCode.Combine(minPlayers);
            Hash.Map.Add(MinPlayersHash, minPlayers.ToString());
            MaxPlayersHash = HashCode.Combine(maxPlayers);
            Hash.Map.Add(MaxPlayersHash, maxPlayers.ToString());

            foreach (var author in authors)
            {
                int AuthorID = HashCode.Combine(author);
                Hash.AuthorsHash.Add(AuthorID, author);
                List<int> AuthorIds = new List<int>();
                AuthorIds.Add(AuthorID);
            }
        }
        public int? ID
        {
            get { return Id; }

        }
        public int Title
        {
            get
            {
                return TitleHash;
            }
        }
        public int GetMinPlayers
        {
            get
            {
                return MinPlayersHash;
            }
        }
        public int GetMaxPlayers
        {
            get
            {
                return MaxPlayersHash;
            }
        }
        public int GetDifficulty
        {
            get
            {
                return DifficultyHash;
            }
        }
        public List<int> GetAuthorList()
        {
            return AuthorIds;
        }
    }

    public class AuthorHashData : IAuthorHashData
    {
        public int? Id { get; set; }
        public int NameHash { get; set; }
        public int SurnameHash { get; set; }
        public int BirthYearHash { get; set; }
        public int NicknameHash { get; set; }
        public AuthorHashData(string name, string surname, int? birthYear, string nicknameHash = "")
        {
            Id = Hash.ID++;
            NameHash = HashCode.Combine(name);
            if (!Hash.Map.ContainsKey(NameHash))
            {
                Hash.Map.Add(NameHash, name);
            }
            SurnameHash = HashCode.Combine(surname);
            if (!Hash.Map.ContainsKey(SurnameHash))
            {
                Hash.Map.Add(SurnameHash, surname);
            }
            BirthYearHash = HashCode.Combine(birthYear);
            if (!Hash.Map.ContainsKey(BirthYearHash))
            {
                Hash.Map.Add(BirthYearHash, birthYear.ToString());
            }
            if (nicknameHash != "")
            {
                NicknameHash = HashCode.Combine(nicknameHash);
                Hash.Map.Add(NicknameHash, nicknameHash.ToString());

            }
        }

        public int? ID
        {
            get
            {
                return ID;
            }
        }
        public int Name
        {
            get
            {
                return NameHash;
            }
        }

        public int Surname
        {
            get
            {
                return SurnameHash;
            }
        }
        public int? BirthYear
        {
            get
            {
                return BirthYear;
            }
        }
        public int NickName
        {
            get
            {
                return NicknameHash;
            }
        }
    }
}
