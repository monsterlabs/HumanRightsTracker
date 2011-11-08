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
        [ValidateNonEmpty]
        public Case Case { get; set;}

        [BelongsTo("intervention_type_id")]
        [ValidateNonEmpty]
        public InterventionType InterventionType { get; set; }

        [BelongsTo("interventor_id")]
        [ValidateNonEmpty]
        public Person Interventor { get; set; }

        [BelongsTo("interventor_institution_id")]
        [ValidateNonEmpty]
        public Institution InterventorInstitution { get; set; }

        [BelongsTo("interventor_job_id")]
        [ValidateNonEmpty]
        public Job InterventorJob { get; set; }

        [BelongsTo("supporter_id")]
        [ValidateNonEmpty]
        public Person Supporter { get; set; }

        [BelongsTo("supporter_institution_id")]
        [ValidateNonEmpty]
        public Institution SupporterInstitution { get; set; }

        [BelongsTo("supporter_job_id")]
        [ValidateNonEmpty]
        public Job SupporterJob { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? Date { get; set; }

        [Property]
        public String Impact { get; set; }

        [Property]
        public String Response { get; set; }

        [HasMany(typeof(InterventionAffectedPeople), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList AffectedPeople { get; set; }

    }
}

