using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InformationSourceWindow : Gtk.Window
    {
        public event EventHandler OnInterventionSaved = null;

        InformationSource information_source;
        bool isEditable;

        public InformationSourceWindow () :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public InformationSourceWindow (InformationSource i, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnInterventionSaved = onSave;
            this.TransientFor = parent;
            InformationSource = i;
        }

         public InformationSource InformationSource {
            get { return this.information_source; }
            set {
                information_source = value;
                if (information_source != null) {
                    affiliation_type.Active = information_source.AffiliationType;
                    language.Active = information_source.Language;
                    indigenous_language.Active = information_source.IndigenousLanguage;
                    reliability_level.Active = information_source.ReliabilityLevel;
                    observations.Buffer.Text = information_source.Observations;
                    comments.Buffer.Text = information_source.Comments;
                }
            }
        }

         public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                affiliation_type.IsEditable = value;
                language.IsEditable = value;
                indigenous_language.IsEditable = value;
                reliability_level.IsEditable = value;
                observations.Editable = value;
                comments.Editable = value;
            }
        }

    }
}

