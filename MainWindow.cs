using System;
using Gtk;
using Castle.ActiveRecord;
using HumanRightsTracker.Models;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		label2.Text = Country.FindFirst().Code;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

