using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public class TableField
    {
        public int Id { set; get; }
        public int Order { set; get; }
        public string Name { set; get; }
        public Type DataType { set; get; }
        public string ViewDataType { set; get; }
        public int Length { set; get; }
        public int DecimalLenght { set; get; }
        public bool Nullable { set; get; }
        public string Caption { set; get; }
        public string Default { set; get; }
        public string RelatedTable { set; get; }
        public string RelatedField { set; get; }
        public string Group { set; get; }

        protected virtual void Init()
        {
            Id = 0;
            Order = 0;
            DataType = typeof(string);
            Length = 0;
            DecimalLenght = 2;
            Nullable = true;
            Caption = "";
            Default = "";
            RelatedTable = "";
            RelatedField = "";
            Group = "";
        }

        public TableField()
        {
            Init();
        }
    }
}
