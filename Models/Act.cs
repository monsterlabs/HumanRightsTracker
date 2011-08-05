using System;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("acts")]
    public class Act : ActiveRecordLinqBase<Act>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set; }
        [BelongsTo("human_rights_violation_id")]
        public State State { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime start_date { get; set; }

        [Property]
        public DateTime end_date { get; set; }

        [BelongsTo("start_date_type_id")]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

    }
}

