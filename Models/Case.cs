using System;
using System.Collections;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("cases")]
    public class Case : ActiveRecordValidationBase<Case>
    {
        protected IList acts;

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Name { get; set; }

        [Property("affected_persons")]
        public int AffectedPeople { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? start_date { get; set; }

        [Property]
        public DateTime? end_date { get; set; }

        [BelongsTo("start_date_type_id")]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

    }
}

