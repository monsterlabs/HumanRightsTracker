using System;
using HumanRightsTracker.Models;


namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonAndInstitutionSelect : Gtk.Bin
    {
        protected Boolean isEditable;

        public PersonAndInstitutionSelect ()
        {
            this.Build ();
        }

        public Person Person {
            get {
                return person.Person;
            }

            set {
                person.Person = value;
            }
        }

        public Institution Institution {
            get {
                return institution.Institution;
            }

            set {
                institution.Institution = value;
            }
        }

        public Job Job {
            get {
                return job.Active as Job;
            }

            set {
                job.Active = value;
            }
        }

        public Boolean IsEditable {
            get { return this.isEditable; }
            set {
                  this.isEditable = value;
                  person.IsEditable = value;
                  institution.IsEditable = value;
                  job.IsEditable = value;
                }
        }
    }
}

