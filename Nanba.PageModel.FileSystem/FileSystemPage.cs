using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel.FileSystem
{
    public class FileSystemPage : IPage
    {
        private FileInfo _file;
        private List<PageBlock> _blocks = new List<PageBlock>();
        private FileSystemSite _root;
        private bool _blocksLoaded = false;

        internal static string GetFullName(string root, string name)
        {
            return Path.Combine(
                    new string[] { root }.Union((name + ".json").Split('/')).ToArray()
            );
        }

        internal FileSystemPage(FileInfo file, FileSystemSite root)
        {
            _file = file;
            _root = root;
        }

        public IEnumerable<PageBlock> Blocks
        {
            get
            {
                LoadBlocksIfNotLoaded();
                return _blocks;
            }
        }

        public IEnumerable<IPage> Children
        {
            get
            {
                return _file
                     .Directory
                     .EnumerateFileSystemInfos()
                     .Where(f => (
                        (f.Attributes & FileAttributes.Directory) != FileAttributes.Directory || 
                        (f.Attributes & FileAttributes.Directory) == FileAttributes.Directory && !File.Exists(f.FullName + ".json")
                    ))
                     .Select(f =>
                     {
                         var pageName = Path.ChangeExtension(f.FullName, null)
                                    .Replace(_root.ConnectionString, "")
                                    .Replace("\\", "/");
                         var fileInfo =
                         (f.Attributes & FileAttributes.Directory) == FileAttributes.Directory && !File.Exists(f.FullName + ".json") ?
                             new FileInfo(f.FullName + ".json")
                         :
                             new FileInfo(f.FullName);

                         return new FileSystemPage(fileInfo, _root)
                         {
                             Name = pageName
                         };
                     });
            }
        }

        public string Name { get; set; }

        public ISite Site
        {
            get
            {
                return _root;
            }
        }

        public bool Exists
        {
            get { return File.Exists(_file.FullName); }
        }

        public void AddBlock(PageBlock block)
        {
            LoadBlocksIfNotLoaded();
            _blocks.Add(block);
        }

        public void ClearBlocks()
        {
            _blocks.Clear();
        }

        public void RemoveBlock(PageBlock block)
        {
            LoadBlocksIfNotLoaded();
            _blocks.Remove(block);
        }

        public void Save()
        {
            LoadBlocksIfNotLoaded();

            var serialized = new PageSerializer().SerializePage(this);

            var fullName = GetFullName(_root.ConnectionString, Name);

            var file = _file;

            if (_file.FullName != fullName)
                file = new FileInfo(fullName);

            if (!file.Directory.Exists)
                file.Directory.Create();

            File.WriteAllText(file.FullName, serialized);

            if (_file.FullName != fullName)
            {
                _file.Delete();
                _file = file;
            }

        }

        public void Remove(bool deep = false)
        {
            _file.Delete();

            if (deep)
                ClearChildren();
        }

        private void ClearChildren()
        {
            var childrenFolder = _file.FullName.Replace(".json", "");
            if (Directory.Exists(childrenFolder))
                Directory.Delete(childrenFolder);
        }

        private void LoadBlocksIfNotLoaded()
        {
            if (!_blocksLoaded)
            {
                if (Exists)
                {
                    var serializedBlocks = File.ReadAllText(_file.FullName);
                    _blocks = new PageSerializer().DeserializePage(serializedBlocks).ToList();
                }

                _blocksLoaded = true;
            }
        }
    }
}
