using System.Collections.Generic;
using Nanba.PageModel;

namespace Nanba.Components
{
    public interface ITemplateComponent : IPageBlockComponent
    {
        string Render(dynamic _layoutModel);
    }
}