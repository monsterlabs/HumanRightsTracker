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

        [Property("information_sourceable_type")]
        public String InformationSourceableType { get; set; }

        [Property("information_sourceable_id")]
        public int InformationSourceableId { get; set; }

        [Property("reported_personable_type")]
        public String ReportedPersonableType { get; set; }

        [Property("reported_personable_id")]
        public int ReportedPersonableId { get; set; }

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