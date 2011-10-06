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
        [ValidateNonEmpty]
        public Person SourcePerson { get; set; }

        [BelongsTo("source_institution_id")]
        [ValidateNonEmpty]
        public Institution SourceInstitution { get; set; }

        [BelongsTo("source_job_id")]
        [ValidateNonEmpty]
        public Job SourceJob { get; set; }

        [BelongsTo("reported_person_id")]
        [ValidateNonEmpty]
        public Person ReportedPerson { get; set; }

        [BelongsTo("reported_institution_id")]
        [ValidateNonEmpty]
        public Institution ReportedInstitution { get; set; }

        [BelongsTo("reported_job_id")]
        [ValidateNonEmpty]
        public Job ReportedJob { get; set; }


        [BelongsTo("affiliation_type_id")]
        public AffiliationType AffiliationType { get; set; }

        [Property("date")]
        public DateTime Date { get; set; }

        [BelongsTo("language_id")]
        public Language Language { get; set; }

        [BelongsTo("indigenous_language_id")]
        public IndigenousLanguage IndigenousLanguage { get; set;}

        [Property("observations")]
        public String Observations { get; set;}

        [BelongsTo("reliability_level_id")]
        public RelialibityLevel ReliabilityLevel { get; set; }

        [Property("comments")]
        public String Comments { get; set;}
   }
}