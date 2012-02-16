using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Mono.Unix;
using System.Linq;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseShow : Gtk.Bin
    {
        public Case mycase;
        protected TrackingInformation tracking_info;
        private EditableHelper editable_helper;
        protected bool isEditing;

        public event EventHandler CaseSaved;

        public CaseShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.isEditing = false;
            ConnectActsHandlers();
            ConnectTrackingHandlers ();
            ConnectPlacesHandlers();
            ConnectDocumentarySourceHandlers();
            ConnectInformationSourceHandlers();
            ConnectCaseRelationshipHandlers();
            ConnectInterventionsHandlers();
        }

        public Case Case {
            get { return this.mycase; }
            set {
                mycase = value;
                if (mycase != null) {
                    mycase.Refresh();
                    nameEntry.Text = mycase.Name == null ? "" : mycase.Name;
                    affectedPeople.Text = mycase.AffectedPeople.ToString();
                    startDateSelector.setDate(mycase.start_date, mycase.StartDateType);
                    endDateSelector.setDate(mycase.end_date, mycase.EndDateType);

                    description.Text = mycase.NarrativeDescription;
                    summary.Text = mycase.Summary;
                    observations.Text = mycase.Observations;
                    if (mycase.Id != 0 ) {
                        act_list.Records = value.Acts.Cast<ListableRecord>().ToList();
                        case_relationship_list.Records = value.CaseRelationships.Cast<ListableRecord>().ToList();
                        interventionlist.Records = value.Interventions.Cast<ListableRecord>().ToList();
                        documentarysourcelist.Records = value.DocumentarySources.Cast<ListableRecord>().ToList();
                        informationsourcelist.Records = value.InformationSources.Cast<ListableRecord>().ToList();
                        List<TrackingInformation> trackings = value.TrackingInformation.Cast<TrackingInformation>().ToList ();
                        trackings.Sort();
                        trackinglist.Records = trackings.Cast<ListableRecord>().ToList ();
                        placeslist.Records = value.Places.Cast<ListableRecord>().ToList ();
                    } else {
                        general_info_expander.Expanded = true;
                        places_expander.Hide ();
                        tracking_info_expander.Hide ();
                        core_expander.Hide ();
                        additional_info_expander.Hide ();
                    }
                }
                IsEditing = false;
            }
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                    if (mycase.Id != 0 ) {
                        places_expander.Visible = true;
                        tracking_info_expander.Visible = true;
                        core_expander.Visible = true;
                        additional_info_expander.Visible = true;
                    }
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                    if (mycase != null && mycase.Id == 0) {
                        this.Hide();
                    }
                }

                this.editable_helper.SetAllEditable (value);
            }
        }

        public void HideEditingButtons () {
            hbuttonbox9.Hide ();
        }

        public void ReloadActs () {
            List<Act> acts = this.Case.Acts.Cast<Act>().ToList ();
            acts.Sort ();
            act_list.Records = acts.Cast<ListableRecord>().ToList ();
        }

        private void ReloadTrackings () {
            List<TrackingInformation> trackings = this.Case.TrackingInformation.Cast<TrackingInformation>().ToList ();
            trackings.Sort ();
            trackinglist.Records = trackings.Cast<ListableRecord>().ToList ();
        }

        private void ReloadPlaces () {
            List<ListableRecord> places = this.Case.Places.Cast<ListableRecord>().ToList ();
            placeslist.Records = places;
        }

        public void ReloadCaseRelationships () {
            List<CaseRelationship> case_relationships = this.Case.CaseRelationships.Cast<CaseRelationship>().ToList ();
            case_relationships.Sort ();
            case_relationship_list.Records = case_relationships.Cast<ListableRecord>().ToList ();
        }

        public void ReloadInformationSources () {
            List<InformationSource> information_sources = this.Case.InformationSources.Cast<InformationSource>().ToList ();
            information_sources.Sort ();
            informationsourcelist.Records = information_sources.Cast<ListableRecord>().ToList ();
        }

        public void ReloadDocumentarySources () {
            List<DocumentarySource> documentary_sources = this.Case.DocumentarySources.Cast<DocumentarySource>().ToList ();
            documentary_sources.Sort ();
            documentarysourcelist.Records = documentary_sources.Cast<ListableRecord>().ToList ();
        }

        private void ReloadInterventions() {
            List<ListableRecord> interventions = this.Case.Interventions.Cast<ListableRecord>().ToList ();
            interventionlist.Records = interventions;
        }

        public void ConnectActsHandlers () {
            act_list.NewButtonPressed += (sender, e) => {
                new ActDetailWindow (this.Case, (o, args) => {
                    this.ReloadActs ();
                },  (Gtk.Window) this.Toplevel);
            };
            act_list.DeleteButtonPressed += (sender, e) => {
                Act record = sender as Act;
                this.Case.Acts.Remove(record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadActs ();
            };
            act_list.DetailButtonPressed += (sender, e) => {
                Act record = sender as Act;
                new ActDetailWindow(record, (o, args) => {
                    this.ReloadActs ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectTrackingHandlers () {
            trackinglist.NewButtonPressed += (sender, e) => {
                new TrackingDetailWindow (this.Case, (o, args) => {
                    this.ReloadTrackings ();
                },  (Gtk.Window) this.Toplevel);
            };
            trackinglist.DeleteButtonPressed += (sender, e) => {
                TrackingInformation t = sender as TrackingInformation;
                this.Case.TrackingInformation.Remove(t);
                if (t.Id >= 1) {
                    t.DeleteAndFlush ();
                }
                this.ReloadTrackings ();
            };
            trackinglist.DetailButtonPressed += (sender, e) => {
                TrackingInformation t = sender as TrackingInformation;
                new TrackingDetailWindow(t, (o, args) => {
                    this.ReloadTrackings ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectPlacesHandlers() {
            placeslist.NewButtonPressed += (sender, e) => {
                new PlaceDetailWindow (this.Case, (o, args) => {
                    this.ReloadPlaces ();
                }, (Gtk.Window) this.Toplevel);
            };
            placeslist.DeleteButtonPressed += (sender, e) => {
                Place p = sender as Place;
                this.Case.Places.Remove (p);
                if (p.Id >= 1) {
                    p.DeleteAndFlush ();
                }
                this.ReloadPlaces ();
            };
            placeslist.DetailButtonPressed += (sender, e) => {
                Place p = sender as Place;
                new PlaceDetailWindow(p, (o, args) => {
                    this.ReloadPlaces ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectCaseRelationshipHandlers() {
            case_relationship_list.NewButtonPressed += (sender, e) => {
                new CaseRelationshipDetailWindow(this.Case, (o, args) => {
                    this.ReloadCaseRelationships ();
                }, (Gtk.Window) this.Toplevel);
            };
            case_relationship_list.DeleteButtonPressed += (sender, e) => {
                CaseRelationship record = sender as CaseRelationship;
                this.Case.CaseRelationships.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadCaseRelationships ();
            };
            case_relationship_list.DetailButtonPressed += (sender, e) => {
                CaseRelationship record = sender as CaseRelationship;
                new CaseRelationshipDetailWindow(record, (o, args) => {
                    this.ReloadCaseRelationships ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectInformationSourceHandlers() {
            informationsourcelist.NewButtonPressed += (sender, e) => {
                new InformationSourceDetailWindow(this.Case, (o, args) => {
                    this.ReloadInformationSources ();
                }, (Gtk.Window) this.Toplevel);
            };
            informationsourcelist.DeleteButtonPressed += (sender, e) => {
                InformationSource record = sender as InformationSource;
                this.Case.InformationSources.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadInformationSources ();

            };
            informationsourcelist.DetailButtonPressed += (sender, e) => {
                InformationSource record = sender as InformationSource;
                new InformationSourceDetailWindow(record, (o, args) => {
                    this.ReloadInformationSources ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectDocumentarySourceHandlers() {
            documentarysourcelist.NewButtonPressed += (sender, e) => {
                new DocumentarySourceDetailWindow(this.Case, (o, args) => {
                    this.ReloadDocumentarySources ();
                }, (Gtk.Window) this.Toplevel);
            };
            documentarysourcelist.DeleteButtonPressed += (sender, e) => {
                DocumentarySource record = sender as DocumentarySource;
                this.Case.DocumentarySources.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadDocumentarySources ();

            };
            documentarysourcelist.DetailButtonPressed += (sender, e) => {
                DocumentarySource record = sender as DocumentarySource;
                new DocumentarySourceDetailWindow(record, (o, args) => {
                    this.ReloadDocumentarySources ();
               }, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectInterventionsHandlers() {
            interventionlist.NewButtonPressed += (sender, e) => {
                new InterventionDetailWindow(this.Case, (o, args) => {
                    this.ReloadInterventions ();
                }, (Gtk.Window) this.Toplevel);
            };
            interventionlist.DeleteButtonPressed += (sender, e) => {
                Intervention i = sender as Intervention;
                this.Case.Interventions.Remove(i);
                if (i.Id >= 1) {
                    i.DeleteAndFlush ();
                }
                this.ReloadInterventions ();
            };
            interventionlist.DetailButtonPressed += (sender, e) => {
                Intervention i = sender as Intervention;
                new InterventionDetailWindow(i, (o, args) => {
                    this.ReloadInterventions ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            mycase.Name = nameEntry.Text;
            mycase.start_date = startDateSelector.SelectedDate ();
            mycase.StartDateType = startDateSelector.SelectedDateType ();
            mycase.AffectedPeople = System.Convert.ToInt32(affectedPeople.Text);

            mycase.end_date = endDateSelector.SelectedDate ();
            mycase.EndDateType = endDateSelector.SelectedDateType ();

            mycase.NarrativeDescription = description.Text;
            mycase.Summary = summary.Text;
            mycase.Observations = observations.Text;

            if (mycase.IsValid())
            {
                mycase.SaveAndFlush ();

                this.IsEditing = false;
                if (CaseSaved != null)
                    CaseSaved (mycase, e);
            } else {
                Console.WriteLine( String.Join(",", mycase.ValidationErrorMessages) );
                new ValidationErrorsDialog (mycase.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

        protected void OnExport (object sender, System.EventArgs e)
        {
            new CaseReportWindow (this.Case);
        }
    }
}
