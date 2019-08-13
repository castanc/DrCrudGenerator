using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public partial class Application
    {
        private string ob = "{";
        private string eb = "}";
        private string dollar = "$";
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();
        private View HomeView = null;

        public Dictionary<string, Table> Tables = new Dictionary<string, Table>();

        public string Name { set; get; }
        public List<View> Views { set; get; }

        public string Language { set; get; }
        public string StartView { set; get; }


        #region constructor and virtual methods
        public Application()
        {
            Views = new List<View>();
        }
        public virtual void CreateApplication()
        {
            throw new NotImplementedException();
        }

        public virtual void CreateTables()
        {
            throw new NotImplementedException();
        }

        public virtual View CreateFilterView()
        {
            return null;
        }
        #endregion

        public string Translate(string text)
        {
            //TODO Implement multi language
            return text;
        }
        public ViewFieldGroup CreateViewFieldGroup(TableField tf)
        {
            var vfg = new ViewFieldGroup();
            var vf = tf as ViewField;
            
            if (string.IsNullOrEmpty(vf.Group))
            {
                if (".text.number.date.time.url.email.file.search".IndexOf(tf.ViewDataType) > 0)
                {
                    vf.HTMLControl = "input";
                    vf.HTML5Type = tf.ViewDataType;
                    vf.PlaceHolder = vf.Name;
                    vf.Caption = "";
                    vfg.Fields.Add(vf);
                }
                else if (!string.IsNullOrEmpty(vf.RelatedTable))
                {
                    //combo
                    vf.HTMLControl = "select";
                    vf.Group = vf.RelatedTable;

                    if (Tables[vf.RelatedTable].Dynamic)
                    {
                        ViewField vfText = new ViewField($"{vf.Name}_input");
                        vfText.PlaceHolder = vf.Name;
                        vfText.Group = vf.RelatedTable;
                        ViewField vfButton = new ViewField($"{vf.Name}_button");
                        vfButton.HTMLControl = "button";
                        vfButton.Caption = "+";
                        vfButton.Group = vf.RelatedTable;

                        vf.Caption = "";
                        vfText.Caption = vf.Name;
                        vfg.Fields.Add(vf);
                        vfg.Fields.Add(vfText);
                        vfg.Fields.Add(vfButton);
                    }
                    else vfg.Fields.Add(vf);
                }
            }
            else
            {
                vfg.Name = vf.Group;
                //todo project group fields and build FieldGroup
            }
            return vfg;
        }
        public string BuildCopyToClipboardView()
        {
            string html = "";
            return html;
        }
        public string BuildFilterView()
        {
            string html = "";
            return html;
        }
        public void CreateHomeView()
        {
            if (Tables.Count > 1)
            {
                HomeView = new View("Home");
                foreach (var v in Views)
                {
                    HomeView.Options.Add(new Option() { Text = v.Name, HRef = v.Name });
                }
            }
            

        }
        public virtual void CreateViews()
        {
            foreach (var kvp in Tables)
            {
                var v = new View() { Name = Name };
                foreach (var kvpf in kvp.Value.Fields)
                    v.FieldGroups.Add(CreateViewFieldGroup(kvpf.Value));
                Views.Add(v);
            }
        }
    }
}


