using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("institution_people")]
    public class InstitutionPerson : ActiveRecordValidationBase<InstitutionPerson>, ListableRecord, IComparable<InstitutionPerson>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("institution_id")]
        [ValidateNonEmpty]
        public Institution Institution { get; set; }

        [BelongsTo("person_id")]
        [ValidateNonEmpty]
        public Person Person { get; set; }

        [BelongsTo("affiliation_type_id")]
        [ValidateNonEmpty]
        public AffiliationType AffiliationType { get; set; }

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
                this.Person.Fullname,
                this.AffiliationType.Name,
                this.Institution.Name,
                "",
                ""
            };

            if (this.start_date.HasValue)
                data[3] = this.StartDateAsString ();
            if (this.end_date.HasValue)
                data[4] = this.EndDateAsString ();

            return data;
        }

        public string StartDateAsString() {
            return (string)DateAsString (StartDateType, start_date);
        }

        public string EndDateAsString() {
            return (string)DateAsString (EndDateType, end_date);
        }

        public string DateAsString(DateType dateType, DateTime? date) {
            string date_string = date.Value.ToShortDateString ();
            if (dateType != null) {
                if (dateType.Id == 3)
                {
                    date_string = date.Value.Month + " de " + date.Value.Year.ToString ();
                }
                else if (dateType.Id == 4)
                {
                    date_string = date.Value.Year.ToString ();
                }
            }
            return date_string;
        }

        public int CompareTo(InstitutionPerson other)
        {
            if (other == null) return 1;
            DateTime timeX = this.start_date.Value;
            DateTime timeY = other.start_date.Value;
            return timeY.CompareTo(timeX);
        }
    }
}