using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajtpik.Data.Interfaces
{
    public interface INewspaper : IEntity
    {
        string Title { get; }
        int? Year { get; }
        int? PageCount { get; }
    }
}
