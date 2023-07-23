using Bajtpik.Data.Interfaces;
using System.Text;

namespace Bajtpik.Data
{
    public class BookListOfTupleAdapter : IBook
    {
        private BookListOfTuple Adaptee;

        public BookListOfTupleAdapter(BookListOfTuple Adaptee)
        {
            this.Adaptee = Adaptee;
        }
        public string Title
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Title")
                        return (string)item.Item2;
                }
                return null;
            }
        }
        public int? Year
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Year")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public int? PageCount
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "PageCount")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public List<IAuthor> GetAuthors()
        {
            foreach (var item in Adaptee.List)
            {
                if (item.Item1 == "Authors")
                    return (List<IAuthor>)item.Item2;
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).Append(" ").Append(Year).Append(" ").Append(PageCount).Append(", ");
            foreach (var author in GetAuthors())
            {
                stringBuilder.Append(author.ToString());
            }
            return stringBuilder.ToString();
        }
        public (object?, string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    return (Title, "string");

                case "year":
                    return (Year, "int");

                case "pagecount":
                    return (PageCount, "int");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            return;
        }
    }
    public class NewspaperListOfTupleAdapter : INewspaper
    {
        private NewspaperListOfTuple Adaptee;

        public NewspaperListOfTupleAdapter(NewspaperListOfTuple Adaptee)
        {
            this.Adaptee = Adaptee;
        }
        public string Title
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Title")
                        return (string)item.Item2;
                }
                return null;
            }

        }
        public int? Year
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Year")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public int? PageCount
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "PageCount")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).Append(" ").Append(Year).Append(" ").Append(PageCount);
            return stringBuilder.ToString();
        }
        public (object?, string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    return (Title, "string");

                case "year":
                    return (Year, "int");

                case "pagecount":
                    return (PageCount, "int");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            return;
        }
    }
    public class BoardgameListOfTupleAdapter : IBoardGame
    {
        private BoardgameListOfTuple Adaptee;
        public BoardgameListOfTupleAdapter(BoardgameListOfTuple adaptee)
        {
            Adaptee = adaptee;
        }
        public string? Title
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Title")
                        return (string)item.Item2;
                }
                return null;
            }
        }
        public int? MinPlayers
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "MinPlayers")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public int? MaxPlayers
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "MaxPlayers")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public int? Difficulty
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Difficulty")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public List<IAuthor> Authors
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Authors")
                        return (List<IAuthor>)item.Item2;
                }
                return null;
            }

        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Title).Append(" ").Append(MinPlayers).Append(" ").Append(MaxPlayers).Append(" ").Append(Difficulty);
            foreach (var auth in Authors)
            {
                stringBuilder.Append(auth.ToString());
            }
            return stringBuilder.ToString();
        }
        public (object?, string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    return (Title, "string");

                case "minplayers":
                    return (MinPlayers, "int");

                case "maxplayers":
                    return (MaxPlayers, "int");

                case "difficulty":
                    return (Difficulty, "int");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            return;
        }

    }
    public class AuthorListOfTupleAdapter : IAuthor
    {
        private AuthorListOfTuple Adaptee;
        public AuthorListOfTupleAdapter(AuthorListOfTuple adaptee)
        {
            Adaptee = adaptee;
        }
        public string? Name
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Name")
                        return (string)item.Item2;
                }
                return null;
            }
        }
        public string? Surname
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "Surname")
                        return (string)item.Item2;
                }
                return null;
            }
        }
        public int? BirthYear
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "BirthYear")
                        return (int)item.Item2;
                }
                return null;
            }
        }
        public string NickName
        {
            get
            {
                foreach (var item in Adaptee.List)
                {
                    if (item.Item1 == "NickName")
                        return (string)item.Item2;
                }
                return null;
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Name).Append(" ").Append(Surname).Append(" ").Append(BirthYear).Append(" ").Append(NickName);
            return stringBuilder.ToString();
        }
        public (object?, string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "name":
                    return (Name, "string");

                case "surname":
                    return (Surname, "string");

                case "birthyear":
                    return (BirthYear, "int");
                case "nickname":
                    return (NickName, "string");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            return;
        }

    }
    public class BookListOfTuple : IEntity
    {
        public List<Tuple<string, object>> List = new List<Tuple<string, object>>();

        public BookListOfTuple(string Title, int Year, int Pagecount, List<IAuthor> authors)
        {

            List.Add(new Tuple<string, object>("Title", Title));
            List.Add(new Tuple<string, object>("Authors", authors));
            List.Add(new Tuple<string, object>("Year", Year));
            List.Add(new Tuple<string, object>("PageCount", Pagecount));
        }
        public (object?, string) GetProperty(string propertyName)
        {
            BookListOfTupleAdapter adapter = new BookListOfTupleAdapter(this);
            switch (propertyName.ToLower())
            {
                case "title":
                    return adapter.GetProperty("title");

                case "year":
                    return adapter.GetProperty("year");

                case "pagecount":
                    return adapter.GetProperty("pagecount");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            foreach (var item in List)
            {
                if (item.Item1 == propertyName)
                {
                    List.Remove(item);
                    Tuple<string, object> added = new Tuple<string, object>(propertyName, value);
                    List.Add(added);
                    return;
                }
            }
        }
    }
    public class NewspaperListOfTuple : IEntity
    {
        public List<Tuple<string, object>> List = new List<Tuple<string, object>>();
        public NewspaperListOfTuple(string title, int Year, int Pagecount)
        {

            List.Add(new Tuple<string, object>("Title", title));
            List.Add(new Tuple<string, object>("Year", Year));
            List.Add(new Tuple<string, object>("PageCount", Pagecount));

        }
        public (object?, string) GetProperty(string propertyName)
        {
            NewspaperListOfTupleAdapter adapter = new NewspaperListOfTupleAdapter(this);
            switch (propertyName.ToLower())
            {
                case "title":
                    return adapter.GetProperty("title");

                case "year":
                    return adapter.GetProperty("year");

                case "pagecount":
                    return adapter.GetProperty("pagecount");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            foreach (var item in List)
            {
                if (item.Item1 == propertyName)
                {
                    List.Remove(item);
                    Tuple<string, object> added = new Tuple<string, object>(propertyName, value);
                    List.Add(added);
                    return;
                }
            }
        }
    }
    public class BoardgameListOfTuple : IEntity
    {
        public List<Tuple<string, object>> List = new List<Tuple<string, object>>();
        public BoardgameListOfTuple(string title, int? MinPlayers, int? MaxPlayers, int Difficulty, List<IAuthor> authors)
        {
            List.Add(new Tuple<string, object>("Title", title));
            List.Add(new Tuple<string, object>("MinPlayers", MinPlayers));
            List.Add(new Tuple<string, object>("MaxPlayers", MaxPlayers));
            List.Add(new Tuple<string, object>("Authors", authors));
        }
        public (object?, string) GetProperty(string propertyName)
        {
            BoardgameListOfTupleAdapter adapter = new BoardgameListOfTupleAdapter(this);
            switch (propertyName.ToLower())
            {
                case "title":
                    return adapter.GetProperty("title");

                case "minplayers":
                    return adapter.GetProperty("minplayers");

                case "maxplayers":
                    return adapter.GetProperty("maxplayers");
                case "difficulty":
                    return adapter.GetProperty("difficulty");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            foreach (var item in List)
            {
                if (item.Item1 == propertyName)
                {
                    List.Remove(item);
                    Tuple<string, object> added = new Tuple<string, object>(propertyName, value);
                    List.Add(added);
                    return;
                }
            }
        }

    }
    public class AuthorListOfTuple : IEntity
    {
        public List<Tuple<string, object>> List = new List<Tuple<string, object>>();
        public AuthorListOfTuple(string name, string surname, int BirthYear, string Nickname)
        {
            List.Add(new Tuple<string, object>("Name", name));
            List.Add(new Tuple<string, object>("Surname", surname));
            List.Add(new Tuple<string, object>("BirthYear", BirthYear));
            List.Add(new Tuple<string, object>("Nickname", Nickname));
        }
        public (object?, string) GetProperty(string propertyName)
        {
            AuthorListOfTupleAdapter adapter = new AuthorListOfTupleAdapter(this);
            switch (propertyName.ToLower())
            {
                case "name":
                    return adapter.GetProperty("name");

                case "surname":
                    return adapter.GetProperty("surname");

                case "birthyear":
                    return adapter.GetProperty("birthyear");
                case "nickname":
                    return adapter.GetProperty("nickname");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null, "");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            foreach (var item in List)
            {
                if (item.Item1 == propertyName)
                {
                    List.Remove(item);
                    Tuple<string, object> added = new Tuple<string, object>(propertyName, value);
                    List.Add(added);
                    return;
                }
            }
        }
    }
}
