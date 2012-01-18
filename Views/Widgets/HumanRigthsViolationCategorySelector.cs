using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using System.Collections;

namespace Views
{


    [Gtk.TreeNode]
    public class HumanRightsViolationCategoryNode : Gtk.TreeNode
    {

        public HumanRightsViolationCategoryNode (HumanRightsViolationCategory record)
        {
            HumanRightsViolationCategory = record;

            Name = record.Name;
            Id = record.Id;
        }

        public HumanRightsViolationCategory HumanRightsViolationCategory;
        [Gtk.TreeNodeValue (Column=0)]
        public String Name;
        [Gtk.TreeNodeValue (Column=1)]
        public int Id;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class HumanRigthsViolationCategorySelector : Gtk.Bin
    {
        protected Gtk.NodeStore store;
        protected HumanRightsViolationCategory human_right_violation_category;
        protected bool isEditable;

        public HumanRigthsViolationCategorySelector ()
        {
            this.Build ();
            nodeview.NodeStore =  Store;
            nodeview.AppendColumn ("-", new Gtk.CellRendererText (), "text", 0);
        }

        Gtk.NodeStore Store {
            get {
                Gtk.NodeStore store = new Gtk.NodeStore (typeof (HumanRightsViolationCategoryNode));
                IList parents = HumanRightsViolationCategory.Parents ();
                foreach (HumanRightsViolationCategory record in parents ) {
                    if (record.Children.Count > 0) {
                        HumanRightsViolationCategoryNode parent = new HumanRightsViolationCategoryNode(record);
                        store.AddNode (parent);
                        walkThroughStore (record, parent);
                    } else
                        store.AddNode (new HumanRightsViolationCategoryNode(record));
                }
                return store;
            }
        }

        static void walkThroughStore (HumanRightsViolationCategory record, HumanRightsViolationCategoryNode parent) {
            foreach(HumanRightsViolationCategory childRecord in record.Children) {
                if (childRecord.Children.Count == 0)
                    parent.AddChild(new HumanRightsViolationCategoryNode(childRecord));
                else {
                    HumanRightsViolationCategoryNode subparent = new HumanRightsViolationCategoryNode(childRecord);
                    walkThroughStore(childRecord, subparent);
                    parent.AddChild(subparent);
                }
            }
        }

        public bool IsEditable {
            set {
                this.isEditable = value;
                if (this.isEditable)
                    nodeview.Visible = true;
                else
                    nodeview.Visible = false;
            }
            get {
                return this.isEditable;
            }
        }

        public HumanRightsViolationCategory Active {
            get {
                return this.human_right_violation_category;
            }
            set {
                this.human_right_violation_category = value;
                if (this.human_right_violation_category != null) {
                    name.Text = this.human_right_violation_category.Name;
                }
            }
        }

        protected void OnNodeviewRowActivated (object o, Gtk.RowActivatedArgs args)
        {
          Gtk.NodeSelection selection = ((Gtk.NodeView)o).NodeSelection;
          Active = ((HumanRightsViolationCategoryNode)selection.SelectedNode).HumanRightsViolationCategory;
          name.Text = Active.Name;
        }

        protected void OnShown (object sender, System.EventArgs e)
        {
            nodeview.QueueDraw ();

        }
    }
}
