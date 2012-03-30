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
            //addBold ("Reporte narrativo de caso");
            //addField("Fecha de expedición", DateTime.Now.ToShortDateString ());

            addTitle ("Información general");

            addField ("Nombre del caso", acase.Name);
            addField ("Personas afectadas", acase.AffectedPeople.ToString ());
            if (acase.start_date != null)
                addField ("Fecha de inicio", acase.StartDateAsString);
            if (acase.end_date != null)
                addField ("Fecha de término", acase.EndDateAsString);

            if (acase.Places.Count > 0 ) {
                addTitle ("Lugares");
                foreach (Place place in acase.Places)
                {
                    addField ("", String.Format ("{0}, {1}, {2}",
                                  place.City.Name, place.State.Name, place.Country.Name));
                }
            }

            addTitle ("Descripción");
            addField ("", acase.NarrativeDescription);

            addTitle ("Resumen");
            addField ("", acase.Summary);

            addTitle ("Observaciones");
            addField ("", acase.Observations);

            addNewline();

            if (acase.TrackingInformation.Count > 0)
            {
                addTitle ("Seguimiento del caso");

                foreach (TrackingInformation info in acase.TrackingInformation)
                {
                    addField ("Clave", info.RecordId.ToString ());
                    addField ("Título", info.Title);
                    if (info.DateOfReceipt != null)
                        addField ("Fecha de recepción", info.DateAsString);
                    if (info.CaseStatus != null)
                        addField ("Estado", info.CaseStatus.Name);
                    addField ("Comentarios", info.Comments);
                    addNewline();
                }
            }

            addTitle ("Núcleo del caso");
            if (acase.Acts.Count > 0) {
                addTitle ("Derechos afectados y actos");
                foreach (Act act in acase.Acts) {
                    if (act.HumanRightsViolationCategory != null)
                        addField ("\tDerecho afectado", act.HumanRightsViolationCategory.Name);
                    if (act.HumanRightsViolation != null)
                        addField ("\tActo", act.HumanRightsViolation.Name);
                    if (act.ActStatus != null)
                        addField ("Estado",act.ActStatus.Name);
                    addField ("\tNo. afectados", act.AffectedPeopleNumber.ToString ());
                    addField ("\tResumen", act.Summary);
                    if (act.start_date != null)
                        addField ("\tFecha de inicio", act.StartDateAsString);
                    if (act.end_date != null)
                        addField ("\tFecha de término", act.EndDateAsString);

                    if (act.Victims.Count > 0)
                    {
                        addNewline();
                        addBold ("\t\tVíctimas");

                        int victim_counter = 1;
                        foreach (Victim victim in act.Victims)
                        {
                            addField (String.Format ("{0} {1}", "\t\t\tVíctima", victim_counter), victim.Person.Fullname);
                            addField ("\t\t\tCaracterísticas", victim.Characteristics);
                            if (victim.VictimStatus != null)
                                addField ("\t\t\tEstado", victim.VictimStatus.Name);
                            /*
                            if (victim.Person.ImmigrationAttempts.Count > 0 )
                                addField ("\t\t\tIntentos de inmigración", victim.Person.ImmigrationAttempts.ToString());
                             */
                            if (victim.Perpetrators.Count > 0 )
                            {
                                addBold ("\t\t\tPerpetradores");
                                int perpetrator_counter = 1;
                                foreach (Perpetrator perpetrator in victim.Perpetrators)
                                {
                                    if (perpetrator.Person != null)
                                        addField (String.Format ("{0} {1}", "\t\t\t\tPerpetrador", perpetrator_counter), perpetrator.Person.Fullname);
                                    if (perpetrator.PerpetratorType != null)
                                        addField ("\t\t\t\tTipo de perpetrador", perpetrator.PerpetratorType.Name);
                                    if (perpetrator.PerpetratorStatus != null)
                                        addField ("\t\t\t\tEstado del perpetrador", perpetrator.PerpetratorStatus.Name);
                                    if (perpetrator.InvolvementDegree != null)
                                        addField ("\t\t\t\tGrado de involucramiento", perpetrator.InvolvementDegree.Name);
                                    if (perpetrator.Institution != null)
                                        addField ("\t\t\t\tInstitución", perpetrator.Institution.Name);
                                    if (perpetrator.AffiliationType != null)
                                        addField ("\t\t\t\tTipo de afiliación", perpetrator.AffiliationType.Name);
                                    addNewline();
                                    perpetrator_counter++;
                                }
                            }
                            victim_counter++;
                        }
                        addNewline ();
                    }
                }
                addNewline ();
            }

            if (acase.Interventions.Count > 0 )
            {
                addTitle ("Intervenciones");

                foreach (Intervention intervention in acase.Interventions) {
                    if (intervention.Date != null)
                        addField ("Fecha de la intervención", intervention.Date.Value.ToShortDateString ());

                    if (intervention.Interventor != null)
                        addField("Interventor", intervention.Interventor.Fullname);

                    if (intervention.InterventorInstitution != null)
                        addField("Institución", intervention.InterventorInstitution.Name);

                    if (intervention.InterventorAffiliationType != null)
                        addField("Afiliación", intervention.InterventorAffiliationType.Name);

                    if (intervention.Supporter != null)
                        addField("Persona que soporta la intervención", intervention.Supporter.Fullname);

                    if (intervention.SupporterInstitution != null)
                        addField("Institución", intervention.SupporterInstitution.Name);

                    if (intervention.SupporterAffiliationType != null)
                        addField("Tipo de afiliación", intervention.SupporterAffiliationType.Name);

                    addField("Impacto", intervention.Impact);
                    addField("Respuesta", intervention.Response);

                    if (intervention.AffectedPeople.Count > 0)
                    {
                        addBold("Personas por las que se intervino");
                        foreach(InterventionAffectedPeople interventionAffectedPeople in intervention.AffectedPeople)
                        {
                            if (interventionAffectedPeople != null)
                                addField ("\t", interventionAffectedPeople.Person.Fullname);
                        }
                    }
                    addNewline();
                }

            }


            addTitle ("Información adicional");

            if (acase.DocumentarySources.Count > 0 )
            {
                addTitle ("Fuentes documentales");

                foreach (DocumentarySource docSource in acase.DocumentarySources) {
                    addField ("Nombre", docSource.Name);
                    if (docSource.Date != null)
                        addField ("Fecha de publicación", docSource.Date.Value.ToShortDateString ());
                    if (docSource.AccessDate != null)
                        addField ("Fecha de acceso", docSource.AccessDate.Value.ToShortDateString ());
                    if (docSource.AdditionalInfo != null)
                        addField ("Información adicional", docSource.AdditionalInfo);
                    if (docSource.ReliabilityLevel != null)
                        addField ("Nivel de confiabilidod", docSource.ReliabilityLevel.Name);
                    addField ("Observaciones", docSource.Observations);
                    addNewline();
                }
            }

            if (acase.InformationSources.Count > 0)
            {
                addTitle ("Fuentes de información personal");

                foreach (InformationSource infoSource in acase.InformationSources) {
                    if (infoSource.SourcePerson != null)
                        addField ("Nombre", infoSource.SourcePerson.Fullname);
                    if (infoSource.Date != null)
                        addField ("Fecha", infoSource.DateAsString);
                    if (infoSource.SourceAffiliationType != null)
                        addField ("Tipo de afiliación", infoSource.SourceAffiliationType.Name);
                    addField ("Observaciones", infoSource.Observations);
                    addNewline ();
                }
            }

            if (acase.CaseRelationships.Count > 0)
            {
                addTitle ("Relación entre casos");

                foreach (CaseRelationship relation in acase.CaseRelationships) {
                    if (relation.Case != null)
                        addField("Caso", relation.Case.Name);
                    if (relation.RelationshipType != null)
                        addField ("Tipo de relación", relation.RelationshipType.Name);
                    if (relation.RelatedCase != null) {
                        addField("Caso relacionado", relation.RelatedCase.Name);
                        if (relation.RelatedCase.start_date != null)
                            addField("Fecha de inicio", relation.RelatedCase.StartDateAsString);
                        if (relation.RelatedCase.end_date != null)
                            addField("Fecha de término", relation.RelatedCase.EndDateAsString);
                    }
                    addNewline ();
                }
            }

        }
    }
}

