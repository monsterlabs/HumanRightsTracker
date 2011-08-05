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
            alphabetlist1.SetEventHandlerOnSelectionChanged(LetterOnSelectionChanged);
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
                show.Show();
                Institution institution = (Institution) sender;
                show.Institution = institution;
                removeButton.Sensitive = true;
            } else {
                show.Hide();
                removeButton.Sensitive = false;
            }
        }

        protected void LetterOnSelectionChanged (object o, System.EventArgs args)
        {
            if (o != null) {
                Gtk.NodeSelection selection = (Gtk.NodeSelection) o;
                LetterNode node = (LetterNode) selection.SelectedNode;
                institutionlist.Search(node.Letter);
                Console.WriteLine(node.Letter);
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