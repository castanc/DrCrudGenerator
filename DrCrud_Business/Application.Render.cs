using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public partial class Application
    {

        public string RenderNavBarOption(string option)
        {
            string html = $@"
<li class='nav - item active'>
        < a class='nav-link' href='#'>Home<span class='sr-only'>{option}</span></a>
</li>
";
            return html;
        }
        public string RenderNavBar(View v)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(RenderNavBarOption("(current)"));
            foreach (var item in v.Options)
            {
                sb.AppendLine(RenderNavBarOption(item.Text));
            }

            string html = $@"
<nav class='navbar navbar-dark  indigo darken-2'>

    < !--Navbar brand-- >
    < a class='navbar-brand' href='#'>{this.Name}</a>

    <!-- Collapse button -->
    <button class='navbar-toggler third-button' type='button' data-toggle='collapse' data-target='#navbarSupportedContent22'
            aria-controls='navbarSupportedContent22' aria-expanded='false' aria-label='Toggle navigation'>
        <div class='animated-icon3'><span></span><span></span><span></span></div>
    </button>

    <!-- Collapsible content -->
    <div class='collapse navbar-collapse' id='navbarSupportedContent22'>

        <!-- Links -->
        <ul class='navbar-nav mr-auto'>
            {sb.ToString()}
        </ul>
        <!-- Links -->

    </div>
    <!-- Collapsible content -->

</nav>
";
            return html;
        }
        public string RenderField(ViewField f)
        {
            string html = "";
            string placeHolder = $"placeholder='{f.Name}'";
            if ("text.number.file.url.email".IndexOf(f.HTML5Type) < 0)
                placeHolder = "";

            if (f.HTMLControl == "input")
                html = $"<input type = '{f.HTML5Type}' class='form-control' id='{f.Id}' {placeHolder}' name='{f.Name}' onchange='changedData(this)'>";
            return html;
        }
        public string RenderFields(View v)
        {
            string fields = "";
            StringBuilder sb = new StringBuilder();
            string groupDiv = "";
            string style = "";
            foreach (var it in v.FieldGroups)
            {
                if (it.Fields.Count == 1)
                {
                    sb.AppendLine(RenderField(it.Fields[0]));
                }
                else
                {
                    fields = "";
                    foreach (var f in it.Fields)
                    {
                        fields += RenderField(f) + "\r\n";
                    }
                    style = $"class='{it.Style}'";
                    if (string.IsNullOrEmpty(it.Style))
                        style = "";

                    groupDiv = $"<div id='{it.Name}' {style}>\r\n{fields}\r\n</div>";
                    sb.AppendLine(groupDiv);

                }
            }
            string html = $@"
 <form id='form{v.Name}'>
            < div class='d-flex justify-content-start'>
                <div class='container'>
                    <div class='form-group'>
                        {fields}
                    </div>
                </div>
        </form>
";
            return html;
        }
        public string RenderDataStructuresJS(View v)
        {
            string js = "";
            /*
             * build es6 class with allk fields
             * build es6 array indexed
             * build saveLocal, loadLocal, saveRemote, loadRemote, GetRemoteId
             */
            return js;
        }
        public string RenderViews()
        {
            StringBuilder sb = new StringBuilder();
            string html = "";
            string navbar = "";
            string fields = "";
            if (HomeView != null)
            {
                navbar = RenderNavBar(HomeView);
                html += $@"
<div id='{HomeView.Name}_Home'>
    {navbar}
</div>
";
            }

            foreach (var v in Views)
            {
                navbar = RenderNavBar(v);
                foreach (var o in v.Options)
                {
                    html += $@"
<div id='{v.Name}_{o.Text}'>
    {navbar}

    <form id='form{o.Text}'>
        <div class='d-flex justify-content-start'>
                <div class='form-group'>
                {fields}
                </div>
    </form>
</div>
";
                }
            }
            return html;
        }



    }
}
