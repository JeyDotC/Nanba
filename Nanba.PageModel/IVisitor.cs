using System.Collections.Generic;

namespace Nanba.PageModel
{
    public interface IVisitor
    {
        void Visit(PageBlock block, IDictionary<string, object> context);
    }
}