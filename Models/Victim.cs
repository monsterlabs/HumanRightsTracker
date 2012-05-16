using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("victims")]
    public class Victim : ActiveRecordValidationBase<Victim>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("person_id"), ValidateNonEmpty]
        public Person Person { get; set; }
        [BelongsTo("act_id"), ValidateNonEmpty]
        public Act Act { get; set; }
        [BelongsTo("victim_status_id")]
        public VictimStatus VictimStatus { get; set; }

        [Property("characteristics")]
        public String Characteristics { get; set; }

        [HasMany(typeof(Perpetrator), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList Perpetrators { get; set; }

    }
}
