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
            peopletab1.InitialSetup ();
            this.Default = peopletab1.DefaultButton ();
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
    }
}

