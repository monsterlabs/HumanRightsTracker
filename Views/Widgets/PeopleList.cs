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
            Photo = "";
            //TODO Fullname property for Person model.
            Name = person.Firstname + " " +  person.Lastname;
        }

        public Person Person;

        [Gtk.TreeNodeValue (Column=0)]
        public string Photo;

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
            tree.AppendColumn ("Photo", new Gtk.CellRendererText (), "text", 0);
            tree.AppendColumn ("Name", new Gtk.CellRendererText (), "text", 1);

            tree.NodeSelection.Changed += new System.EventHandler (OnSelectionChanged);
        }

        protected void OnSelectionChanged (object o, System.EventArgs args)
        {
            Gtk.NodeSelection selection = (Gtk.NodeSelection) o;
            PersonNode node = (PersonNode) selection.SelectedNode;
            if (SelectionChanged != null)
                SelectionChanged(node.Person, args);
        }

        protected virtual void onSearch (object sender, System.EventArgs e)
        {
            people = Person.FindAll(new ICriterion[] { Restrictions.InsensitiveLike("Firstname", searchEntry.Text, MatchMode.Anywhere)});
            store.Clear();
            foreach (Person p in people)
                store.AddNode(new PersonNode(p));
        }


        Gtk.NodeStore store;
        Gtk.NodeStore Store {
            get {
                if (store == null) {
                    people = Person.FindAll();

                    store = new Gtk.NodeStore (typeof (PersonNode));

                    foreach (Person p in people)
                        store.AddNode(new PersonNode(p));
                }
                return store;
            }
        }

        public Gtk.Button SearchButton { get { return searchButton; } }
    }
}

