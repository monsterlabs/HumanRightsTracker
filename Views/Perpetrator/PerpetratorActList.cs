using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class PerpetratorActNode : Gtk.TreeNode
    {
        public PerpetratorActNode (PerpetratorAct perpetratorAct)
        {
            PerpetratorAct = perpetratorAct;
            Name = PerpetratorAct.HumanRightsViolation.Name;
        }

        public PerpetratorAct PerpetratorAct;
        [Gtk.TreeNodeValue (Column=0)]
        public string Name;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorActList : Gtk.Bin
    {
        PerpetratorAct[] perpetratorActs;
        Perpetrator perpetrator;

        public event EventHandler SelectionChanged;
        public event EventHandler SelectionWithDoubleClick;

        public PerpetratorActList ()
        {
            this.Build ();
            tree.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 0);

            tree.NodeSelection.Changed += new System.EventHandler (OnSelectionChanged);
        }

        public PerpetratorAct[] PerpetratorActs {
            get {
                return this.perpetratorActs;
            }
            set {
                perpetratorActs = value;
            }
        }
        protected void OnSelectionChanged (object o, System.EventArgs args)
        {
            Gtk.NodeSelection selection = (Gtk.NodeSelection)o;
            PerpetratorActNode node = (PerpetratorActNode)selection.SelectedNode;
            if (SelectionChanged != null)
            {
                PerpetratorAct p = null;
                if (node != null)
                    p = node.PerpetratorAct;
                SelectionChanged (p, args);
            }
        }

        public Perpetrator Perpetrator {
            get {
                return this.perpetrator;
            }
            set {
                perpetrator = value;
                if (perpetrator != null)
                    NewStore ();
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

        public void ReloadStore ()
        {
            if (perpetrator.Id > 0) {
                perpetratorActs = PerpetratorAct.FindAllByProperty("Perpetrator", perpetrator);
            } else {
                perpetratorActs = new PerpetratorAct[0];
            }

            store.Clear ();

            foreach (PerpetratorAct p in perpetratorActs)
                store.AddNode (new PerpetratorActNode (p));

            tree.NodeStore = store;

            if (perpetratorActs.Length > 0)
                tree.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void NewStore ()
        {
            store = new Gtk.NodeStore (typeof(PerpetratorActNode));

            ReloadStore ();
        }

        public void UnselectAll ()
        {
            tree.NodeSelection.UnselectAll();
        }

        protected void OnRowActivated (object o, Gtk.RowActivatedArgs args)
        {
            if (SelectionWithDoubleClick != null)
            {
                Gtk.NodeSelection selection = ((Gtk.NodeView)o).NodeSelection;
                PerpetratorAct p = ((PerpetratorActNode)selection.SelectedNode).PerpetratorAct;
                SelectionWithDoubleClick (p, args);
            }
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            ReloadStore ();
        }

    }
}

