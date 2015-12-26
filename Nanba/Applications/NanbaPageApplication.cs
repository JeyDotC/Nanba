using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.Systems;

namespace Nanba.Applications
{
    public class NanbaPageApplication : INanbaApplication
    {
        private IEnumerable<ISystem> _systems = new ISystem[]
        {
            new RenderHtmlContentSystem(),
            new LayoutHtmlContentSystem(),
        };

        public string Name
        {
            get { return "Nanba Page"; }
        }

        public IEnumerable<ISystem> Systems
        {
            get
            {
                return _systems;
            }
        }
    }
}
