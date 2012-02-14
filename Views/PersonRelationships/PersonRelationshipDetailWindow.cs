using System;
using HumanRightsTracker.Models;
namespace Views
{
    public partial class PersonRelationshipDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public PersonRelationshipDetailWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public PersonRelationshipDetailWindow (Person p, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            PersonRelationship pr = new PersonRelationship ();
            pr.Person = p;
            show.PersonRelationship = pr;
            show.IsEditable = true;
        }

        public PersonRelationshipDetailWindow (PersonRelationship pr, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.PersonRelationship = pr;
            show.IsEditable = true;
        }

        public PersonRelationshipDetailWindow (PersonRelationship pr, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.TransientFor = parent;
            show.PersonRelationship = pr;
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
            if (show.PersonRelationship.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

