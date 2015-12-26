﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.PageModel;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using System.IO;

namespace Nanba.Components.Default
{
    public class SuperSimpleViewEngineTemplateComponent : ITemplateComponent
    {
        private SuperSimpleViewEngine _viewEngine = new SuperSimpleViewEngine();
        private IViewEngineHost _viewEngineHost = new FakeViewEngineHost();

        public IPage Owner { get; set; }

        public string TemplateLocation { get; set; }
        
        public string Render(dynamic _layoutModel)
        {
            var templateContents = File.ReadAllText(TemplateLocation);

            return _viewEngine.Render(templateContents, _layoutModel, _viewEngineHost);
        }

        public class FakeViewEngineHost : IViewEngineHost
        {
            public Func<string, object, string> GetTemplateCallback { get; set; }
            public Func<string, string> ExpandPathCallBack { get; set; }

            public FakeViewEngineHost()
            {
                Context = new FakeContext { Name = "Frank" };
            }

            /// <summary>
            /// Html "safe" encode a string
            /// </summary>
            /// <param name="input">Input string</param>
            /// <returns>Encoded string</returns>
            public string HtmlEncode(string input)
            {
                return input.Replace("&", "&amp;").
                    Replace("<", "&lt;").
                    Replace(">", "&gt;").
                    Replace("\"", "&quot;");
            }

            /// <summary>
            /// Get the contenst of a template
            /// </summary>
            /// <param name="templateName">Name/location of the template</param>
            /// <param name="model">Model to use to locate the template via conventions</param>
            /// <returns>Contents of the template, or null if not found</returns>
            public string GetTemplate(string templateName, object model)
            {
                return GetTemplateCallback != null ? GetTemplateCallback.Invoke(templateName, model) : string.Empty;
            }

            /// <summary>
            /// Gets a uri string for a named route
            /// </summary>
            /// <param name="name">Named route name</param>
            /// <param name="parameters">Parameters to use to expand the uri string</param>
            /// <returns>Expanded uri string, or null if not found</returns>
            public string GetUriString(string name, params string[] parameters)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Expands a path to include any base paths
            /// </summary>
            /// <param name="path">Path to expand</param>
            /// <returns>Expanded path</returns>
            public string ExpandPath(string path)
            {
                return ExpandPathCallBack != null ? ExpandPathCallBack.Invoke(path) : path;
            }

            public string AntiForgeryToken()
            {
                return "CSRF";
            }

            private class FakeContext
            {
                public FakeContext()
                {
                    User = new FakeUser { Username = "Frank123" };
                }

                public string Name { get; set; }

                public FakeUser User { get; set; }

                public class FakeUser
                {
                    public string Username { get; set; }
                }
            }

            /// <summary>
            /// Context object of the host application.
            /// </summary>
            /// <value>An instance of the context object from the host.</value>
            public object Context { get; set; }
        }
    }
}
