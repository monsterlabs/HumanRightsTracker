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
            MethodInfo modelMethod = mod.Type.GetMethod("ParentName");
            if (modelMethod != null) {
                ParentName = modelMethod.Invoke (record, null) as String;
            } else {
                ParentName = "";
            }

            Selected = false;
        }

        public Object Record;
        ActiveRecordModel mod;

        [Gtk.TreeNodeValue (Column=0)]
        public bool Selected;
        [Gtk.TreeNodeValue (Column=1)]
        public string Name;
        [Gtk.TreeNodeValue (Column=2)]
        public string ParentName;

        public void SaveRecord () {
            PropertyInfo nameProp =  mod.PropertyDictionary["Name"].Property;
            nameProp.SetValue (Record, Name, null);
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
                    Gtk.CellRendererCombo parentCell = new Gtk.CellRendererCombo ();

                    string ParentClassName = modelMethod.Invoke (options.GetValue (0), null) as String;
                    Type pT = asm.GetType ("HumanRightsTracker.Models." + ParentClassName);
                    Array comboOptions = ActiveRecordMetaBase.All(pT, new Order("Name", true));
                    PropertyInfo nameProp =  ActiveRecordModel.GetModel(pT).PropertyDictionary["Name"].Property;

                    Gtk.ListStore parentsStore = new Gtk.ListStore (typeof (string));

                    foreach (Object o in comboOptions) {
                        String name = nameProp.GetValue(o, null) as String;
                        parentsStore.AppendValues (name);
                    }

                    parentCell.Model = parentsStore;
                    parentCell.TextColumn = 0;
                    parentCell.Editable = true;

                    table.AppendColumn ("Parent", parentCell, "text", 2);
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

        void HandleNameCellEdited (object o, Gtk.EditedArgs args)
        {
            RecordNode node = store.GetNode(new Gtk.TreePath(args.Path)) as RecordNode;
            node.Name = args.NewText;
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

