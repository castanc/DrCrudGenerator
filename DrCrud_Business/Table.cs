using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public class Table
    {
        private string ob = "{";
        private string eb = "}";
        private string dollar = "$";

        public string Name { set; get; }
        public bool Primary { set; get; }
        public string Description { set; get; }
        public bool Dynamic { set; get; }
        public string JScript { set; get; }

        public Dictionary<string, TableField> Fields = new Dictionary<string, TableField>();

        public Table()
        {
            Primary = true;
            Dynamic = false;
        }

        public void AddStringField(string name, string stringRender = "string", string caption = "" )
        {
            if (string.IsNullOrEmpty(caption))
                caption = name;
            Fields.Add(name, new TableField() { Name = name, ViewDataType = stringRender, Caption = caption });
        }

        public void AddComboField(string name, string relatedTable, string caption = "")
        {
            if (string.IsNullOrEmpty(caption))
                caption = name;
            Fields.Add(name, new TableField() { Name = name, ViewDataType = "select", Caption = caption, RelatedTable = relatedTable });
        }


        public void AddField(TableField f)
        {
            Fields.Add(f.Name, f);
        }

        public Table CreateAutoTable(string name, bool dynamic = false, string items = ",", bool primary = false )
        {
            var t = new Table() { Name = name, Description = "Automatic Table", Primary = primary, Dynamic = dynamic };

            string[] cols = "".Split(',');
            var tf = new TableField();

            int count = 0;
            foreach(string c in cols)
            {
                if ( string.IsNullOrEmpty(c))
                    tf = new TableField() { Name = $"{c}", DataType = typeof(string),ViewDataType = "text" };
                else
                    tf = new TableField() { Name = $"Col{count}", DataType = typeof(string), ViewDataType = "text" };
                t.Fields.Add(tf.Name, tf);
                count++;
            }

            return t;
        }

        public virtual void CreateJS()
        {
            StringBuilder sbInitializer = new StringBuilder();
            StringBuilder sbGetData = new StringBuilder();
            StringBuilder sbSetData = new StringBuilder();
            StringBuilder sbImport = new StringBuilder();
            StringBuilder sbExport = new StringBuilder();

            foreach(var kvp in Fields)
            {
                sbInitializer.AppendLine($"\t\t{kvp.Value.Name}='';");
                sbGetData.AppendLine($"\t\trecord.{kvp.Value.Name} = {dollar}('#{kvp.Value.Name}().val();");
                sbSetData.AppendLine($"\t\t{dollar}('#{kvp.Value.Name}().val(record.{Name});");
                sbImport.AppendLine($"record.{kvp.Value.Name} = p[i];");
                sbExport.AppendLine($"s = s + data{Name}[i].{kvp.Value.Name}" + "\t");
            }


            JScript = $@"class Table{Name} ${ob}
    constructor() ${ob}
        {sbInitializer.ToString()}
    ${eb}
    //Methods
    setData(record)${ob}
        {sbSetData.ToString()}
    ${eb}

    getData()${ob}
        record = new Table{Name}();                
        ${sbGetData.ToString()};
    ${eb}

    saveRecord(record)${ob}
        record.Id = data{Name}.length;
        data{Name}.set(record.Id, record);
    ${eb}

    importData(rawData)${ob}
    var lines = rawData.split('\n');
    lines.forEach(function (l, index) ${ob}
        record = new data{Name}();
        var cols = l.split('\'t');
        count = 0;
        cols.forEach(function ( c, index2) %{ob}
            {sbImport.ToString()}
        ${eb}
    ${eb});

    exportData()${ob}
    var data = "";
    for([key,value] of dat{Name}) 
      ${sbExport.ToString()}

    lines.forEach(function (l, index) ${ob}
        record = new data{Name}();
        var cols = l.split('\'t');
        count = 0;
        cols.forEach(function ( c, index2) %{ob}
            {sbImport.ToString()}
        ${eb}
    ${eb});


${eb}

    var data{Name} = new Map();
";
        }
    }
}


//https://stackoverflow.com/questions/7196212/how-to-create-dictionary-and-add-key-value-pairs-dynamically