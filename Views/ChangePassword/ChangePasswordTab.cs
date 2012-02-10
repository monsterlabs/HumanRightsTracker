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
            this.Build();
        }

        public void InitialSetup ()
        {
            if (hasLoaded == false) {
                Console.WriteLine("Building Change Password Tab...");
                this.ShowAll ();
                hasLoaded = true;
                error_message.Visible = false;
                Console.WriteLine("Change Password Tab Complete.");
            }
        }

        public Gtk.Button DefaultButton ()
        {
            return saveButton;
        }

        protected void ChangePassword (object sender, System.EventArgs e)
        {
            // FIX IT: This current implementation supports only the change of password for  the admin user
            User u = User.authenticate("admin", current_password.Text);
            if (u != null)
            {
                if (User.ChangePassword("admin", new_password.Text, password_confirmation.Text)) {
                    error_message.Text = Catalog.GetString("Your password has been changed!");
                    ClearTextEntries ();
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

        private void ClearTextEntries () {
            current_password.Text = "";
            new_password.Text = "";
            password_confirmation.Text = "";
        }
    }
}

