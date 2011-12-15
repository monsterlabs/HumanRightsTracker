using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("information_sources")]
    public class InformationSource : ActiveRecordValidationBase<InformationSource>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set;}

        [BelongsTo("source_person_id")]
        public Person SourcePerson { get; set; }

        [BelongsTo("source_institution_id")]
        public Institution SourceInstitution { get; set; }

        [BelongsTo("source_job_id")]
        public Job SourceJob { get; set; }

        [BelongsTo("reported_person_id")]
        public Person ReportedPerson { get; set; }

        [BelongsTo("reported_institution_id")]
        public Institution ReportedInstitution { get; set; }

        [BelongsTo("reported_job_id")]
        public Job ReportedJob { get; set; }

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
   }
}