using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanba.PageModel;

namespace Nanba.Components.Default
{
    class JustStackItTemplateComponent : ITemplateComponent
    {
        public IPage Owner { get; set; }

        public string Render(dynamic layoutModel)
        {
            var model = layoutModel as ICollection<KeyValuePair<string, object>>;

            var stringBuilder = new StringBuilder(_heading);

            foreach(var slot in model)
            {
                stringBuilder
                    .AppendLine()
                    .AppendFormat(@"<div id=""{0}"">", slot.Key);

                foreach (var content in slot.Value as IEnumerable<string>)
                    stringBuilder
                        .AppendLine()
                        .AppendFormat(@"<div>{0}</div>", content);

                stringBuilder.AppendLine("</div>");
            }

            stringBuilder.AppendLine(_footer);

            return stringBuilder.ToString();
        }

        private const string _heading = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
    <title></title>
</head>
<body>";
        private const string _footer = @"</body>
</html>";

    }
}
