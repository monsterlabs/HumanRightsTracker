using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class AddressDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;


        public AddressDetailWindow (Person p, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            Address a = new Address ();
            a.Person = p;
            show.Address = a;
            show.IsEditable = true;
        }

        public AddressDetailWindow (Address a, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.Address = a;
            show.IsEditable = false;
        }

        public AddressDetailWindow (Address a, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.TransientFor = parent;
            show.Address = a;
            show.IsEditable = false;
            show.HideActionButtons ();
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.Address.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

