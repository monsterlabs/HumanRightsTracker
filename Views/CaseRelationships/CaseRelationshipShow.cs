using System;
using Mono.Unix;
using HumanRightsTracker.Models;
using System.Collections;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseRelationshipShow : Gtk.Bin
    {
        bool isEditable;
        CaseRelationship case_relationship;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public CaseRelationshipShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public CaseRelationship CaseRelationship {
            get { return this.case_relationship; }
            set {
                case_relationship = value;
                if (case_relationship != null ) {
                    if (case_relationship.Case != null)
                        case_name.Text = case_relationship.Case.Name;

                    related_case_select.Case = case_relationship.RelatedCase;
                    relationship_type.Active = case_relationship.RelationshipType;
                }
                IsEditable = false;
            }
        }

        public Boolean IsEditable {
            get { return this.isEditable;}
            set {
                isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }
        }


        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
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

               this.IsEditable = false;
           } else {
                Console.WriteLine( String.Join(",",case_relationship.ValidationErrorMessages) );
                new ValidationErrorsDialog (case_relationship.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }

        }
    }
}

