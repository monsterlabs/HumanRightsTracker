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
        protected PersonAndInstitutionSelect personSelect;
        protected InstitutionSelect institutionSelect;
        protected Boolean isEditable;

        public PersonOrInstitutionSelector ()
        {
            this.Build ();
        }

        public Institution Institution {
            get {
                  if (personSelect.Person == null)
                    return institutionSelect.Institution;
                  else
                    return personSelect.Institution;
                }
            set { institution = value; }
        }

        public Person Person {
            get { return personSelect.Person; }
            set { person = value; }
        }

         public Job Job {
            get { return personSelect.Job; }
            set { job = value; }
        }

        public Boolean AllSet {
            get { return allset; }
            set {
                  allset = value;
                  if (allset == true)
                    setRadioButtonAndSelect ();
                }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                if (personSelect !=null)
                    personSelect.IsEditable = value;
                if (institutionSelect != null)
                    institutionSelect.IsEditable = value;
            }
        }

        protected void setRadioButtonAndSelect () {
            if ((person == null && institution == null)  || (person != null)){
                radiobutton1.Active = true;
                setPersonAndInstitutionSelect ();
            } else {
                 radiobutton2.Active = true;
                 setInstitutionSelect ();
            }
        }

        protected void setPersonAndInstitutionSelect () {
            personSelect = new PersonAndInstitutionSelect();
            personSelect.Person = this.person;
            personSelect.Institution = this.institution;
            personSelect.Job = this.job;
            destroyVboxChildren ();
            vbox.PackEnd (personSelect);
            vbox.ShowAll ();
        }

        protected void setInstitutionSelect () {
            institutionSelect = new InstitutionSelect();
            this.person = null;
            institutionSelect.Institution = this.institution;
            destroyVboxChildren ();
            vbox.PackEnd (institutionSelect);
            vbox.ShowAll ();
        }

        protected void destroyVboxChildren () {
            if (vbox.AllChildren.Cast<Gtk.Widget>().ToArray().Length > 1)
               vbox.AllChildren.Cast<Gtk.Widget>().ToArray()[1].Destroy();
        }


        protected void OnRadiobutton2Toggled (object sender, System.EventArgs e)
        {
            destroyVboxChildren ();
            setInstitutionSelect ();
        }

        protected void OnRadiobutton1Toggled (object sender, System.EventArgs e)
        {
            destroyVboxChildren ();
            setPersonAndInstitutionSelect ();
        }

        protected void OnShown (object sender, System.EventArgs e)
        {
            setRadioButtonAndSelect ();
            this.ShowAll ();
        }
    }
}
