using Nanba.PageModel;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.Components
{
    public interface IHtmlContentComponent : IPageBlockComponent
    {
        string Render(Request context);
        
    }
    
}
