using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsList : Gtk.Bin
    {
        List<Act> acts;
        Case c;
        bool isEditable;

        public ActsList ()
        {
            this.Build ();
            row.Destroy ();
            acts = new List<Act>();
        }

        public List<Act> Acts
        {
            get {return acts;}
        }

        public Case Case
        {
            get { return c; }
            set
            {
                c = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                newButton.Visible = value;
                foreach (Gtk.Widget row in actsList.AllChildren) {
                    ((ActRow) row).IsEditable = value;
                }
            }
        }

        public void ReloadList ()
        {
            foreach (Gtk.Widget w in actsList.AllChildren)
            {
                w.Destroy();
            }
            if (c.Id < 1) {
                return;
            }
            acts = new List<Act> (Act.FindAll (new ICriterion[] { Restrictions.Eq("Case", c) }));
            foreach (Act a in acts)
            {
                actsList.PackStart (new ActRow (a, OnActRowRemoved));
            }
            actsList.ShowAll ();
        }

        protected void OnNewAct (object sender, System.EventArgs e)
        {
            new ActDetailWindow (c, OnNewActReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewActReturned (object sender, EventArgs args)
        {
            Act a = sender as Act;
            actsList.PackStart (new ActRow (a, OnActRowRemoved));
            actsList.ShowAll ();
            acts.Add (a);
            return;
        }

        protected void OnActRowRemoved (object sender, EventArgs e)
        {
            ActRow actRow = sender as ActRow;
            Act a = actRow.Act;
            acts.Remove(a);
            actsList.Remove(actRow);

            if (a.Id >= 1)
            {
                // TODO: Confirmation.
                a.Delete ();
            }

            return;
        }
    }
}

