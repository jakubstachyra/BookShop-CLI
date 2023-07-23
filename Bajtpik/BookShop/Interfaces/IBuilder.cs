using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajtpik.Data.Interfaces
{
    public interface IBuilder
    {
        IEntity Build();
        bool SetField(string fieldName, string value);
        List<string> GetFieldNames();
    }
}
