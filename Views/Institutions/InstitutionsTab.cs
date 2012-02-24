using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionsTab : Gtk.Bin, TabWithDefaultButton
    {
        bool hasLoaded = false;

        public InstitutionsTab () {}

        public Gtk.Button DefaultButton ()
        {
            return institutionlist.SearchButton;
        }

        public void InitialSetup ()
        {
            if (hasLoaded == false) {
                Console.WriteLine("Building Institutions Tab...");
                this.Build ();
                institutionlist.ReloadStore ();
                this.ShowAll ();
                hasLoaded = true;
                Console.WriteLine("Institutions Tab Complete.");
                show.IsEditing = false;
                show.EnableActionButtons ();
            }
        }

        protected void OnInstitutionlistSelectionChanged (object sender, System.EventArgs e)
        {
            if (sender != null){
                Institution institution = sender as Institution;
                show.Institution = institution;
                show.Show();
                removeButton.Sensitive = true;
            } else {
                show.Hide();
                removeButton.Sensitive = false;
            }
        }


        protected void OnAddButtonClicked (object sender, System.EventArgs e)
        {
            Institution i = new Institution();
            institutionlist.UnselectAll();
            show.Institution = i;
            show.IsEditing = true;
            show.Show();
            return;
        }

        protected void OnRemoveButtonClicked (object sender, System.EventArgs e)
        {
            Institution i = show.Institution;
            if (i.HasRelateRecords == false) {
                i.DeleteAndFlush();
                institutionlist.ReloadStore();
            } else {
                 Console.WriteLine("We can't delete this collective actor because it is still associated to other records");
                 new ValidationErrorsDialog ("We can't delete this collective actor because it is still associated to other records", (Gtk.Window)this.Toplevel);
            }
            return;
        }
      
        protected void OnInstitutionSaved (object sender, System.EventArgs e)
        {
            institutionlist.ReloadStore();
            show.Institution = sender as Institution;
            show.Show();
        }
    }
}