using System;
using Mono.Unix;
using HumanRightsTracker.Models;
using System.Collections;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InformationSourceShow : Gtk.Bin
    {
        bool isEditable;
        InformationSource information_source;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public InformationSourceShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public InformationSource InformationSource {
            get { return this.information_source; }
            set {
                    information_source = value;
                    if (information_source != null) {
                        source_person_selector.Person = information_source.SourcePerson;
                        source_person_selector.Institution = information_source.SourceInstitution;
                        source_person_selector.AffiliationType = information_source.SourceAffiliationType;
                        source_person_selector.AllSet = true;

                        affiliation_type.Active = information_source.AffiliationType;
                        language.Active = information_source.Language;
                        indigenous_language.Active = information_source.IndigenousLanguage;
                        reliability_level.Active = information_source.ReliabilityLevel;
                        observations.Buffer.Text = information_source.Observations;
                        comments.Buffer.Text = information_source.Comments;

                        datetypeanddateselector.setDate(information_source.Date, information_source.DateType);

                        reported_person_selector.Person = information_source.ReportedPerson;
                        reported_person_selector.Institution = information_source.ReportedInstitution;
                        reported_person_selector.AffiliationType = information_source.ReportedAffiliationType;
                        reported_person_selector.AllSet = true;
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

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (information_source.Id < 1) {
                newRow = true;
            }

            information_source.SourcePerson = source_person_selector.Person;
            information_source.SourceInstitution = source_person_selector.Institution;
            information_source.SourceAffiliationType = source_person_selector.AffiliationType;

            information_source.AffiliationType = affiliation_type.Active as AffiliationType;
            information_source.Language = language.Active as Language;
            information_source.IndigenousLanguage = indigenous_language.Active as IndigenousLanguage;
            information_source.ReliabilityLevel = reliability_level.Active as ReliabilityLevel;
            information_source.Observations = observations.Buffer.Text;
            information_source.Comments = comments.Buffer.Text;
            information_source.Date = datetypeanddateselector.SelectedDate();
            information_source.DateType = datetypeanddateselector.SelectedDateType ();
            information_source.ReportedPerson = reported_person_selector.Person;
            information_source.ReportedInstitution = reported_person_selector.Institution;
            information_source.ReportedAffiliationType = reported_person_selector.AffiliationType;

            if (information_source.IsValid()) {
                information_source.SaveAndFlush ();

                if (newRow) {
                    information_source.Case.InformationSources.Add (InformationSource);
                    information_source.Case.SaveAndFlush ();
                }

                if (Saved != null)
                   Saved (this.information_source, e);

                this.IsEditable = false;
            } else {
                Console.WriteLine( String.Join(",",information_source.ValidationErrorMessages) );
                new ValidationErrorsDialog (information_source.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
        }
    }
}

