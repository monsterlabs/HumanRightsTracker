using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("interventions")]
    public class Intervention : ActiveRecordValidationBase<Intervention>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set;}

        [BelongsTo("intervention_type_id")]
        public InterventionType InterventionType { get; set; }

        [BelongsTo("interventor_id")]
        public Person Interventor { get; set; }

        [BelongsTo("supporter_id")]
        public Person Supporter { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? Date { get; set; }

        [Property]
        [ValidateNonEmpty]
        public String Impact { get; set; }

        [Property]
        [ValidateNonEmpty]
        public String Response { get; set; }

        [HasMany(typeof(InterventionAffectedPeople), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList AffectedPeople { get; set; }

    }
}

