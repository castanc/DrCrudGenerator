using System;
using System.Collections.Generic;
using System.Text;

namespace DrCrud_Business
{
    public class AppLinks : Application
    {
        public AppLinks() : base()
        {
            CreateTables();
            CreateViews();
        }

        public override void CreateTables()
        {
            var cats = "Categorias";
            var t = new Table() { Name = "Links", Description = "Links repository",Primary=true };
            t.AddStringField("Fecha","date");
            t.AddStringField("Hora","time");
            t.AddComboField("Categoria",cats);
            t.AddStringField("Descripcion");

            Tables.Add(t.Name,t);
            Tables.Add(cats,t.CreateAutoTable(cats,true));
        }

        public override void CreateApplication()
        {
            Name = "Links";
            var view = new View();

            view.FieldGroups.Add(new ViewFieldGroup(new ViewField() { Name = "Fecha", HTML5Type = "hidden" }));
            view.FieldGroups.Add(new ViewFieldGroup(new ViewField() { Name = "Hora", HTML5Type = "hidden" }));

            var fildNewCat = new ViewField() { Name = "newCategory", Caption = "New Category", OnLooseFocus = "update('catList(this)'" };
            var fieldCatList = new ViewField() { Name = "catList", Caption = "Categories" };
            var gf = new ViewFieldGroup();
            gf.Fields.Add(fildNewCat);
            gf.Fields.Add(fieldCatList);
            view.FieldGroups.Add(gf);

            view.FieldGroups.Add(new ViewFieldGroup("ShortDescr"));
            view.FieldGroups.Add(new ViewFieldGroup("Title"));
            view.FieldGroups.Add( new ViewFieldGroup(new ViewField() { Name = "Url", HTML5Type = "url" }));
            view.FieldGroups.Add(new ViewFieldGroup(new ViewField() { Name = "Image", HTML5Type = "file" }));

            Views.Add(view);
        }
    }
}
