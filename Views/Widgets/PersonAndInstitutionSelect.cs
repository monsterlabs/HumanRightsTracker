using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PersonAndInstitutionSelect : Gtk.Bin, IEditable
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

        public AffiliationType AffiliationType {
            get {
                return affiliation_type.Active as AffiliationType;
            }

            set {
                affiliation_type.Active = value;
            }
        }

        public Boolean IsEditable {
            get { return this.isEditable; }
            set {
                  this.isEditable = value;
                  person.IsEditable = value;
                  institution.IsEditable = value;
                  affiliation_type.IsEditable = value;
                }
        }
    }
}

