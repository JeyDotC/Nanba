using Nanba.PageModel;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.Systems
{
    public interface ISystem : IVisitor
    {
        void Start(IDictionary<string, object> context);

        void End(IDictionary<string, object> context);
    }
}
