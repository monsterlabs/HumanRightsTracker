using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsList : Gtk.Bin
    {
        Act[] acts;
        int case_id;

        public ActsList ()
        {
            this.Build ();
            row.Destroy ();
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
            acts = Act.FindAll (new ICriterion[] { Restrictions.Eq("CaseId", case_id) });
            foreach (Act a in acts)
            {
                actsList.PackStart (new ActRow (a));
            }
        }

        protected void OnNewAct (object sender, System.EventArgs e)
        {
            new NewActWindow (case_id, OnNewActReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewActReturned (object sender, EventArgs args)
        {
            Act a = sender as Act;
            actsList.PackStart (new ActRow (a));
            return;
        }
    }
}

