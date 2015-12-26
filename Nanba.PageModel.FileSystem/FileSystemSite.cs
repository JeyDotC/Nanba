using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.PageModel.FileSystem
{
    public class FileSystemSite : ISite
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                if (!Directory.Exists(value))
                    throw new ArgumentException(string.Format("Directory '{0}' must exist.", value));

                _connectionString = value;
            }
        }

        public FileSystemSite(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public FileSystemSite()
        {

        }

        public string Name { get; set; }

        public IPage Root
        {
            get
            {
                return CreatePage("");
            }
        }

        public IPage CreatePage(string name)
        {
            var fullName = FileSystemPage.GetFullName(ConnectionString, name);
            return new FileSystemPage(new FileInfo(fullName), this)
            {
                Name = name
            };
        }

        public IPage Get(string name)
        {
            return CreatePage(name);
        }
    }
}
