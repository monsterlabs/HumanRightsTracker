using System;
using System.Collections;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;
using System.Reflection;
using NHibernate.Criterion;
using HumanRightsTracker.Models;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class RecordNode : Gtk.TreeNode
    {
        public RecordNode (Object record, ActiveRecordModel mod)
        {
            Record = record;
            this.mod = mod;
            PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
            Name = nameProp.GetValue(record, null) as String;
            MethodInfo parentMethod = mod.Type.GetMethod("ParentName");
            hasParent = parentMethod != null;
            if (hasParent) {
                PropertyInfo parentProp =  mod.Type.GetProperty ("ParentId");
                parentId = (parentProp.GetValue(record, null) as int?).Value;
                if (parentId > 0)
                    ParentName = parentMethod.Invoke (record, null) as String;
                else
                    ParentName = "";
            } else {
                ParentName = "";
            }

            MethodInfo categoryMethod = mod.Type.GetMethod("CategoryName");
            hasCategory = categoryMethod != null;
            if (hasCategory) {
                PropertyInfo categoryProp =  mod.Type.GetProperty ("CategoryId");
                categoryId = (categoryProp.GetValue(record, null) as int?).Value;
                if (categoryId > 0)
                    CategoryName = categoryMethod.Invoke (record, null) as String;
                else
                    CategoryName = "";
            } else {
                CategoryName = "";
            }
			
            MethodInfo notesMethod = mod.Type.GetMethod("Notes");
            hasNotes = notesMethod != null;
            if (hasNotes) {
                PropertyInfo notesProp =  mod.Type.GetProperty ("Notes");
                Notes = (notesProp.GetValue(record, null) as String);
            } else {
                Notes = "";
            }
			

            Selected = false;
        }

        public Object Record;
        ActiveRecordModel mod;
        public int parentId;
        bool hasParent;
        public Object ParentRecord {get; set;}
        public Object CategoryRecord {get; set;}
        public int categoryId;
        bool hasCategory;
        bool hasNotes;

        [Gtk.TreeNodeValue (Column=0)]
        public bool Selected;
        [Gtk.TreeNodeValue (Column=1)]
        public string Name;
        [Gtk.TreeNodeValue (Column=2)]
        public string ParentName;
        [Gtk.TreeNodeValue (Column=3)]
        public string CategoryName;
        [Gtk.TreeNodeValue (Column=4)]
        public string Notes;

        public int ParentId {
            get {
                return this.parentId;
            }
            set {
                parentId = value;
            }
        }
        public void SaveRecord () {
            PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
            nameProp.SetValue (Record, Name, null);
            if (hasParent) {
                PropertyInfo parentIdProp =  mod.Type.GetProperty ("ParentId");
                PropertyInfo idParentProp = ParentRecord.GetType ().GetProperty ("Id");
                parentIdProp.SetValue (Record, idParentProp.GetValue (ParentRecord, null), null);
            }
            if (hasCategory) {
                PropertyInfo categoryIdProp =  mod.Type.GetProperty ("CategaryId");
                PropertyInfo idCategoryProp = CategoryRecord.GetType ().GetProperty ("Id");
                categoryIdProp.SetValue (Record, idCategoryProp.GetValue (CategoryRecord, null), null);
            }
            if (hasNotes) {
                PropertyInfo notesProp =  mod.PropertyDictionary["Notes"].Property;
                notesProp.SetValue (Record, Notes, null);
            }
            ActiveRecordMetaBase.Save (Record);

        }

        public void DeleteRecord () {
            ActiveRecordMetaBase.Delete (Record);
        }

    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class CatalogCRUD : Gtk.Bin
    {
        String model;
        ActiveRecordModel mod;
        Type t;
        Array options;
        Hashtable selected;

        public CatalogCRUD ()
        {
            this.Build ();
            table.NodeStore = Store;
            Gtk.CellRendererToggle selectCell = new Gtk.CellRendererToggle ();
            selectCell.Activatable = true;
            selectCell.Toggled += Toggled;
            table.AppendColumn ("", selectCell, "active", 0);

            Gtk.CellRendererText nameCell = new Gtk.CellRendererText ();
            nameCell.Editable = true;
            nameCell.Edited += HandleNameCellEdited;
            table.AppendColumn ("Name", nameCell, "text", 1);

            selected = new Hashtable ();
        }

        Gtk.NodeStore store;

        Gtk.NodeStore Store {
            get {
                if (store == null) {
                    store = new Gtk.NodeStore (typeof(RecordNode));
                }
                return store;
            }
        }

        public String Model {
            get { return this.model; }
            set {
                model = value;
                Assembly asm = Assembly.Load ("Models");
                t = asm.GetType ("HumanRightsTracker.Models." + model);
                mod = ActiveRecordModel.GetModel(t);

                Populate ();

                MethodInfo modelMethod = t.GetMethod("ParentModel");
                if (modelMethod != null) {
                    //Gtk.CellRendererCombo parentCell = new Gtk.CellRendererCombo ();

                    string ParentClassName = modelMethod.Invoke (options.GetValue (0), null) as String;
                    //Type pt = asm.GetType ("HumanRightsTracker.Models." + ParentClassName);

                    CellRendererCatalogSelector parentCell = new CellRendererCatalogSelector (ParentClassName);
                    parentCell.Changed += HandleParentChanged;
                    parentCell.Editable = true;

                    table.AppendColumn ("Parent", parentCell, "text", 2);
                }

                MethodInfo categoryModelMethod = t.GetMethod("CategoryModel");
                if (categoryModelMethod != null) {
                    //Gtk.CellRendererCombo parentCell = new Gtk.CellRendererCombo ();

                    string CategoryClassName = modelMethod.Invoke (options.GetValue (0), null) as String;
                    //Type pt = asm.GetType ("HumanRightsTracker.Models." + ParentClassName);

                    CellRendererCatalogSelector categoryCell = new CellRendererCatalogSelector (CategoryClassName);
                    categoryCell.Changed += HandleCategoryChanged;
                    categoryCell.Editable = true;

                    table.AppendColumn ("Category", categoryCell, "text", 3);
                }

                MethodInfo notesMethod = t.GetMethod("Notes");
                if (notesMethod != null) {
                    Gtk.CellRendererText notesCell = new Gtk.CellRendererText ();
                    notesCell.Editable = true;
                    notesCell.Edited += HandleNotesCellEdited;
                    table.AppendColumn ("Notes", notesCell, "text", 4);
                }
            }
        }

        private void Populate()
        {
            if (entry.Text.Length == 0)
                options = ActiveRecordMetaBase.All(t, new Order("Name", true));
            else
            {
                ICriterion[] query = {
                    Expression.Like("Name",entry.Text,MatchMode.Anywhere),
                };
                options = ActiveRecordMetaBase.Where(t, query,new Order("Name", true));
            }

            store.Clear ();
            total.Text = options.Length + " records";
            foreach (Object o in options) {
                store.AddNode (new RecordNode (o, mod));
            }
        }


        // Actions

        void Toggled(object o, Gtk.ToggledArgs args) {
            RecordNode node = store.GetNode(new Gtk.TreePath(args.Path)) as RecordNode;

            node.Selected = !node.Selected;
            // Add it to selected list
            if (node.Selected)
                selected.Add (args.Path, node);
            else
                selected.Remove (args.Path);
        }

        void HandleParentChanged (object o, ComboSelectionChangedArgs args)
        {
            RecordNode node = store.GetNode(new Gtk.TreePath(args.Path)) as RecordNode;

            CatalogSelector selector = o as CatalogSelector;
            Object parent = selector.Active;
            if (parent != null)
            {
                node.ParentRecord = parent;
                PropertyInfo ParentNameProp = parent.GetType ().GetProperty ("Name");
                //parentIdProp.SetValue (Record, idParentProp.GetValue (ParentRecord, null), null);
                selector.Combobox.Entry.Text = ParentNameProp.GetValue (parent, null) as String;
                node.ParentName = ParentNameProp.GetValue (parent, null) as String;
                // tell node to save the value
                node.SaveRecord ();
            }

        }

        void HandleCategoryChanged (object o, ComboSelectionChangedArgs args)
        {
            RecordNode node = store.GetNode(new Gtk.TreePath(args.Path)) as RecordNode;

            CatalogSelector selector = o as CatalogSelector;
            Object category = selector.Active;
            if (category != null)
            {
                node.CategoryRecord = category;
                PropertyInfo CategoryNameProp = category.GetType ().GetProperty ("Name");
                //parentIdProp.SetValue (Record, idParentProp.GetValue (ParentRecord, null), null);
                selector.Combobox.Entry.Text = CategoryNameProp.GetValue (category, null) as String;
                node.CategoryName = CategoryNameProp.GetValue (category, null) as String;
                // tell node to save the value
                node.SaveRecord ();
            }

        }

        void HandleNameCellEdited (object o, Gtk.EditedArgs args)
        {
            RecordNode node = store.GetNode(new Gtk.TreePath(args.Path)) as RecordNode;
            node.Name = args.NewText;
            // tell node to save the value
            node.SaveRecord ();
        }

        void HandleNotesCellEdited (object o, Gtk.EditedArgs args)
        {
            RecordNode node = store.GetNode(new Gtk.TreePath(args.Path)) as RecordNode;
            node.Notes = args.NewText;
            // tell node to save the value
            node.SaveRecord ();
        }


        protected void Filter (object sender, System.EventArgs e)
        {
            Populate ();
        }

        protected void OnRemove (object sender, System.EventArgs e)
        {
            foreach (RecordNode node in selected.Values) {
                node.DeleteRecord ();
            }
            selected.Clear ();
            Populate ();
        }

        protected void OnAdd (object sender, System.EventArgs e)
        {
            Object record = Activator.CreateInstance(t);
            PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
            nameProp.SetValue (record, "New " + t.Name, null);
            store.AddNode (new RecordNode (record, mod));
            table.ScrollToCell (new Gtk.TreePath ("" + (options.Length)), table.Columns[1], false, 0, 0);
        }
    }
}

