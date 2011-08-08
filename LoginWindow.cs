using System;
using Gtk;
using Castle.ActiveRecord;
using HumanRightsTracker.Models;

public partial class LoginWindow : Gtk.Window
{
	public LoginWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected virtual void authenticate (object sender, System.EventArgs e)
	{
		User u = User.authenticate(login.Text, password.Text);
		Console.WriteLine("Authenticating " + login.Text);
		if (u != null)
		{
			// show the main view
			HumanRightsTracker.MainWindow mw = new HumanRightsTracker.MainWindow ();
			//mw.ShowNow ();
			this.Hide();
		} else {
			error_message.Visible = true;
            password.Text = "";
		}
	}
	
	
}

