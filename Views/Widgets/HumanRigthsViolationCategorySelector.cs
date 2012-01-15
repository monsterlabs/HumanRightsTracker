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
            // TODO: Change this for a treeview
            comboboxentry.Model = this.store;
            comboboxentry.Entry.Completion = new Gtk.EntryCompletion ();
            comboboxentry.Entry.Completion = new Gtk.EntryCompletion();
            comboboxentry.Entry.Completion.Model = comboboxentry.Model;
            comboboxentry.Entry.Completion.TextColumn = 0;
            comboboxentry.Entry.Completion.InlineCompletion = false;
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
                        this.store.AppendValues(parent, recordChild.Name, recordChild.Id);
                    } else {
                        Gtk.TreeIter subparent = this.store.AppendValues (parent, recordChild.Name, recordChild.Id);
                        addChildrenToStore(recordChild, subparent);
                    }
                }
        }

    }
}

