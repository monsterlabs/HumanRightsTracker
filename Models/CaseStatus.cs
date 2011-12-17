using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("case_statuses")]
    public class CaseStatus : ActiveRecordValidationBase<CaseStatus>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("name")]
        public String Name { get; set; }
    }
}

