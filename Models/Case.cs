using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("cases")]
    public class Case : ActiveRecordValidationBase<Case>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Name { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime StartDate { get; set; }

        [Property]
        public DateTime EndDate { get; set; }

        [BelongsTo("start_date_type_id")]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

    }
}

