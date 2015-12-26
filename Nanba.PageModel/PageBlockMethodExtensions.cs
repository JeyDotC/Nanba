using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel
{
    public static class PageBlockMethodExtensions
    {
        public static PageBlock With(this PageBlock self, IPageBlockComponent component)
        {
            self.AddComponent(component);
            return self;
        }
    }
}
