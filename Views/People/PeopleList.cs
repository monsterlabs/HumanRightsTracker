using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class PersonNode : Gtk.TreeNode
    {
        public PersonNode (Person person)
        {
            Person = person;
            if (person.Photo != null)
                Photo = new Gdk.Pixbuf (person.Photo.Icon);
            else
                Photo = Gdk.Pixbuf.LoadFromResource ("Views.images.Missing.jpg");
            Name = person.Fullname;
        }

        public Person Person;
        [Gtk.TreeNodeValue (Column=0)]
        public Gdk.Pixbuf Photo;
        [Gtk.TreeNodeValue (Column=1)]
        public string Name;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleList : Gtk.Bin
    {
        Person[] people;

        public event EventHandler SelectionChanged;
        public event EventHandler SelectionWithDoubleClick;
        bool isImmigrant = false;

        public PeopleList ()
        {
            this.Build ();
            tree.NodeStore = Store;
            tree.AppendColumn ("Photo", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
            tree.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 1);

            tree.NodeSelection.Changed += new System.EventHandler (OnSelectionChanged);
        }

        protected void OnSelectionChanged (object o, System.EventArgs args)
        {
            Gtk.NodeSelection selection = (Gtk.NodeSelection)o;
            PersonNode node = (PersonNode)selection.SelectedNode;
            if (SelectionChanged != null)
            {
                Person p = null;
                if (node != null)
                    p = node.Person;
                SelectionChanged (p, args);
            }
        }
        public void Search(string searchString) {
            people = Person.FindAll (new ICriterion[] { Restrictions.Or (
                        Restrictions.InsensitiveLike("Firstname", searchString, MatchMode.Anywhere),
                        Restrictions.InsensitiveLike("Lastname", searchString, MatchMode.Anywhere)
                     ), isImmigrantCriterion () });
            tree.NodeStore.Clear ();
            foreach (Person p in people)
                tree.NodeStore.AddNode (new PersonNode (p));

        }

        protected virtual void onSearch (object sender, System.EventArgs e)
        {
            Search(searchEntry.Text);
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
            people = Person.FindAll(isImmigrantCriterion ());
            tree.NodeStore.Clear ();

            foreach (Person p in people)
                tree.NodeStore.AddNode (new PersonNode (p));
            if (people.Length > 0)
                tree.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void NewStore ()
        {
            people = Person.FindAll(isImmigrantCriterion ());

            store = new Gtk.NodeStore (typeof(PersonNode));

            foreach (Person p in people)
                store.AddNode (new PersonNode (p));
            if (people.Length > 0)
                tree.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void UnselectAll ()
        {
            tree.NodeSelection.UnselectAll();
        }

        public bool IsImmigrant {
            get {
                return this.isImmigrant;
            }
            set {
                this.isImmigrant = value;
            }
        }

        protected void OnRowActivated (object o, Gtk.RowActivatedArgs args)
        {
            if (SelectionWithDoubleClick != null)
            {
                Gtk.NodeSelection selection = ((Gtk.NodeView)o).NodeSelection;
                Person p = ((PersonNode)selection.SelectedNode).Person;
                SelectionWithDoubleClick (p, args);
            }
        }

        protected void OnSearchByLetter (object sender, System.EventArgs e)
        {
            if (sender != null) {
                Gtk.NodeSelection selection = (Gtk.NodeSelection)sender;
                LetterNode node = (LetterNode) selection.SelectedNode;
                Search(node.Letter);
            }
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            ReloadStore ();
        }
        protected ICriterion isImmigrantCriterion () {
            ICriterion criterion = Restrictions.Eq("IsImmigrant", this.isImmigrant);
            return criterion;
        }
    }
}

