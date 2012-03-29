using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    using Mixins;

    [ActiveRecord("institution_relationships")]
    public class InstitutionRelationship : ActiveRecordValidationBase<InstitutionRelationship>, ListableRecord, IComparable<InstitutionRelationship>, AffiliableRecord, IDateExtension
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("institution_id")]
        [ValidateNonEmpty]
        public Institution Institution { get; set; }

        [BelongsTo("related_institution_id")]
        [ValidateNonEmpty]
        public Institution RelatedInstitution { get; set; }

        [BelongsTo("institution_relationship_type_id")]
        [ValidateNonEmpty]
        public InstitutionRelationshipType InstitutionRelationshipType { get; set; }

        [BelongsTo("start_date_type_id")]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

        [Property]
        public DateTime? start_date { get; set; }

        [Property]
        public DateTime? end_date { get; set; }

        [Property("comments")]
        public String Comments { get; set;}

        public string[] ColumnData ()
        {
            string[] data = {
                this.Institution.Name,
                (this.InstitutionRelationshipType.Name+ " de "),
                this.RelatedInstitution.Name,
                "",
                ""
            };

            if (this.start_date.HasValue)
                data[3] = this.StartDateAsString;
            if (this.end_date.HasValue)
                data[4] = this.EndDateAsString;

            return data;
        }

        public string[] AffiliationColumnData ()
        {
            string[] data = {
                this.Institution.Name,
                (this.InstitutionRelationshipType.Name+ " de "),
                 this.RelatedInstitution.Name,
                "",
                ""
            };

            if (this.start_date.HasValue)
                data[3] = this.StartDateAsString;
            if (this.end_date.HasValue)
                data[4] = this.EndDateAsString;

            return data;
        }

        public string StartDateAsString {
            get {
                return this.DateAsString(StartDateType, start_date);
            }
        }

        public string EndDateAsString {
            get {
                return this.DateAsString(EndDateType, end_date);
            }
        }

        public int CompareTo(InstitutionRelationship other)
        {
            if (other == null) return 1;
            DateTime timeX = this.start_date.Value;
            DateTime timeY = other.start_date.Value;
            return timeY.CompareTo(timeX);
        }
    }
}