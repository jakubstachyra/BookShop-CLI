using Bajtpik.Data.Interfaces;

namespace Bajtpik.Data.Builders
{
    public class BookBuilder : IBuilder
    {
        protected string title;
        protected int year;
        protected int pageCount;

        public IEntity Build()
        {
            return new Book(title, year, pageCount, null);
        }

        public bool SetField(string fieldName, string value)
        {
            switch (fieldName.ToLower())
            {
                case "title":
                    title = value;
                    return true;

                case "year":
                    if (int.TryParse(value, out int yearValue))
                    {
                        year = yearValue;
                        return true;
                    }
                    break;

                case "pagecount":
                    if (int.TryParse(value, out int pageCountValue))
                    {
                        pageCount = pageCountValue;
                        return true;
                    }
                    break;
            }

            return false;
        }
        public List<string> GetFieldNames()
        {
            return new List<string> { "title", "year", "pageCount" };
        }
    }
    public class BookListOfTupleBuilder : BookBuilder
    {
        public new IEntity Build()
        {
            return new BookListOfTuple(title, year, pageCount, null);
        }
    }
}
