using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InstitutionRelationshipDetailWindow : Gtk.Window
    {

        public event EventHandler OnSaved = null;
        public InstitutionRelationshipDetailWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public InstitutionRelationshipDetailWindow (Institution i, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            InstitutionRelationship ir = new InstitutionRelationship ();
            ir.Institution = i;
            show.InstitutionRelationship = ir;
            show.IsEditable = true;
        }

        public InstitutionRelationshipDetailWindow (InstitutionRelationship ir, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.InstitutionRelationship = ir;
            show.IsEditable = true;
        }

        public InstitutionRelationshipDetailWindow (InstitutionRelationship ir, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.TransientFor = parent;
            show.InstitutionRelationship = ir;
            show.IsEditable = true;
            show.HideActionButtons ();
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.InstitutionRelationship.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

