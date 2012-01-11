using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class DocumentarySourceWindow : Gtk.Window
    {
        public event EventHandler OnDocumentarySourceSaved = null;

        DocumentarySource documentary_source;
        bool isEditable;

        public DocumentarySourceWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public DocumentarySourceWindow (DocumentarySource ds, EventHandler onSave, Gtk.Window parent) :
            base(Gtk.WindowType.Toplevel)
       {
           this.Build ();
           this.Modal = true;
           this.OnDocumentarySourceSaved= onSave;
           this.TransientFor = parent;
           DocumentarySource = ds;

       }

       public DocumentarySource DocumentarySource {
           get { return this.documentary_source; }
           set {
               documentary_source = value;
               if (documentary_source != null) {

                    name.Text = documentary_source.Name;
                    additional_info.Text = documentary_source.AdditionalInfo;
                    source_information_type.Active = documentary_source.SourceInformationType;
                    url.Text = documentary_source.Url;
                    language.Active = documentary_source.Language;
                    indigenous_language.Active = documentary_source.IndigenousLanguage;
                    reliability_level.Active = documentary_source.ReliabilityLevel;
                    observations.Text = documentary_source.Observations;
                    comments.Text = documentary_source.Comments;

                    if (documentary_source.Id != null) {
                        if (documentary_source.Date != null)
                            publication_date.CurrentDate = documentary_source.Date.Value;

                        if (documentary_source.AccessDate != null)
                            access_date.CurrentDate = documentary_source.AccessDate.Value;

                        person_or_institution_selector.Person = documentary_source.ReportedPerson;
                        person_or_institution_selector.Job = documentary_source.ReportedJob;
                        person_or_institution_selector.Institution = documentary_source.ReportedInstitution;
                        person_or_institution_selector.AllSet = true;
                    }

                }
            }
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            this.Destroy ();
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            documentary_source.Name = name.Text;
            documentary_source.AdditionalInfo = additional_info.Text;
            documentary_source.SourceInformationType = source_information_type.Active as SourceInformationType;
            documentary_source.Date = publication_date.CurrentDate;
            documentary_source.Url = url.Text;
            documentary_source.AccessDate = access_date.CurrentDate;

            documentary_source.Language = language.Active as Language;
            documentary_source.IndigenousLanguage = indigenous_language.Active as IndigenousLanguage;
            documentary_source.ReliabilityLevel = reliability_level.Active as ReliabilityLevel;
            documentary_source.Observations = observations.Text;
            documentary_source.Comments = comments.Text;

            documentary_source.ReportedPerson = person_or_institution_selector.Person;
            documentary_source.ReportedInstitution = person_or_institution_selector.Institution;
            documentary_source.ReportedJob = person_or_institution_selector.Job;

            if (documentary_source.IsValid()) {
                documentary_source.Save ();
                OnDocumentarySourceSaved (documentary_source, e);
                this.Destroy();
            } else {
                Console.WriteLine( String.Join(",",documentary_source.ValidationErrorMessages) );
                new ValidationErrorsDialog (documentary_source.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }

        }
    }
}
