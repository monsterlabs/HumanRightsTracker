using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class CaseRelationshipDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public CaseRelationshipDetailWindow () : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public CaseRelationshipDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            CaseRelationship cr = new CaseRelationship ();
            cr.Case = c;
            show.CaseRelationship = cr;
            show.IsEditable = true;
        }

        public CaseRelationshipDetailWindow (CaseRelationship case_relationship, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.CaseRelationship = case_relationship;
            show.IsEditable = false;
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.CaseRelationship.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

