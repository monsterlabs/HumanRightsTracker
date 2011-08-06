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
        int case_id;

        public ActsList ()
        {
            this.Build ();
            row.Destroy ();
        }

        public List<Act> Acts
        {
            get {return acts;}
        }

        public int CaseId
        {
            get { return case_id; }
            set
            {
                case_id = value;
                ReloadList ();
            }
        }

        public void ReloadList ()
        {
            foreach (Gtk.Widget w in actsList.AllChildren)
            {
                w.Destroy();
            }
            acts = new List<Act> (Act.FindAll (new ICriterion[] { Restrictions.Eq("CaseId", case_id) }));
            foreach (Act a in acts)
            {
                actsList.PackStart (new ActRow (a, OnActRowRemoved));
            }
            actsList.ShowAll ();
        }

        protected void OnNewAct (object sender, System.EventArgs e)
        {
            new ActDetailWindow (case_id, OnNewActReturned, (Gtk.Window)this.Toplevel);
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

