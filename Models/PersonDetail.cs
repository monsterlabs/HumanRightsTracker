using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("person_details")]
    public class PersonDetail : ActiveRecordLinqBase<PersonDetail>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        public int NumberOfChildren { get; set; }
        [Property]
        public String MostRecentJob { get; set; }
        [Property]
        public String IndigenousGroup { get; set; }

        [BelongsTo("ethnic_group_id")]
        public EthnicGroup EthnicGroup { get; set; }
        [BelongsTo("scholarity_level_id")]
        public ScholarityLevel ScholarityLevel { get; set; }
        [BelongsTo("religion_id")]
        public Religion Religion { get; set; }
        [BelongsTo("person_id")]
        public Person Person { get; set; }
    }
}
