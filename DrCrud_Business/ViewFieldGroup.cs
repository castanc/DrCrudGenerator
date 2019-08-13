using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public class ViewFieldGroup
    {
        public string Name { set; get; }
        public string Caption { set; get; }
        public string Style { set; get; }
        public List<ViewField> Fields { set; get; }

        public ViewFieldGroup()
        {
            Fields = new List<ViewField>();
        }

        public ViewFieldGroup(string name)
        {
            Fields = new List<ViewField>();
            var fld = new ViewField(name);
            Fields.Add(fld);
            Name = name;
        }

        public ViewFieldGroup(ViewField fld)
        {
            Fields = new List<ViewField>();
            Fields.Add(fld);
            Name = fld.Name;
        }

        public ViewFieldGroup(TableField tf)
        {
            Fields = new List<ViewField>();
            
        }

        public ViewFieldGroup(string name, List<ViewField> fields)
        {
            Name = name;
            Fields = fields;
        }

    }
}
