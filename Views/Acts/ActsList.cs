using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;
using System.Linq;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsList : Gtk.Bin, IEditable
    {
        List<Act> acts;
        Case c;
        bool isEditable;

        public ActsList ()
        {
            this.Build ();

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
            }
        }

        public void ReloadList ()
        {
            if (c.Id < 1) {
                return;
            }
            acts = new List<Act> (Act.FindAll (new ICriterion[] { Restrictions.Eq("Case", c) }));
            editablelist1.Records = acts.Cast<ListableRecord>().ToList();
        }

        protected void OnNewAct (object sender, System.EventArgs e)
        {
            new ActDetailWindow (c, OnNewActReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewActReturned (object sender, EventArgs args)
        {
            Act a = sender as Act;
            acts.Add (a);
            return;
        }

        protected void OnActRowRemoved (object sender, EventArgs e)
        {
            ActRow actRow = sender as ActRow;
            Act a = actRow.Act;
            acts.Remove(a);

            if (a.Id >= 1)
            {
                // TODO: Confirmation.
                a.Delete ();
            }

            return;
        }
    }
}

