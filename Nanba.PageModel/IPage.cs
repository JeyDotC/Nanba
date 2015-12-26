using System.Collections.Generic;

namespace Nanba.PageModel
{
    public interface IPage
    {
        ISite Site { get; }

        string Name { get; set; }

        bool Exists { get; }

        IEnumerable<PageBlock> Blocks { get; }

        IEnumerable<IPage> Children { get; }

        void AddBlock(PageBlock block);

        void RemoveBlock(PageBlock block);

        void ClearBlocks();

        void Save();

        void Remove(bool deep = false);
    }
}