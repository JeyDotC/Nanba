using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.Systems;

namespace Nanba
{
    public class DefaultSystemsResolver : ISystemsResolver
    {
        private static readonly IEnumerable<ISystem> _knownSystems = new ISystem[] {
            new RenderHtmlContentSystem(),
            new LayoutHtmlContentSystem()
        };

        public IEnumerable<ISystem> ResolveFor(string route)
        {
            return _knownSystems;
        }
    }
}
