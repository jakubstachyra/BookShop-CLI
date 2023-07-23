using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
