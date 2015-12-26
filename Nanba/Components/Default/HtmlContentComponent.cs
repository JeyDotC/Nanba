using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.PageModel;
using Nancy;

namespace Nanba.Components.Default
{
    public class HtmlContentComponent : IHtmlContentComponent
    {
        public IPage Owner { get; set; }

        public string Contents { get; set; }

        public string Render(Request context)
        {
            return Contents;
        }
    }
}
