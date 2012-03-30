using System;
using Mono.Unix;
using HumanRightsTracker.Models;
using System.Collections;
using System.Collections.Generic;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DocumentarySourceShow : Gtk.Bin
    {
        bool isEditable;
        DocumentarySource documentary_source;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public DocumentarySourceShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public DocumentarySource DocumentarySource {
            get { return this.documentary_source; }
            set {
                documentary_source = value;
                if (documentary_source != null) {
                    name.Text = documentary_source.Name;
                    additional_info.Text = documentary_source.AdditionalInfo;
                    documentarySourceType.Active = documentary_source.DocumentarySourceType;

                    url.Text = documentary_source.Url;
                    language.Active = documentary_source.Language;
                    indigenous_language.Active = documentary_source.IndigenousLanguage;
                    reliability_level.Active = documentary_source.ReliabilityLevel;
                    observations.Text = documentary_source.Observations;
                    comments.Text = documentary_source.Comments;

                    if (documentary_source.Id != 0) {
                        if (documentary_source.Date != null)
                            publication_date.CurrentDate = documentary_source.Date.Value;

                        if (documentary_source.AccessDate != null)
                            access_date.CurrentDate = documentary_source.AccessDate.Value;

                        person_or_institution_selector.Person = documentary_source.ReportedPerson;
                        person_or_institution_selector.AffiliationType = documentary_source.ReportedAffiliationType;
                        person_or_institution_selector.Institution = documentary_source.ReportedInstitution;
                        person_or_institution_selector.AllSet = true;
                    }
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
            if (documentary_source.Id < 1) {
                newRow = true;
            }

            documentary_source.Name = name.Text;
            documentary_source.AdditionalInfo = additional_info.Text;
            documentary_source.DocumentarySourceType = documentarySourceType.Active as DocumentarySourceType;
            documentary_source.Date = publication_date.CurrentDate;
            documentary_source.Url = url.Text;
            documentary_source.AccessDate = access_date.CurrentDate;

            documentary_source.Language = language.Active as Language;
            documentary_source.IndigenousLanguage = indigenous_language.Active as IndigenousLanguage;
            documentary_source.ReliabilityLevel = reliability_level.Active as ReliabilityLevel;
            documentary_source.Observations = observations.Text;
            documentary_source.Comments = comments.Text;

            documentary_source.ReportedPerson = person_or_institution_selector.Person;
            if (person_or_institution_selector.Institution != null) {
                documentary_source.ReportedInstitution = person_or_institution_selector.Institution;
                documentary_source.ReportedAffiliationType = person_or_institution_selector.AffiliationType;
            }

            if (documentary_source.IsValid()) {
                documentary_source.SaveAndFlush ();

                if (newRow) {
                    documentary_source.Case.DocumentarySources.Add (DocumentarySource);
                    documentary_source.Case.SaveAndFlush ();
                }

                if (Saved != null)
                   Saved (this.documentary_source, e);

                this.IsEditable = false;
            } else {
                Console.WriteLine( String.Join(",",documentary_source.ValidationErrorMessages) );
                new ValidationErrorsDialog (documentary_source.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
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

