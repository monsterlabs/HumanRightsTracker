using System;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Collections;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class PersonNode : Gtk.TreeNode
    {
        public PersonNode (Person person)
        {
            Person = person;
            Byte[] icon = Person.Photo.Icon;
            if (icon != null)
                Photo = new Gdk.Pixbuf (icon);
            else
                Photo = Gdk.Pixbuf.LoadFromResource ("Views.images.MissingIcon.jpg");
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
        HashSet<Person> personList = new HashSet<Person>(new ARComparer<Person>());

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

        public void SimpleSearch(string searchString) {
            people = Person.SimpleSearch(searchString, this.isImmigrant);
            personList.Clear();
            foreach (Person p in people)
                personList.Add(p);
        }

        protected void FillNodeStore () {
            tree.NodeStore.Clear ();
            foreach (Person p in personList)
                tree.NodeStore.AddNode (new PersonNode (p));

            total.Text = personList.Count.ToString () + " records";
        }

        protected void FindVictims (string searchString) {
            foreach (Person p in Person.FindVictims(this.isImmigrant, searchString))
                personList.Add(p);
        }

        protected void FindPerpetrators (string searchString) {
            foreach (Person p in Person.FindPerpetrators(this.isImmigrant, searchString))
                personList.Add(p);
        }

        protected void FindInterventors (string searchString) {
            foreach (Person p in Person.FindInterventors(this.isImmigrant, searchString))
                personList.Add(p);
        }

        protected void FindSupporters (string searchString) {
            foreach (Person p in Person.FindSupporters(this.isImmigrant, searchString))
                personList.Add(p);
        }

        protected void SearchWithFilters(string searchString) {
            personList.Clear();
            if (victims_checkbutton.Active)
                FindVictims (searchString);
            else if (perpetrators_checkbutton.Active)
                FindPerpetrators (searchString);
            else if (interventors_checkbutton.Active)
                FindInterventors (searchString);
            else if (interventors_checkbutton.Active)
                FindSupporters (searchString);
            else
                ReloadStore ();
        }

        protected void Search (string searchString) {
            if ( areFiltersActivated() )
                SearchWithFilters(searchString);
            else
                SimpleSearch(searchString);
        }

        protected virtual void onSearch (object sender, System.EventArgs e)
        {
            Search (searchEntry.Text);
            FillNodeStore ();
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
            personList.Clear();
            FillStore ();
        }

        public void NewStore ()
        {
            store = new Gtk.NodeStore (typeof(PersonNode));
            FillStore ();
        }

        public void FillStore ()
        {
            people = Person.FindAllByPersonType(this.isImmigrant);

            foreach (Person p in people) {
                personList.Add(p);
                store.AddNode (new PersonNode (p));
            }

            if (people.Length > 0)
                tree.NodeSelection.SelectPath(new Gtk.TreePath("0"));

            total.Text = people.Length + " records";
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
                FillNodeStore ();
            }
        }

        protected void OnReloadButtonClicked (object sender, System.EventArgs e)
        {
            victims_checkbutton.Active = false;
            perpetrators_checkbutton.Active = false;
            interventors_checkbutton.Active = false;
            supporters_checkbutton.Active = false;
            ReloadStore ();
        }

        protected ICriterion isImmigrantCriterion () {
            ICriterion criterion = Restrictions.Eq("IsImmigrant", this.isImmigrant);
            return criterion;
        }

        protected void OnVictimsCheckbuttonToggled (object sender, System.EventArgs e)
        {
            SearchWithFilters(searchEntry.Text);
            FillNodeStore ();
        }

        protected void OnPerpetratorsCheckbuttonToggled (object sender, System.EventArgs e)
        {
            SearchWithFilters(searchEntry.Text);
            FillNodeStore ();
        }

        protected void OnInterventorsCheckbuttonToggled (object sender, System.EventArgs e)
        {
           SearchWithFilters(searchEntry.Text);
           FillNodeStore ();
        }

        protected void OnSupportersCheckbuttonToggled (object sender, System.EventArgs e)
        {
            SearchWithFilters(searchEntry.Text);
            FillNodeStore ();
        }

         protected Boolean areFiltersActivated () {
            return (victims_checkbutton.Active || perpetrators_checkbutton.Active || interventors_checkbutton.Active ||
                    supporters_checkbutton.Active );
        }

    }
}

