using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba
{
    public class NanbaStartup : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            TinyIoCContainer.Current.Register<ISitesResolver, DefaultSitesResolver>().AsSingleton();
        }
    }
}
