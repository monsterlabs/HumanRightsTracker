using System;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.Content.Text;
using AODL.ExternalExporter.PDF;

using HumanRightsTracker.Models;

namespace Reports
{
    public class CaseReportGenerator : TextReportGenerator
    {
        public CaseReportGenerator (Case acase)
        {
            addBold ("Reporte narrativo de caso");
            addField("Fecha de expedición", DateTime.Now.ToShortDateString ());

            addTitle ("DATOS GENERALES");

            addField ("Nombre del caso", acase.Name);
            addField ("Fecha de inicio", acase.start_date.Value.ToShortDateString ());
            foreach (Place place in acase.Places)
            {

                addField ("Localidades", String.Format ("{0}, {1}, {2}",
                                                      place.City.Name, place.State.Name, place.Country.Name));
            }
            addField ("No. personas afectadas", acase.AffectedPeople.ToString ());

            addTitle ("SEGUIMIENTO DEL CASO");

            addField ("Resumen", acase.Summary);
            addField ("Observaciones", acase.Observations);

            addNewline();

            foreach (TrackingInformation info in acase.TrackingInformation)
            {
                addBold (info.Title);
                addField ("Comentarios", info.Comments);
                addNewline();
            }

            addTitle ("DETALLES DE ACTOS");

            foreach (Act act in acase.Acts) {
                addBold ("\t" + act.HumanRightsViolation.Name);
                addNewline ();
                if (act.ActStatus != null)
                    addField ("Estado",act.ActStatus.Name);
                addField ("\tNo. afectados", act.AffectedPeopleNumber.ToString ());
                addField ("\tResumen", act.Summary);
                addField ("\tFecha de inicio", act.start_date.Value.ToShortDateString ());
                if (act.end_date != null)
                    addField ("\tFecha de término", act.end_date.Value.ToShortDateString ());
                addField ("\tObservaciones de la victima", act.VictimObservations);

                addBold ("\tVictimas");

                foreach (Victim victim in act.Victims) {
                    addField ("\t\tVictima", victim.Person.Fullname);
                    addField ("\t\tCaracterísticas", victim.Characteristics);
                    if (victim.VictimStatus != null)
                        addField ("\t\tEstado", victim.VictimStatus.Name);
                    addField ("\t\tIntentos de inmigración", victim.Person.ImmigrationAttempts.ToString());
                    addBold ("\t\tPerpetradares");
                    foreach (Perpetrator perpetrator in victim.Perpetrators) {
                        addField ("\t\t\tPerpetrador", perpetrator.Person.Fullname);
                        if (perpetrator.Institution != null) {
                            addField ("\t\t\tInstitución", perpetrator.Institution.Name);
                        }
                        if (perpetrator.PerpetratorType != null)
                            addField ("\t\t\tTipo de perpetrador", perpetrator.PerpetratorType.Name);
                        if (perpetrator.PerpetratorStatus != null)
                            addField ("\t\t\tEstado del perpetrador", perpetrator.PerpetratorStatus.Name);
                        if (perpetrator.InvolvementDegree != null)
                            addField ("\t\t\tGrado de involucramiento", perpetrator.InvolvementDegree.Name);
                    }
                }

                addNewline ();
            }
        }
    }
}

