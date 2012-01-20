using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class CaseRelationshipWindow : Gtk.Window, IEditable
    {
        public event EventHandler Saved;
        public event EventHandler Canceled;

        CaseRelationship case_relationship;
        bool isEditable;

        public CaseRelationshipWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public CaseRelationshipWindow (CaseRelationship case_relationship, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.Saved= onSave;
            this.TransientFor = parent;
            CaseRelationship = case_relationship;
        }

        public CaseRelationshipWindow (Case c, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.Saved = OnSave;
            this.TransientFor = parent;
            case_relationship = new CaseRelationship ();
            case_relationship.Case = c;
            CaseRelationship = case_relationship;
        }

        public CaseRelationship CaseRelationship {
            get { return this.case_relationship; }
            set {
                case_relationship = value;
                if (case_relationship != null) {
                    related_case_select.Case = case_relationship.RelatedCase;
                    relationship_type.Active = case_relationship.RelationshipType;
                }
            }
        }

         public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
            }
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            this.Destroy ();
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (case_relationship.Id < 1) {
                newRow = true;
            }

            case_relationship.RelatedCase = related_case_select.Case;
            case_relationship.RelationshipType = relationship_type.Active as RelationshipType;


            if (case_relationship.IsValid()) {
                case_relationship.Save ();

                if (newRow) {
                    case_relationship.Case.CaseRelationships.Add (CaseRelationship);
                    case_relationship.Case.SaveAndFlush ();
                }

                if (Saved != null)
                   Saved (this.case_relationship, e);
                this.Destroy ();

            } else {
                Console.WriteLine( String.Join(",",case_relationship.ValidationErrorMessages) );
                new ValidationErrorsDialog (case_relationship.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

