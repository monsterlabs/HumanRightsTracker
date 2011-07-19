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
		
		Religion r = new Religion();
		r.Name = "test";
		if (r.IsValid()) {
			r.Save();
			error_message.Text = "Religion saved";
		} else
			error_message.Text = String.Join(",",r.ValidationErrorMessages);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

