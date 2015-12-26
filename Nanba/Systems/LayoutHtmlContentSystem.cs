using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.PageModel;
using Nancy;
using Nanba.Components;
using System.IO;
using System.Dynamic;
using Nanba.Components.Default;

namespace Nanba.Systems
{
    public class LayoutHtmlContentSystem : ISystem
    {
        private IDictionary<string, List<string>> _layoutModelAssemble = new Dictionary<string, List<string>>();

        private ITemplateComponent _template;

        public void Start(IDictionary<string, object> context) { }

        public void End(IDictionary<string, object> context)
        {
            var dynamicData = new ExpandoObject();
            var layoutModel = (ICollection<KeyValuePair<string, object>>)dynamicData;

            var contents = context["RenderHtmlContentSystem.Contents"] as IDictionary<string, IEnumerable<string>>;
            
            foreach (var slotDefinition in _layoutModelAssemble)
                layoutModel.Add(new KeyValuePair<string, object>
                (
                    slotDefinition.Key, slotDefinition.Value.SelectMany(uid => contents.ContainsKey(uid) ? contents[uid] : new string[] { })
                ));

            var page = (_template ?? new JustStackItTemplateComponent()).Render(dynamicData);
            var response = context["Response"] as Response;

            response.Contents += stream =>
            {
                using (var writer = new StreamWriter(stream))
                    writer.WriteLine(page);
            };
        }

        public void Visit(PageBlock block, IDictionary<string, object> context)
        {
            var template = block.Get<ITemplateComponent>();

            if (template != null)
                _template = template;

            var layout = block.Get<BlockLayoutComponent>();

            AddBlockContentsToSlot(layout != null ? layout.Slot : "Default", block.Uid);
        }

        private void AddBlockContentsToSlot(string slot, string blockUid)
        {
            if (!_layoutModelAssemble.ContainsKey(slot))
                _layoutModelAssemble[slot] = new List<string> { blockUid };
            else
                _layoutModelAssemble[slot].Add(blockUid);
        }
    }
}
