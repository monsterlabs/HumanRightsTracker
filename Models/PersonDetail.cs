using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("person_details")]
    public class PersonDetail : ActiveRecordValidationBase<PersonDetail>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("number_of_sons")]
        public int NumberOfSons { get; set; }

        [Property("is_spanish_speaker")]
        public Boolean IsSpanishSpeaker { get; set; }

        [BelongsTo("indigenous_group_id")]
        public IndigenousGroup IndigenousGroup { get; set; }

        [BelongsTo("ethnic_group_id")]
        public EthnicGroup EthnicGroup { get; set; }

        [BelongsTo("scholarity_level_id")]
        public ScholarityLevel ScholarityLevel { get; set; }

        [BelongsTo("religion_id")]
        public Religion Religion { get; set; }

        [BelongsTo("person_id")]
        public Person Person { get; set; }

        [BelongsTo("job_id")]
        public Job MostRecentJob {get; set;}

    }
}
