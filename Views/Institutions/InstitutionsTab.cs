using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionsTab : Gtk.Bin, TabWithDefaultButton
    {
        public InstitutionsTab ()
        {
            this.Build ();
        }

        public Gtk.Button DefaultButton ()
        {
            return institutionlist.SearchButton;
        }

        public void InitialSetup ()
        {
            show.Hide ();
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
            i.Delete();
            institutionlist.ReloadStore();
            return;
        }

        protected void OnInstitutionSaved (object sender, System.EventArgs e)
        {
            institutionlist.ReloadStore();
        }

    }
}