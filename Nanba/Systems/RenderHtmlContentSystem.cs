using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.PageModel;
using Nanba.Components;
using Nancy;
using System.IO;

namespace Nanba.Systems
{
    public class RenderHtmlContentSystem : ISystem
    {
        private Request _request;

        private IDictionary<string, IEnumerable<string>> _renderedBlocks;
             
        public void Start(IDictionary<string, object> context)
        {
            _request = context["Request"] as Request;
            _renderedBlocks = new Dictionary<string, IEnumerable<string>>();
        }

        public void End(IDictionary<string, object> context) {
            context["RenderHtmlContentSystem.Contents"] = _renderedBlocks;
        }

        public void Visit(PageBlock block, IDictionary<string, object> context)
        {
            var contents = block.GetAll<IHtmlContentComponent>().Select(c => c.Render(_request));

            if (contents.Count() > 0)
                _renderedBlocks[block.Uid] = contents;
        }
    }
}
