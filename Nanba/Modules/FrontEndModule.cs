using Nanba.PageModel;
using Nanba.PageModel.Exceptions;
using Nancy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.Modules
{
    public class FrontEndModule : NancyModule
    {
        private static IDictionary<string, ISite> _sites;
        private ISystemsResolver _systemsResolver;

        public FrontEndModule(ISitesResolver sites, ISystemsResolver systemsResolver)
        {
            _sites = sites.ResolveSites().ToDictionary(s => s.Name);
            _systemsResolver = systemsResolver;

            var baseUrl = string.Format(@"/(?<Site>{0})", string.Join("|", _sites.Keys));

            Get[baseUrl] = ServePage;

            Get[baseUrl + "/{Page*}"] = ServePage;
        }


        dynamic ServePage(dynamic parameters)
        {
            if (!_sites.ContainsKey(parameters.Site))
                return 404;

            var site = _sites[parameters.Site] as ISite;
            var route = (string)parameters.Page;

            var page = site.Get(route);

            if (!page.Exists)
                return 404;

            var systems = _systemsResolver.ResolveFor(route);
            var context = new ConcurrentDictionary<string, object>();
            context["Request"] = Request;
            context["Response"] = (Response)200;
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };

            Parallel.ForEach(
                systems, parallelOptions,
                s => s.Start(context)
            );

            Parallel.ForEach(
                systems, parallelOptions,
                s =>
                {
                    foreach (var block in page.Blocks)
                        s.Visit(block, context);
                }
            );

            Parallel.ForEach(
                systems, parallelOptions,
                s => s.End(context)
            );

            return context["Response"] as Response;
        }
    }
}
