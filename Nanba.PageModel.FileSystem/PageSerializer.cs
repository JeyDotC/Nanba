using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel.FileSystem
{
    internal class PageSerializer
    {
        static PageSerializer()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
        }

        public PageSerializer() { }

        public string SerializePage(IPage page)
        {
           
            return JsonConvert.SerializeObject(page.Blocks);
        }

        public IEnumerable<PageBlock> DeserializePage(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<PageBlock>>(json);
        }
    }
    
    internal class BlockSpecification
    {
        public IEnumerable<ObjectSpecification> Components { get; set; }
    }

    internal class PageSpecification
    {
        public IEnumerable<BlockSpecification> Blocks { get; set; }
    }

    internal class ObjectSpecification
    {
        public string ObjectType { get; set; }
        public string ObjectData { get; set; }

        public T Deserialize<T>()
        {
            return (T)JsonConvert.DeserializeObject(ObjectData, Type.GetType(ObjectType));
        }

        public ObjectSpecification() { }
        public ObjectSpecification(object target) {
            ObjectType = target.GetType().AssemblyQualifiedName;
            ObjectData = JsonConvert.SerializeObject(target);
        }
    }
}
