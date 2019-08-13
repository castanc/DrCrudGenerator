using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public class ViewField : TableField
    {
        public string Value { set; get; }
        public bool ReadOnly { set; get; }
        public string HTMLControl { set; get; }
        public string HTML5Type { set; get; }
        public string Style { set; get; }
        public string Mask { set; get; }
        public bool Visible { set; get; }
        public bool VisibleInPortrait { set; get; }
        public string OnChanged { set; get; }
        public string OnLooseFocus { set; get; }
        public string OnFocus { set; get; }
        public string PlaceHolder { set; get; }
        public string Step { set; get; }
        public bool Required
        {
            set { Nullable = !value; }
            get { return !Nullable;  }
        }

        //textrea rows, cols and wordwrap via style


        protected override void Init()
        {
            Value = "";
            ReadOnly = false;
            HTMLControl = "input";
            HTML5Type = "text";
            Style = "";
            Mask = "";
            Visible = true;
            VisibleInPortrait = true;
            OnChanged = "";
            OnLooseFocus = "";
            OnFocus = "";
            Name = "";
            PlaceHolder = "";
            Step = "1";
        }
        public ViewField()
        {
            base.Init();
            Init();
        }
        public ViewField(string name)
        {
            base.Init();
            Init();
            Name = name;
        }


    }
}
