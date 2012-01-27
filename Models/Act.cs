using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("acts")]
    public class Act : ActiveRecordValidationBase<Act>, ListableRecord, IComparable<Act>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set;}

        [BelongsTo("human_rights_violation_category_id")]
        public HumanRightsViolationCategory HumanRightsViolationCategory { get; set; }

        [BelongsTo("human_rights_violation_id")]
        public HumanRightsViolation HumanRightsViolation { get; set; }

        [Property("settlement")]
        public String Settlement { get; set; }

        [Property("affected_people_number")]
        public int AffectedPeopleNumber { get; set; }

        [Property("summary")]
        public String Summary { get; set; }
        [Property("narrative_information")]
        public String NarrativeInformation { get; set; }
        [Property("comments")]
        public String Comments { get; set; }
        [Property("affiliation_group")]
        public String AffiliationGroup { get; set; }
        [Property("victim_observations")]
        public String VictimObservations { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? start_date { get; set; }

        [Property]
        public DateTime? end_date { get; set; }

        [BelongsTo("start_date_type_id")]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

        [BelongsTo("country_id")]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }

        [BelongsTo("act_status_id")]
        public ActStatus ActStatus { get; set; }
        [BelongsTo("victim_status_id")]
        public VictimStatus VictimStatus { get; set; }
        [BelongsTo("affiliation_type_id")]
        public AffiliationType AffiliationType { get; set; }
        [BelongsTo("location_type_id")]
        public LocationType LocationType { get; set; }

        [HasMany(typeof(Victim), Cascade=ManyRelationCascadeEnum.AllDeleteOrphan, Lazy=true)]
        public IList Victims { get; set; }

        public string[] ColumnData ()
        {
            string[] data = {
                this.HumanRightsViolationCategory.Name,
                this.HumanRightsViolation.Name,
                "",
                ""
            };

            if (this.start_date.HasValue)
                data[2] = this.start_date.Value.ToShortDateString ();
            if (this.end_date.HasValue)
                data[3] = this.end_date.Value.ToShortDateString ();

            return data;
        }

         public int CompareTo(Act other)
        {
            if (other == null) return 1;
            DateTime timeX = this.start_date.Value;
            DateTime timeY = other.start_date.Value;
            return timeY.CompareTo(timeX);
        }
    }
}

