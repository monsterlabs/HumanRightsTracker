using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasesTab : Gtk.Bin, TabWithDefaultButton
    {
        bool hasLoaded = false;

        public CasesTab ()
        {
            //Console.WriteLine("Building Cases Tab...");
            //this.Build ();
        }

        public Gtk.Button DefaultButton ()
        {
            return caselist.SearchButton;
        }

        public void InitialSetup ()
        {
            if (hasLoaded == false) {
                Console.WriteLine("Building Cases Tab...");
                this.Build ();
                caselist.ReloadStore ();
                this.ShowAll ();
                hasLoaded = true;
                Console.WriteLine("Cases Tab Complete.");
                show.IsEditing = false;
                show.EnableActionButtons ();
            }
        }

        protected void OnCaseSaved (object sender, System.EventArgs e)
        {
            caselist.ReloadStore();
            show.Case = sender as Case;
            show.Show();
        }


        protected void OnAddButtonClicked (object sender, System.EventArgs e)
        {
            Case c = new Case();
            caselist.UnselectAll();
            show.Case = c;
            show.IsEditing = true;
            show.Show();
            return;
        }

        protected void OnRemoveButtonClicked (object sender, System.EventArgs e)
        {
            Case c = show.Case;
            if (c.HasRelateRecords == false) {
                c.DeleteAndFlush ();
                caselist.ReloadStore();
            } else {
                Console.WriteLine("We can't delete this case because it is still associated to other records");
                new ValidationErrorsDialog ("We can't delete this case because it is still associated to other records", (Gtk.Window)this.Toplevel);
            }

            return;
        }

        protected void CaseSelected (object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                show.Show();
                Case mycase = (Case) sender;
                mycase.Refresh ();
                show.Case = mycase;
                removeButton.Sensitive = true;
            } else {
                show.Hide();
                removeButton.Sensitive = false;
            }
        }
    }
}

