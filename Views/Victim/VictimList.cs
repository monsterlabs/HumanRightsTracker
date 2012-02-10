using System;
using System.Collections.Generic;
using System.Linq;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class VictimNode : Gtk.TreeNode
    {
        public VictimNode (Victim victim)
        {
            Victim = victim;
            if (victim.Person.Photo != null)
                Photo = new Gdk.Pixbuf (victim.Person.Photo.Icon);
            else
                Photo = Gdk.Pixbuf.LoadFromResource ("Views.images.Missing.jpg");
            Name = victim.Person.Fullname;
        }

        public Victim Victim;
        [Gtk.TreeNodeValue (Column=0)]
        public Gdk.Pixbuf Photo;
        [Gtk.TreeNodeValue (Column=1)]
        public string Name;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class VictimList : Gtk.Bin
    {
        Victim[] victims;
        Act act;

        public event EventHandler SelectionChanged;
        public event EventHandler SelectionWithDoubleClick;

        public VictimList ()
        {
            this.Build ();
            tree.AppendColumn ("Photo", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
            tree.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 1);

            tree.NodeSelection.Changed += new System.EventHandler (OnSelectionChanged);
        }

        public Victim[] Victims {
            get {
                return this.victims;
            }
            set {
                victims = value;
            }
        }
        protected void OnSelectionChanged (object o, System.EventArgs args)
        {
            Gtk.NodeSelection selection = (Gtk.NodeSelection)o;
            VictimNode node = (VictimNode)selection.SelectedNode;
            if (SelectionChanged != null)
            {
                Victim v = null;
                if (node != null)
                    v = node.Victim;
                SelectionChanged (v, args);
            }
        }

        public Act Act {
            get {
                return this.act;
            }
            set {
                act = value;
                if (act != null)
                    NewStore ();
            }
        }

//        public void Search(string searchString) {
//            people = Person.FindAll (new ICriterion[] { Restrictions.Or (
//                        Restrictions.InsensitiveLike("Firstname", searchString, MatchMode.Anywhere),
//                        Restrictions.InsensitiveLike("Lastname", searchString, MatchMode.Anywhere)
//                     ), isImmigrantCriterion () });
//            tree.NodeStore.Clear ();
//            foreach (Person p in people)
//                tree.NodeStore.AddNode (new PersonNode (p));
//
//        }
//
//        protected virtual void onSearch (object sender, System.EventArgs e)
//        {
//            Search(searchEntry.Text);
//        }

        Gtk.NodeStore store;

        Gtk.NodeStore Store {
            get {
                if (store == null) {
                    NewStore();
                }
                return store;
            }
        }

        //public Gtk.Button SearchButton { get { return searchButton; } }

        public void ReloadStore ()
        {
            if (act.Victims != null) {
                victims = act.Victims.Cast<Victim>().ToArray();
            } else {
                victims = new Victim[0];
            }
            store.Clear ();

            foreach (Victim v in victims)
                store.AddNode (new VictimNode (v));

            tree.NodeStore = store;

            if (victims.Length > 0)
                tree.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void NewStore ()
        {
            store = new Gtk.NodeStore (typeof(VictimNode));

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
                Victim v = ((VictimNode)selection.SelectedNode).Victim;
                SelectionWithDoubleClick (v, args);
            }
        }

//        protected void OnSearchByLetter (object sender, System.EventArgs e)
//        {
//            if (sender != null) {
//                Gtk.NodeSelection selection = (Gtk.NodeSelection)sender;
//                LetterNode node = (LetterNode) selection.SelectedNode;
//                Search(node.Letter);
//            }
//        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            ReloadStore ();
        }

    }
}

