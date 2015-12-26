using Nanba.PageModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel
{
    public static class PageMethodExtensions
    {
        public static IPage AddBlock(this IPage self, Action<PageBlock> configure)
        {
            var newPageBlock = new PageBlock();
            configure(newPageBlock);
            self.AddBlock(newPageBlock);
            return self;
        }

        public static IPage FindPage(this ISite self, string route)
        {
            if(route == null)
                return self.Root;

            var routeParts = route.Split('/');
            var current = self.Root;

            foreach (var part in routeParts)
            {
                current = current.Children.FirstOrDefault(p => p.Name == part);
                if (current == null)
                    throw new PageNotFoundException();
            }

            return current;

        }
    }
}
