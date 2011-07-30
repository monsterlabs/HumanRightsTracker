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
                Photo = new Gdk.Pixbuf (person.Photo.Thumbnail);
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

        protected virtual void onSearch (object sender, System.EventArgs e)
        {
            people = Person.FindAll (new ICriterion[] { Restrictions.Or (
                        Restrictions.InsensitiveLike("Firstname", searchEntry.Text, MatchMode.Anywhere),
                        Restrictions.InsensitiveLike("Lastname", searchEntry.Text, MatchMode.Anywhere)
                     )});
            tree.NodeStore.Clear ();
            foreach (Person p in people)
                tree.NodeStore.AddNode (new PersonNode (p));
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
            people = Person.FindAll ();

            tree.NodeStore.Clear ();

            foreach (Person p in people)
                tree.NodeStore.AddNode (new PersonNode (p));
            if (people.Length > 0)
                tree.NodeSelection.SelectPath(new Gtk.TreePath("0"));
        }

        public void NewStore ()
        {
            people = Person.FindAll ();

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
    }
}

