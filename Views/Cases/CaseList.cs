using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

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
            cases = Case.FindAll (new ICriterion[] { Restrictions.InsensitiveLike("Name", searchEntry.Text, MatchMode.Anywhere)});
            CaseNodeView.NodeStore.Clear ();
            foreach (Case c in cases)
                CaseNodeView.NodeStore.AddNode (new CaseNode (c));
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
            cases = Case.FindAll ();

            CaseNodeView.NodeStore.Clear ();

            foreach (Case c in cases)
                CaseNodeView.NodeStore.AddNode (new CaseNode (c));
            if (cases.Length > 0)
            {
                CaseNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
            }
        }

        public void NewStore ()
        {
            cases = Case.FindAll ();

            store = new Gtk.NodeStore (typeof(CaseNode));

            foreach (Case c in cases)
                store.AddNode (new CaseNode (c));
            if (cases.Length > 0)
            {
                CaseNodeView.NodeSelection.SelectPath(new Gtk.TreePath("0"));
            }
        }

        public void UnselectAll ()
        {
            CaseNodeView.NodeSelection.UnselectAll();
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            cases = Case.FindAll ();
            CaseNodeView.NodeStore.Clear ();
            foreach (Case c in cases)
                CaseNodeView.NodeStore.AddNode (new CaseNode (c));
        }
   }
}