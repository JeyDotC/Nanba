using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.PageModel;
using Nancy;
using System.IO;
using Newtonsoft.Json;

namespace Nanba
{
    class DefaultSitesResolver : ISitesResolver
    {
        private IRootPathProvider _paths;

        public DefaultSitesResolver(IRootPathProvider paths)
        {
            _paths = paths;
        }

        public IEnumerable<ISite> ResolveSites()
        {
            var root = _paths.GetRootPath();
            var sitesFile = Path.Combine(root, "_sites", "sites.json");

            if(!File.Exists(sitesFile))
            return new ISite[] { };

            var sitesJson = File.ReadAllText(sitesFile);

            return JsonConvert.DeserializeObject<IEnumerable<ISite>>(sitesJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }
}
