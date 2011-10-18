using System;
using Gtk;
using Views;

namespace HumanRightsTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow () : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            individual_peopletab.InitialSetup ();
            immigrant_peopletab.InitialSetup ();
            casestab.InitialSetup ();
            institutionstab.InitialSetup ();
            this.Default = individual_peopletab.DefaultButton ();
        }

        protected void OnDeleteEvent (object sender, DeleteEventArgs a)
        {
            Application.Quit ();
            a.RetVal = true;
        }
        
        protected void OnChangeTab (object o, Gtk.SwitchPageArgs args)
        {
            Gtk.Notebook n = o as Notebook;
            TabWithDefaultButton tab = n.CurrentPageWidget as TabWithDefaultButton;
            if (tab != null)
                this.Default = tab.DefaultButton ();
            return;
        }

        protected void onRemove (object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException ();
        }

        protected void onAdd (object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException ();
        }
    }
}

