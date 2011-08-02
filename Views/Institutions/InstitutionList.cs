using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class InstitutionNode : Gtk.TreeNode
    {
        public InstitutionNode (Institution institution)
        {
            Institution = institution;
            if (institution.Photo != null)
                Photo = new Gdk.Pixbuf (institution.Photo.Thumbnail);
            else
                Photo = Gdk.Pixbuf.LoadFromResource ("Views.images.Missing.jpg");
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
            institutions = Institution.FindAll ();

            institutionNodeView.NodeStore.Clear ();

            foreach (Institution i in institutions)
                institutionNodeView.NodeStore.AddNode (new InstitutionNode (i));
            if (institutions.Length > 0)
                institutionNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void NewStore ()
        {
            institutions = Institution.FindAll ();

            store = new Gtk.NodeStore (typeof(InstitutionNode));

            foreach (Institution i in institutions)
                store.AddNode (new InstitutionNode (i));
            if (institutions.Length > 0)
                institutionNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void UnselectAll ()
        {
            institutionNodeView.NodeSelection.UnselectAll();
        }

        protected void OnSearch (object sender, System.EventArgs e)
        {
          institutions = Institution.FindAll (new ICriterion[] { Restrictions.InsensitiveLike("Name", searchEntry.Text, MatchMode.Anywhere)});

          institutionNodeView.NodeStore.Clear ();
          foreach (Institution i in institutions)
            institutionNodeView.NodeStore.AddNode (new InstitutionNode (i));
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            institutions = Institution.FindAll ();
            institutionNodeView.NodeStore.Clear ();
            foreach (Institution i in institutions)
                institutionNodeView.NodeStore.AddNode (new InstitutionNode (i));
        }
    }
}