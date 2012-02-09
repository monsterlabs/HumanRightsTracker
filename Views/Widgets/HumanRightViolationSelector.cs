using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using System.Collections;

namespace Views
{

    [Gtk.TreeNode]
    public class HumanRightsViolationNode : Gtk.TreeNode
    {

        public HumanRightsViolationNode (HumanRightsViolation record)
        {
            HumanRightsViolation = record;

            Name = record.Name;
            Id = record.Id;
        }

        public HumanRightsViolation HumanRightsViolation;
        [Gtk.TreeNodeValue (Column=0)]
        public String Name;
        [Gtk.TreeNodeValue (Column=1)]
        public int Id;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class HumanRightViolationSelector : Gtk.Bin, IEditable
    {

        protected Gtk.NodeStore store;
        protected HumanRightsViolation human_right_violation;
        protected bool isEditable;

        public event EventHandler ViolationSelected;

        public HumanRightViolationSelector ()
        {
            this.Build ();
            store = new Gtk.NodeStore (typeof (HumanRightsViolationNode));
            nodeview.NodeStore = store;
            nodeview.AppendColumn ("-", new Gtk.CellRendererText (), "text", 0);
        }

        public void FilterByCategoryId(int category_id) {
            HumanRightsViolation[] parents = HumanRightsViolation.FindAll(new ICriterion[] { Restrictions.Eq ("CategoryId", category_id) });
            store.Clear();
            foreach (HumanRightsViolation record in parents ) {
                if (record.Children.Count > 0) {
                    HumanRightsViolationNode parent = new HumanRightsViolationNode(record);
                    store.AddNode (parent);
                    walkThroughStore (record, parent);
                } else
                    store.AddNode (new HumanRightsViolationNode(record));
            }
        }

        static void walkThroughStore (HumanRightsViolation record, HumanRightsViolationNode parent) {
            foreach(HumanRightsViolation childRecord in record.Children) {
                if (childRecord.Children.Count == 0)
                    parent.AddChild(new HumanRightsViolationNode(childRecord));
                else {
                    HumanRightsViolationNode subparent = new HumanRightsViolationNode(childRecord);
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

        public HumanRightsViolation Active {
            get {
                return this.human_right_violation;
            }
            set {
                this.human_right_violation = value;
                if (this.human_right_violation != null) {
                    SetWidgets ();
                }
            }
        }

        public void SetWidgets() {
            name.Text = this.human_right_violation.Name.Trim();
            if ((this.human_right_violation.Notes != null) && (this.human_right_violation.Notes.Trim().Length >0)) {
                note_label.Show ();
                notes.Text = this.human_right_violation.Notes.Trim();
                notes.Show ();
            } else {
                note_label.Hide ();
                notes.Hide();
           }
        }

        protected void OnNodeviewRowActivated (object o, Gtk.RowActivatedArgs args)
        {
            Gtk.NodeSelection selection = ((Gtk.NodeView)o).NodeSelection;
            Active = ((HumanRightsViolationNode)selection.SelectedNode).HumanRightsViolation;
            SetWidgets ();

            if (ViolationSelected != null) {
                ViolationSelected (Active, args);
            }
        }
    }
}

