using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel
{
    public interface IPageBlockComponent
    {
        IPage Owner { get; set; }
    }
}
