using System;
using System.Collections;
using System.Collections.Generic;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PeopleTab : Gtk.Bin, TabWithDefaultButton
    {
        bool isImmigrant = false;
        bool hasLoaded = false;
        public PeopleTab ()
        {
            //Console.WriteLine("Building People Tab...");
            //this.Build ();
        }

        public void InitialSetup ()
        {
            if (hasLoaded == false) {
                Console.WriteLine("Building People Tab...");
                this.Build ();
                peoplelist.IsImmigrant = this.isImmigrant;
                peoplelist.ReloadStore();
                this.ShowAll ();
                hasLoaded = true;
                Console.WriteLine("People Tab Complete.");
                show.IsEditing = false;
            }
        }

        protected virtual void PersonSelected (object sender, System.EventArgs e)
        {
            show.IsImmigrant = this.isImmigrant;
            if (sender != null)
            {
                show.Show();
                Person person = (Person) sender;
                show.Person = person;
                removeButton.Sensitive = true;
            } else {

                show.Hide();
                removeButton.Sensitive = false;
            }
        }

        protected void onAdd (object sender, System.EventArgs e)
        {
            Person p = new Person();
            peoplelist.UnselectAll();
            show.Person = p;
            show.IsImmigrant = this.isImmigrant;
            show.IsEditing = true;
            show.Show();
            return;
        }

        protected void onRemove (object sender, System.EventArgs e)
        {
            Person p = show.Person;
            if (p.HasRelateRecords == false) {
                p.DeleteAndFlush();
                peoplelist.ReloadStore ();
            } else {
                Console.WriteLine("We can't delete this person because it is still associated to other records");
                new ValidationErrorsDialog ("We can't delete this person because it is still associated to other records", (Gtk.Window)this.Toplevel);
            }
            return;
        }

        public Gtk.Button DefaultButton ()
        {
            return peoplelist.SearchButton;
        }

        protected void OnPersonSaved (object sender, System.EventArgs e)
        {
            peoplelist.ReloadStore();
            show.Person = sender as Person;
            show.Show();
        }

        public bool IsImmigrant {
            get {
                return this.isImmigrant;
            }
            set {
                this.isImmigrant = value;

            }
        }
    }
}
