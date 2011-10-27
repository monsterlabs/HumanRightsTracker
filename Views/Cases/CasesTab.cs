using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasesTab : Gtk.Bin, TabWithDefaultButton
    {
        public CasesTab ()
        {
            Console.WriteLine("Building Cases Tab...");
            this.Build ();
        }


        public Gtk.Button DefaultButton ()
        {
            return caselist.SearchButton;
        }

        public void InitialSetup ()
        {
            show.Hide ();
        }

        protected void OnCaseSaved (object sender, System.EventArgs e)
        {
            caselist.ReloadStore();
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
            c.Delete();
            caselist.ReloadStore();
            return;
        }

        protected void CaseSelected (object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                show.Show();
                Case mycase = (Case) sender;
                show.Case = mycase;
                removeButton.Sensitive = true;
            } else {
                show.Hide();
                removeButton.Sensitive = false;
            }
        }
    }
}

