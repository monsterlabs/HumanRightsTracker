using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("person_acts")]
    public class PersonAct : ActiveRecordValidationBase<PersonAct>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("person_id"), ValidateNonEmpty]
        public Person Person { get; set; }
        [BelongsTo("act_id"), ValidateNonEmpty]
        public Act Act { get; set; }
        [BelongsTo("role_id"), ValidateNonEmpty]
        public Role Role { get; set; }

    }
}
