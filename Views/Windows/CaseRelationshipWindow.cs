using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class CaseRelationshipWindow : Gtk.Window
    {
        public event EventHandler OnCaseRelationshipSaved = null;

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
            this.OnCaseRelationshipSaved= onSave;
            this.TransientFor = parent;
            CaseRelationship = case_relationship;

        }

        public CaseRelationship CaseRelationship {
            get { return this.case_relationship; }
            set {
                case_relationship = value;
                if (case_relationship != null) {
                    case_relationship.RelatedCase = related_case_select.Case;
                    case_relationship.RelationshipType = relationship_type.Active as RelationshipType;
                }
            }
        }

         public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                related_case_select.IsEditable = value;
                relationship_type.IsEditable  = value;

            }
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            case_relationship.RelatedCase = related_case_select.Case;
            case_relationship.RelationshipType = relationship_type.Active as RelationshipType;

            if (case_relationship.IsValid()) {
                case_relationship.Save ();
                OnCaseRelationshipSaved (case_relationship, e);
                this.Destroy();
            } else {
                Console.WriteLine( String.Join(",",case_relationship.ValidationErrorMessages) );
                new ValidationErrorsDialog (case_relationship.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

