using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("perpetrator_acts")]
    public class PerpetratorAct : ActiveRecordValidationBase<PerpetratorAct>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("perpetrator_id"), ValidateNonEmpty]
        public Perpetrator Perpetrator { get; set; }
        [BelongsTo("human_rights_violation_id"), ValidateNonEmpty]
        public HumanRightsViolation HumanRightsViolation { get; set; }

        [BelongsTo("act_place_id"), ValidateNonEmpty]
        public ActPlace ActPlace { get; set; }

        [Property("location")]
        public String Location { get; set; }
    }
}