using System;
using System.Linq;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonOrInstitutionSelector : Gtk.Bin
    {
        protected Person person;
        protected Institution institution;
        protected Job job;
        protected Boolean allset = false;

        protected PersonAndInstitutionSelect personAndInstitutionWidget;

        public PersonOrInstitutionSelector ()
        {
            this.Build ();
        }

        public Institution Institution {
            get {  return institution; }
            set { institution = value; }
        }

        public Person Person {
            get {  return person; }
            set { person = value; }
        }

         public Job Job {
            get {  return job; }
            set { job = value; }
        }

        public Boolean AllSet {
            get { return allset; }
            set {
                allset = value;
                if (allset == true) {
                    if (person != null) {
                        radiobutton1.Active = true;
                        setPersonAndInstitutionSelect();
                    } else {
                        radiobutton2.Active = true;
                        setInstitutionSelect();
                    }
                    vbox.ShowAll ();
                }
            }

        }

        protected void setPersonAndInstitutionSelect() {
            PersonAndInstitutionSelect personAndInstitutionWidget = new PersonAndInstitutionSelect();
            personAndInstitutionWidget.Person = this.person;
            personAndInstitutionWidget.Institution = this.institution;
            personAndInstitutionWidget.Job = this.job;
            destroyVboxChildren();
            vbox.PackEnd (personAndInstitutionWidget);
        }

        protected void setInstitutionSelect() {
            InstitutionSelect institutionSelectWidget = new InstitutionSelect();
            institutionSelectWidget.Institution = this.institution;
            destroyVboxChildren();
            vbox.PackEnd (institutionSelectWidget);
        }

        protected void destroyVboxChildren () {
            if (vbox.AllChildren.Cast<Gtk.Widget>().ToArray().Length > 1)
               vbox.AllChildren.Cast<Gtk.Widget>().ToArray()[1].Destroy();
        }


        protected void OnRadiobutton2Toggled (object sender, System.EventArgs e)
        {
            destroyVboxChildren();
            setInstitutionSelect();
            vbox.ShowAll();
        }

        protected void OnRadiobutton1Toggled (object sender, System.EventArgs e)
        {
            destroyVboxChildren();
            setPersonAndInstitutionSelect ();
            vbox.ShowAll();
        }

        protected void OnShown (object sender, System.EventArgs e)
        {
            if (person != null) {
                radiobutton1.Active = true;
                setPersonAndInstitutionSelect();
            } else {
                radiobutton2.Active = true;
                setInstitutionSelect();
            }
            this.ShowAll ();
        }
    }
}

