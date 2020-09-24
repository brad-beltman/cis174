using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module4.TagHelpers
{
    public class SubmitButtonTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("type", "submit");

            string newClasses = "btn btn-dark";
            string oldClasses = output.Attributes["class"]?.Value?.ToString();
            string classes = (string.IsNullOrEmpty(oldClasses)) ? newClasses : $"{oldClasses} {newClasses}";

            output.Attributes.SetAttribute("class", classes);
        }
    }
}
