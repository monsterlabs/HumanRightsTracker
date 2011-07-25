using System;
using Gtk;
using HumanRightsTracker.Models;

namespace HumanRightsTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow () : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Default = peoplelist.SearchButton;
        }

        protected void OnDeleteEvent (object sender, DeleteEventArgs a)
        {
            Application.Quit ();
            a.RetVal = true;
        }
        protected virtual void PersonSelected (object sender, System.EventArgs e)
        {
            Person person = (Person) sender;
            show.Person = person;
        }
        
        
    }
}

