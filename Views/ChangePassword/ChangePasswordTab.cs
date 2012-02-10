using System;
using Gtk;
using Castle.ActiveRecord;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ChangePasswordTab : Gtk.Bin,  TabWithDefaultButton
    {
        bool hasLoaded = false;

        public ChangePasswordTab ()
        {
            this.Build ();
            error_message.Visible = false;
        }

        public void InitialSetup ()
        {
             if (hasLoaded == false) {
                Console.WriteLine("Building Change Password Tab...");
                this.Build ();
                this.ShowAll ();
                hasLoaded = true;
                Console.WriteLine("Change Password Tab Complete.");
            }
        }

        public Gtk.Button DefaultButton ()
        {
            return new Gtk.Button();
        }


        protected void ChangePassword (object sender, System.EventArgs e)
        {
            User u = User.authenticate("admin", current_password.Text);
            if (u != null)
            {
                if (User.ChangePassword("admin", new_password.Text, password_confirmation.Text)) {
                    error_message.Text = Catalog.GetString("Your password has been changed!");
                }
                else
                {
                    error_message.Text = Catalog.GetString("Your new password has errors!");
                }

            }
            else
               error_message.Text = Catalog.GetString("Your current password is incorrect!");
            
            error_message.Visible = true;
        }
    }
}

