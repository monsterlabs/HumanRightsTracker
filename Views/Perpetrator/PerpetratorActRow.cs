using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorActRow : Gtk.Bin
    {
        protected PerpetratorAct perpetratorAct;
        protected bool isEditable;

        public event EventHandler OnRowRemoved;

        public PerpetratorActRow ()
        {
            this.Build ();
        }

        public PerpetratorActRow (PerpetratorAct perpetratorAct, EventHandler removed)
        {
            this.Build ();
            this.PerpetratorAct = perpetratorAct;
            this.OnRowRemoved = removed;
        }

        public virtual bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                button8.Visible = value;
                button71.Visible = value;
            }
        }
        public virtual PerpetratorAct PerpetratorAct
        {
            get {return perpetratorAct;}
            set
            {
                perpetratorAct = value;
                if (perpetratorAct != null)
                {
                    humanRight.Text = perpetratorAct.HumanRightsViolation.Name;
                    place.Text = perpetratorAct.ActPlace.Name;
                    location.Text = perpetratorAct.Location;
                } else {
                    humanRight.Hide ();
                    place.Hide ();
                    location.Hide ();
                }
            }
        }

        protected void OnRemove (object sender, System.EventArgs e)
        {
            if (OnRowRemoved != null)
                OnRowRemoved (this, e);
        }

        protected void OnInfo (object sender, System.EventArgs e)
        {
            new PerpetratorActWindow (perpetratorAct, OnPerpetratorActUpdated, (Gtk.Window) this.Toplevel);
        }

        protected void OnPerpetratorActUpdated (object sender, PerpetratorActEventArgs args)
        {
            PerpetratorAct p = args.PerpetratorAct;
            if (perpetratorAct.Perpetrator.Id > 0) {
                this.PerpetratorAct = p;
            }

            return;
        }
    }
}
