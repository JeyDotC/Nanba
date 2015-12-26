using System;
using Nanba.PageModel;

namespace Nanba.Components
{
    public class BlockLayoutComponent : IPageBlockComponent
    {
        public IPage Owner { get; set; }

        public string Slot { get; set; }
    }
}