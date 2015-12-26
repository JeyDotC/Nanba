using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nanba.Components;
using Nanba.Components.Default;
using Nanba.PageModel;
using Nanba.PageModel.FileSystem;
using Nanba.Systems;
using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanba.Tests
{
    [TestClass]
    public class SystemsTests
    {
        private static string _dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private static ISite _site = new FileSystemSite(Path.Combine(_dir, "HelloWorldSite"));
        private static IPage _page;
        private static RenderHtmlContentSystem _renderHtml;
        private static LayoutHtmlContentSystem _layoutHtml;
        private static Dictionary<string, object> _context;
        private static Stream _stream;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _renderHtml = new RenderHtmlContentSystem();
            _layoutHtml = new LayoutHtmlContentSystem();
            _context = new Dictionary<string, object>
            {
                {"Request", new Request("GET", "/TestPages/PageWithContents", "http") },
                {"Response", new Response() }
            };

            _page = _site.CreatePage("TestPages/PageWithContents");
            if (_page.Exists)
                return;

            _page.AddBlock(b => b
                    .With(new SuperSimpleViewEngineTemplateComponent
                    {
                        TemplateLocation = Path.Combine(_dir, "Templates", "Site.html")
                    })
                );
            _page.AddBlock(b => b
                    .With(new HtmlContentComponent
                    {
                        Contents = "<h1>Hello!</h1> <p>This content and all it's siblings will end up in the default slot!</p>",
                    })
                    .With(new HtmlContentComponent
                    {
                        Contents = "<p>Me and my bro above are in the default slot!</p>"
                    })
                );
            _page.AddBlock(b => b
                    .With(new HtmlContentComponent
                    {
                        Contents = @"<ul>
    <li>
        <h2>I am the Menu!</h2>
        <p>And I end up in the Menu Slot</p>
    </li>
    <li><a href=""#"">Link1</a></li>
    <li><a href=""#"">Link2</a></li>
    <li><a href=""#"">Link3</a></li>
    <li><a href=""#"">Link4</a></li>
</ul>"
                    })
                    .With(new BlockLayoutComponent
                    {
                        Slot = "MainMenu"
                    })
                );
            _page.AddBlock(b => b
                    .With(new HtmlContentComponent
                    {
                        Contents = @"<p>I'm from the sewers, I live at the bottom!</p>"
                    })
                    .With(new BlockLayoutComponent
                    {
                        Slot = "Footer"
                    })
                );

            _page.Save();

        }

        [TestMethod]
        public void RenderBlocks()
        {
            _renderHtml.Start(_context);

            foreach (var block in _page.Blocks)
                _renderHtml.Visit(block, _context);

            _renderHtml.End(_context);
            var contents = _context["RenderHtmlContentSystem.Contents"] as IDictionary<string, IEnumerable<string>>;

            Assert.AreEqual(3, contents.Count());
        }

        [TestMethod]
        public void AssembleLayout()
        {
            _renderHtml.Start(_context);
            _layoutHtml.Start(_context);

            foreach (var block in _page.Blocks)
            {
                _renderHtml.Visit(block, _context);
                _layoutHtml.Visit(block, _context);
            }

            _renderHtml.End(_context);
            _layoutHtml.End(_context);
        }
    }
}
