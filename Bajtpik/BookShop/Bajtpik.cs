using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bajtpik.Data.Interfaces;

namespace Bajtpik.Data
{
    public class Book : IBook
    {

        public string Title { get; set; }
        public List<IAuthor> Authors { get; set; }
        public int? Year { get; set; }
        public int? PageCount { get; set; }
        public Book(string title, int year, int pagecount, List<IAuthor> Author)
        {
            Title = title;
            List<IAuthor> Authors = new List<IAuthor>();
            Authors = Author;
            Year = year;
            PageCount = pagecount;

        }
        public List<IAuthor> GetAuthors()
        {
            return Authors;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Title).Append(" ").Append(Year).Append(" ").Append(PageCount).Append(" ");
            if(Authors != null)
            {
                foreach (var author in Authors)
                {
                    sb.Append(author.ToString);
                }
            }
            return sb.ToString();
        }
        public (object?,string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    return (Title,"string");

                case "year":
                    return (Year,"int");

                case "pagecount":
                    return (PageCount,"int");


                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null,"");
            }
        }
        public void SetProperty(string propertyName, object value)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    Title = (string)value;
                    break;

                case "year":
                    Year = (int)value;
                    break;

                case "pagecount":
                    PageCount = (int)value;
                    break;

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    break;
            }
        }
    }
    public class Newspaper : INewspaper
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public int? PageCount { get; set; }
        public Newspaper(string title, int year, int pagecount)
        {
            Title = title;
            Year = year;
            PageCount = pagecount;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Title).Append(" ").Append(Year).Append(" ").Append(PageCount);
            return sb.ToString();
        }
        public (object?,string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    return (Title,"string");

                case "year":
                    return (Year,"int");

                case "pagecount":
                    return (PageCount,"int");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null,"");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    Title = (string)value;
                    break;

                case "pagecount":
                    PageCount = (int)value;
                    break;

                case "year":
                    Year = (int)value;
                    break;

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    break;
            }
        }
    }

    public class BoardGame : IBoardGame
    {
        public string? Title { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public int? Difficulty { get; set; }
        public List<IAuthor> Authors { get; set; }
        public BoardGame(string title, int? minplayers, int? maxplayers, int? difficulty, List<IAuthor> authors)
        {
            Title = title;
            MinPlayers = minplayers;
            MaxPlayers = maxplayers;
            Difficulty = difficulty;
            Authors = authors;
        }
        public (object?,string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "title":
                    return (Title,"string");

                case "minplayers":
                    return (MinPlayers,"int");

                case "maxplayers":
                    return (MaxPlayers,"int");
                case "difficulty":
                    return (Difficulty,"int");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null,"");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            switch (propertyName.ToLower())
            {
                case "name":
                    Title = (string)value;
                    break;

                case "difficulty":
                    Difficulty = (int)value;
                    break;

                case "minplayers":
                    MinPlayers = (int)value;
                    break;

                case "maxplayers":
                    MaxPlayers = (int)value;
                    break;

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    break;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Title).Append(" ").Append(MinPlayers).Append(" ").Append(MaxPlayers).Append(" ").Append(Difficulty);
            return sb.ToString();
        }
    }

    public class Author : IAuthor
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? BirthYear { get; set; }
        public string? NickName { get; set; }
        public Author(string name, string surname, int birthyear, string nickname = "")
        {
            Name = name;
            Surname = surname;
            BirthYear = birthyear;
            NickName = nickname;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name).Append(" ").Append(Surname).Append(" ").Append(BirthYear).Append(" ").Append(NickName).Append("\n");
            return sb.ToString();
        }
        public (object?,string) GetProperty(string propertyName)
        {
            switch (propertyName.ToLower())
            {
                case "name":
                    return (Name,"string");

                case "surname":
                    return (Surname,"string");
                    
                case "birthyear":
                    return (BirthYear,"int");

                case "nickname":
                    return (NickName,"string");

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    return (null,"");
            }
        }
        public void SetProperty(string propertyName, object? value)
        {
            switch (propertyName.ToLower())
            {
                case "name":
                    Name = (string)value;
                    break;

                case "surname":
                    Surname = (string)value;
                    break;

                case "nickname":
                    NickName = (string)value;
                    break;

                case "birthyear":
                    BirthYear = (int)value;
                    break;

                default:
                    Console.WriteLine("Unknown field: " + propertyName);
                    break;
            }
        }
    }
}
