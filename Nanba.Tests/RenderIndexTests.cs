using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Nanba.PageModel;
using Nanba.PageModel.FileSystem;
using Nanba.Components.Default;
using System.Linq;

namespace Nanba.Tests
{
    [TestClass]
    public class RenderIndexTests
    {
        private static string _dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private ISite _site = new FileSystemSite(Path.Combine(_dir, "HelloWorldSite"));

        [TestMethod]
        public void SavePageInHelloWorldSite()
        {
            var page = _site.CreatePage("TestPages/" + Guid.NewGuid().ToString());
            var mainContentBlock = new PageBlock();
            mainContentBlock.AddComponent(new HtmlContentComponent
            {
                Contents = "<div> <strong>XD</strong> </div>"
            });
            mainContentBlock.AddComponent(new SuperSimpleViewEngineTemplateComponent
            {
                TemplateLocation = Path.Combine(_dir, "Template.html")
            });
            page.AddBlock(mainContentBlock);
            page.Save();
        }

        [TestMethod]
        public void ListPagesFromHelloWorldSite()
        {
            var children = _site.Root.Children.Select(p => p.Name);

            Assert.AreEqual(2, children.Count());
            Assert.IsTrue(children.Any(n => n == "/Index"));
        }

        [TestMethod]
        public void AddChildToIndex()
        {
            var page = _site.CreatePage("Index/" + Guid.NewGuid().ToString());
            var mainContentBlock = new PageBlock();
            mainContentBlock.AddComponent(new HtmlContentComponent
            {
                Contents = "<div> <strong>I'm a index child</strong> </div>"
            });
            mainContentBlock.AddComponent(new SuperSimpleViewEngineTemplateComponent
            {
                TemplateLocation = Path.Combine(_dir, "Template.html")
            });
            page.AddBlock(mainContentBlock);
            page.Save();
        }
    }
}
