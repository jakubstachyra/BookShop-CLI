using Bajtpik.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajtpik.Data.Builders
{
    public class NewspaperBuilder : IBuilder
    {
        protected string title;
        protected int year;
        protected int pageCount;

        public IEntity Build()
        {
            return new Newspaper(title, year, pageCount);
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
    public class NewspaperListOfTupleBuilder: NewspaperBuilder
    {
        public new IEntity Build()
        {
            return new NewspaperListOfTuple(title, year, pageCount);
        }
    }
}
