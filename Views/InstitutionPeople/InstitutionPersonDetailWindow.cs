using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InstitutionPersonDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public InstitutionPersonDetailWindow () :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public InstitutionPersonDetailWindow (Institution i, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            InstitutionPerson ip = new InstitutionPerson();
            ip.Institution = i;
            show.InstitutionPerson = ip;
            show.IsEditable = true;
            show.InstitutionReadOnly ();
        }

        public InstitutionPersonDetailWindow (InstitutionPerson ip, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.InstitutionPerson = ip;
            show.IsEditable = false;
            show.InstitutionReadOnly ();
        }

        public InstitutionPersonDetailWindow (InstitutionPerson ip, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.TransientFor = parent;
            show.InstitutionPerson = ip;
            show.IsEditable = true;
            show.HideActionButtons ();
        }

        public InstitutionPersonDetailWindow (Person p, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            InstitutionPerson ip = new InstitutionPerson();
            ip.Person = p;
            show.InstitutionPerson = ip;
            show.IsEditable = true;
            show.PersonReadOnly ();
        }

        public InstitutionPersonDetailWindow (InstitutionPerson ip, EventHandler OnSave, Gtk.Window parent, bool usingPerson) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.InstitutionPerson = ip;
            show.IsEditable = false;
            show.PersonReadOnly ();
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.InstitutionPerson.Id < 1) {
                this.Destroy ();
            }
        }
    }
}