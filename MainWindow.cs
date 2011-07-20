using System;
using Gtk;
namespace HumanRightsTracker
{
	public partial class MainWindow : Window
	{
		public MainWindow () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}
}

