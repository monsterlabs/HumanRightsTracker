using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("acts")]
    public class Act : ActiveRecordValidationBase<Act>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("case_id")]
        public int CaseId {get; set;}

        [BelongsTo("human_rights_violation_id")]
        public HumanRightsViolation HumanRightsViolation { get; set; }

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

