using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Mono.Unix;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class CaseNode : Gtk.TreeNode
    {
        public CaseNode (Case mycase)
        {
            Case = mycase;
            Name = mycase.Name;
        }

        public Case Case;
        [Gtk.TreeNodeValue (Column=0)]
        public string Name;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseList : Gtk.Bin
    {
        Case[] cases;

        public event EventHandler SelectionChanged;

        public CaseList ()
        {
            this.Build ();
            CaseNodeView.NodeStore = Store;
            CaseNodeView.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 0);
            CaseNodeView.NodeSelection.Changed += new System.EventHandler (OnSelectionChanged);
        }

        protected void OnSelectionChanged (object o, System.EventArgs args)
        {
            Gtk.NodeSelection selection = (Gtk.NodeSelection)o;
            CaseNode node = (CaseNode)selection.SelectedNode;
            if (SelectionChanged != null)
            {
                Case c = null;
                if (node != null)
                    c = node.Case;
                SelectionChanged (c, args);
            }
        }

        protected virtual void onSearch (object sender, System.EventArgs e)
        {
            cases = Case.SimpleSearch(searchEntry.Text);
            FillStore ();
        }

        public void FillStore() {
            CaseNodeView.NodeStore.Clear ();

            foreach (Case c in cases)
                CaseNodeView.NodeStore.AddNode (new CaseNode (c));

            if (cases.Length > 0)
            {
                CaseNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
            }

            total.Text = cases.Length.ToString() + " " + Catalog.GetString("records");
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
            cases = Case.FindAllOrderedByName();
            FillStore();
        }

        public void NewStore ()
        {
            cases = Case.FindAllOrderedByName();

            store = new Gtk.NodeStore (typeof(CaseNode));

            foreach (Case c in cases)
                store.AddNode (new CaseNode (c));
            if (cases.Length > 0)
            {
                CaseNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
            }
            total.Text = cases.Length.ToString () + " " + Catalog.GetString("records");
        }

        public void UnselectAll ()
        {
            CaseNodeView.NodeSelection.UnselectAll();
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            cases = Case.FindAllOrderedByName();
            FillStore ();
        }

        protected void OnLetterSelectionChanged (object sender, System.EventArgs e)
        {
            if (sender != null) {
                Gtk.NodeSelection selection = (Gtk.NodeSelection)sender;
                LetterNode node = (LetterNode) selection.SelectedNode;
                cases = Case.SimpleSearch(node.Letter);
                FillStore ();
            }

        }
   }
}