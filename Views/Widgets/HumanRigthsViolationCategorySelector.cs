using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using System.Collections;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class HumanRigthsViolationCategorySelector : Gtk.Bin
    {
        Gtk.TreeStore store;

        public HumanRigthsViolationCategorySelector ()
        {
            this.Build ();
            this.BuildStore ();

            Gtk.TreeViewColumn nameColumn = new Gtk.TreeViewColumn ();
            nameColumn.Title = "-";
            Gtk.CellRendererText nameCell = new Gtk.CellRendererText ();
            nameColumn.PackStart (nameCell, true);
            nameColumn.AddAttribute (nameCell, "text", 0);
            treeview.AppendColumn (nameColumn);
            
            /*
                Gtk.TreeViewColumn idColumn = new Gtk.TreeViewColumn ();
                idColumn.Title = "";
                Gtk.CellRendererText idCell = new Gtk.CellRendererText ();
                idColumn.PackStart (idCell, false);
                treeview.AppendColumn (idColumn);
            */

            treeview.Model = this.store;
            treeview.Show ();
        }

         public void BuildStore () {
            this.store = new Gtk.TreeStore (typeof(string), typeof(int));
            foreach (HumanRightsViolationCategory record in HumanRightsViolationCategory.Parents() ) {
                walkThroughStore(record);
            }
        }

        public void walkThroughStore(HumanRightsViolationCategory record) {
            if (record.Children.Count > 0) {
                Gtk.TreeIter parent = this.store.AppendValues (record.Name, record.Id);
                addChildrenToStore(record, parent);
            } else {
                this.store.AppendValues (record.Name, record.Id);
            }
        }

        public void addChildrenToStore(HumanRightsViolationCategory record, Gtk.TreeIter parent) {
            foreach(HumanRightsViolationCategory recordChild in record.Children) {
                    if (recordChild.Children.Count == 0) {
                        this.store.AppendValues(parent, recordChild.Name, record.Id);
                    } else {
                        Gtk.TreeIter subparent = this.store.AppendValues (parent, recordChild.Name, record.Id);
                        addChildrenToStore(recordChild, subparent);
                    }
                }
        }

    }
}

