using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Mono.Unix;
using System.Collections.Generic;
using Gtk;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class InstitutionNode : Gtk.TreeNode
    {
        public InstitutionNode (Institution institution)
        {
            Institution = institution;
            if (institution.Photo != null)
                Photo = new Gdk.Pixbuf (institution.Photo.Icon);
            else
                Photo = Gdk.Pixbuf.LoadFromResource ("Views.images.MissingInstitutionIcon.jpg");
            Name = institution.Name;
        }

        public Institution Institution;
        [Gtk.TreeNodeValue (Column=0)]
        public Gdk.Pixbuf Photo;
        [Gtk.TreeNodeValue (Column=1)]
        public string Name;
    }


    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionList : Gtk.Bin
    {
        Institution[] institutions;

        public event EventHandler SelectionChanged;
        public InstitutionList ()
        {
            this.Build ();
            institutionNodeView.NodeStore = Store;
            institutionNodeView.AppendColumn ("Photo", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
            institutionNodeView.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 1);

            institutionNodeView.NodeSelection.Changed += new System.EventHandler (OnSelectionChanged);
        }

        protected void OnSelectionChanged (object o, System.EventArgs args)
        {
            Gtk.NodeSelection selection = (Gtk.NodeSelection)o;
            InstitutionNode node = (InstitutionNode)selection.SelectedNode;
            if (SelectionChanged != null)
            {
                Institution i = null;
                if (node != null)
                    i = node.Institution;
                SelectionChanged (i, args);
            }
        }

        Gtk.NodeStore store;

        Gtk.NodeStore Store {
            get {
                if (store == null) {
                    NewStore();
                }
                return store;
            }
        }

        public Gtk.Button SearchButton { get { return searchButton; } }

        public void ReloadStore ()
        {
            institutions = Institution.FindAllOrderedByName ();
            FillStore ();
        }

        public void FillStore() {
            institutionNodeView.NodeStore.Clear ();

            foreach (Institution i in institutions)
                institutionNodeView.NodeStore.AddNode (new InstitutionNode (i));
            if (institutions.Length > 0)
                institutionNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
            total.Text = institutions.Length.ToString() + " " + Catalog.GetString("records");
        }

        public void NewStore ()
        {
            institutions = Institution.FindAllOrderedByName ();

            store = new Gtk.NodeStore (typeof(InstitutionNode));

            foreach (Institution i in institutions)
                store.AddNode (new InstitutionNode (i));
            if (institutions.Length > 0)
                institutionNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
            total.Text = institutions.Length.ToString () + " " + Catalog.GetString("records");
        }

        public void UnselectAll ()
        {
            institutionNodeView.NodeSelection.UnselectAll();
        }

        public void Search(string searchString) {
          institutions = Institution.SimpleSearch(searchString);
          FillStore ();
        }

        protected void OnSearch (object sender, System.EventArgs e)
        {
          Search(searchEntry.Text);
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            ReloadStore();
            searchEntry.Text = "";
        }

        protected void OnSearchByLetter (object sender, System.EventArgs e)
        {
            if (sender != null) {
                Gtk.NodeSelection selection = (Gtk.NodeSelection)sender;
                LetterNode node = (LetterNode) selection.SelectedNode;
                Search(node.Letter);
            }
        }
    }
}