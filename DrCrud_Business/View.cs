using System;
using System.Collections.Generic;
using System.Text;


namespace DrCrud_Business
{
    public class View
    {
        public string Name { set; get; }
        public List<ViewFieldGroup> FieldGroups { set; get; }
        public bool Visible { set; get; }
        public bool Modal { set; get; }
        public string Style { set; get; }

        public string PreInput { set; get; }
        public string PostInput { set; get; }

        public List<Option> Options { set; get; }

        private void init()
        {
            Options = new List<Option>();
            Visible = false;

        }
        public View()
        {
            init();
        }
        public View(string name)
        {
            init();
            Name = name;
        }
    }
}
