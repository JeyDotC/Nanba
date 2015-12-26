using Nanba.PageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba
{
    public interface ISitesResolver
    {
        IEnumerable<ISite> ResolveSites();
    }
}
