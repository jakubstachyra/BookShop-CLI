using Bajtpik.Data.Interfaces;

namespace Bajtpik.Data.Builders
{
    public class AuthorBuilder : IBuilder
    {
        protected string name;
        protected string surname;
        protected int birthYear;
        protected string nickname;

        public IEntity Build()
        {
            return new Author(name, surname, birthYear, nickname);
        }

        public bool SetField(string fieldName, string value)
        {
            switch (fieldName.ToLower())
            {
                case "name":
                    name = value;
                    return true;

                case "surname":
                    surname = value;
                    return true;

                case "birthyear":
                    if (int.TryParse(value, out int birthYearValue))
                    {
                        birthYear = birthYearValue;
                        return true;
                    }
                    break;

                case "nickname":
                    nickname = value;
                    return true;
            }

            return false;
        }

        public List<string> GetFieldNames()
        {
            return new List<string> { "name", "surname", "birthYear", "nickname" };
        }
    }
    public class AuthorListOfTupleBuilder : AuthorBuilder
    {
        public new IEntity Build()
        {
            return new AuthorListOfTuple(name, surname, birthYear, nickname);
        }
    }
}
