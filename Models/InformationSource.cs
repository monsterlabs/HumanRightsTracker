using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;
using Mono.Unix;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("information_sources")]
    public class InformationSource : ActiveRecordValidationBase<InformationSource>, ListableRecord, IComparable<InformationSource>, AffiliableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set;}

        [BelongsTo("source_person_id")]
        public Person SourcePerson { get; set; }

        [BelongsTo("source_institution_id")]
        public Institution SourceInstitution { get; set; }

        [BelongsTo("source_affiliation_type_id")]
        public AffiliationType SourceAffiliationType { get; set; }

        [BelongsTo("reported_person_id")]
        public Person ReportedPerson { get; set; }

        [BelongsTo("reported_institution_id")]
        public Institution ReportedInstitution { get; set; }

        [BelongsTo("reported_affiliation_type_id")]
        public AffiliationType ReportedAffiliationType { get; set; }

        [BelongsTo("affiliation_type_id")]
        public AffiliationType AffiliationType { get; set; }

        [Property("date")]
        public DateTime? Date { get; set; }

        [BelongsTo("date_type_id")]
        public DateType DateType { get; set; }
        
        [BelongsTo("language_id")]
        public Language Language { get; set; }

        [BelongsTo("indigenous_language_id")]
        public IndigenousLanguage IndigenousLanguage { get; set;}

        [Property("observations")]
        public String Observations { get; set;}

        [BelongsTo("reliability_level_id")]
        public ReliabilityLevel ReliabilityLevel { get; set; }

        [Property("comments")]
        public String Comments { get; set;}

        public String sourceName
        {
            get {
                if (SourcePerson != null)
                    return SourcePerson.Fullname;
                 else if (SourceInstitution != null)
                    return SourceInstitution.Name;
                else
                    return "Not defined";
            }
        }

        public String reportedName
        {
            get {
                if (ReportedPerson!= null)
                    return ReportedPerson.Fullname;
                else if (ReportedInstitution != null)
                    return ReportedInstitution.Name;
                else
                    return "Not defined";
            }
        }

        public string[] ColumnData ()
        {
            string[] data = {
                this.sourceName,
                this.AffiliationType.Name,
                ""
            };

            if (this.Date.HasValue)
                data[2] = this.Date.Value.ToShortDateString ();

            return data;
        }

        public int CompareTo(InformationSource other)
        {
            if (other == null) return 1;
            DateTime timeX = this.Date.Value;
            DateTime timeY = other.Date.Value;
            return timeY.CompareTo(timeX);
        }


        public string[] AffiliationColumnData ()
        {
            string roleName = Catalog.GetString("Not defined role in information source record");
            string affiliationTypeName = Catalog.GetString("Not defined affiliation type in information source record");
            string institutionName = "";

            if (this.ReportedPerson != null)
            {
                roleName = Catalog.GetString("Reported person in source information");
                affiliationTypeName = this.ReportedAffiliationType.Name;
                institutionName = this.ReportedInstitution.Name;
            }
            else if (this.SourcePerson != null)
            {
                roleName = Catalog.GetString("As source in source information");
                affiliationTypeName = this.SourceAffiliationType.Name;
                institutionName = this.SourceInstitution.Name;
            }

            string[] data = {
                roleName,
                affiliationTypeName,
                institutionName,
                this.Case.Name,
                "",
            };

            if (this.Date.HasValue)
                data[4] = this.Date.Value.ToShortDateString ();

            return data;
        }
   }
}