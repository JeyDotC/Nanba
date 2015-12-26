using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel
{
    public interface ISite
    {
        string Name { get; set; }

        IPage Root { get; }

        IPage CreatePage(string name);

        IPage Get(string name);

        string ConnectionString { get; set; }
    }
}
