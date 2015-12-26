using System;
using System.Collections.Generic;
using System.Linq;

namespace Nanba.PageModel
{
    public class PageBlock
    {
        private string _uid = Guid.NewGuid().ToString();

        private List<IPageBlockComponent> _components = new List<IPageBlockComponent>();

        public string Uid { get { return _uid; } }

        public IEnumerable<IPageBlockComponent> Components
        {
            get { return _components; }
        }

        public void AddComponent(IPageBlockComponent component)
        {
            _components.Add(component);
        }
        public void RemoveComponent(IPageBlockComponent component)
        {
            _components.Remove(component);
        }
        
        public IEnumerable<TPageBlockComponent> GetAll<TPageBlockComponent>()
            where TPageBlockComponent : class, IPageBlockComponent
        {
            return _components.OfType<TPageBlockComponent>();
        }

        public TPageBlockComponent Get<TPageBlockComponent>()
            where TPageBlockComponent : class, IPageBlockComponent
        {
            return _components.FirstOrDefault(c => c is TPageBlockComponent) as TPageBlockComponent;
        }
    }
}