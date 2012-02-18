using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using Mono.Unix;

namespace Views
{

    [Gtk.TreeNode]
    public class RecordTreeNode : Gtk.TreeNode
    {

        public RecordTreeNode (Object record, ActiveRecordModel mod)
        {
            Record = record;

            PropertyInfo nameProp = mod.PropertyDictionary["Name"].Property;
            Name = nameProp.GetValue(record, null) as String;

            PropertyInfo notesProp = mod.PropertyDictionary["Notes"].Property;
            Notes = notesProp.GetValue(record, null) as String;

            PropertyInfo IdProp = record.GetType ().GetProperty ("Id");
            Id = (int)IdProp.GetValue(record, null);
        }

        public Object Record;

        [Gtk.TreeNodeValue (Column=0)]
        public String Name;

        [Gtk.TreeNodeValue (Column=1)]
        public String Notes;

        [Gtk.TreeNodeValue (Column=2)]
        public int Id;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class CatalogSelectorAsTree : Gtk.Bin, IEditable
    {

        bool isEditable;
        String model;
        ActiveRecordModel mod;
        Type t;
        Object current_record;

        public CatalogSelectorAsTree ()
        {
            this.Build ();
            nodeview.AppendColumn ("-", new Gtk.CellRendererText (), "text", 0);
        }

        public String Model {
            get { return this.model; }
            set {
                model = value;
                Assembly asm = Assembly.Load ("Models");
                t = asm.GetType ("HumanRightsTracker.Models." + model);
                mod = ActiveRecordModel.GetModel(t);
                nodeview.NodeStore = Store;
                title_label.Text = Catalog.GetString("<b>" + model + "</b>");
                title_label.UseMarkup = true;
            }
        }

        Gtk.NodeStore Store {
            get {
                Gtk.NodeStore store = new Gtk.NodeStore (typeof (RecordTreeNode));

                MethodInfo parentsMethod = mod.Type.GetMethod("Parents");
                IList parents = parentsMethod.Invoke (mod, null) as IList;

                foreach (object record in parents) {
                    PropertyInfo childrenProp =  mod.Type.GetProperty ("Children");
                    IList children = childrenProp.GetValue (record, null) as IList;

                    if (children.Count > 0) {
                        RecordTreeNode parent = new RecordTreeNode(record, mod);
                        store.AddNode (parent);
                        walkThroughStore (record, parent);
                    } else
                        store.AddNode (new RecordTreeNode(record, mod));
                }
                return store;
            }
        }

        public void walkThroughStore (object record, RecordTreeNode parent) {
            PropertyInfo childrenProp =  mod.Type.GetProperty ("Children");
            IList children = childrenProp.GetValue (record, null) as IList;

            foreach(object childRecord in children) {
                PropertyInfo cProp =  mod.Type.GetProperty ("Children");
                IList childrenRecord = cProp.GetValue (childRecord, null) as IList;
                if (childrenRecord.Count > 0) {
                    RecordTreeNode subparent = new RecordTreeNode(childRecord, mod);
                    walkThroughStore(childRecord, subparent);
                    parent.AddChild(subparent);
                } else
                {
                   parent.AddChild(new RecordTreeNode(childRecord, mod));
                }
            }
        }

        public Object Active {
            get
            {
                return this.current_record;
            }
            set
            {
                if (value != null)
                {
                    this.current_record = value;
                    SetWidgets();
                }
            }
        }

        public void SetWidgets ()
        {
            PropertyInfo nameProp = mod.PropertyDictionary["Name"].Property;
            name.Text = nameProp.GetValue(this.current_record, null) as String;

            PropertyInfo notesProp = mod.PropertyDictionary["Notes"].Property;
            String noteString = notesProp.GetValue(this.current_record, null) as String;

              if ((noteString != null) && (noteString.Trim().Length >0)) {
                note_label.Show ();
                notes.Text = noteString.Trim();
                notes.Show ();
            } else {
                note_label.Hide ();
                notes.Hide();
           }
        }

        public bool IsEditable {
            set {
                this.isEditable = value;
                if (this.isEditable == true)
                    nodeview.Visible = true;
                else
                    nodeview.Visible = false;
            }
            get {
                return this.isEditable;
            }
        }

        protected void OnNodeviewRowActivated (object o, Gtk.RowActivatedArgs args)
        {
            Gtk.NodeSelection selection = ((Gtk.NodeView)o).NodeSelection;
            Object record = ((RecordTreeNode)selection.SelectedNode).Record;
            PropertyInfo IdProp = record.GetType ().GetProperty ("Id");
            int recordId = (int)IdProp.GetValue(record, null);
            Active = ActiveRecordMetaBase.FindFirst(t, new ICriterion[] {  Restrictions.Eq("Id", recordId)});
            SetWidgets ();
        }
    }
}

