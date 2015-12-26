using Nanba.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.Applications
{
    public interface INanbaApplication
    {
        string Name { get; }

        IEnumerable<ISystem> Systems { get; }
    }
}
