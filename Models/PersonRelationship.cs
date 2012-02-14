using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;


namespace HumanRightsTracker.Models
{
    [ActiveRecord("person_relationships")]
    public class PersonRelationship : ActiveRecordValidationBase<PersonRelationship>, ListableRecord, IComparable<PersonRelationship>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("person_id")]
        public Person Person { get; set; }

        [BelongsTo("related_person_id")]
        public Person RelatedPerson { get; set; }

        [BelongsTo("person_relationship_type_id")]
        public PersonRelationshipType PersonRelationshipType { get; set; }

        [BelongsTo("start_date_type_id")]
        [ValidateNonEmpty]
        public DateType StartDateType { get; set; }

        [BelongsTo("end_date_type_id")]
        public DateType EndDateType { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime? start_date { get; set; }

        [Property]
        public DateTime? end_date { get; set; }

        [Property("comments")]
        public String Comments { get; set;}


        public string[] ColumnData ()
        {
            string[] data = {
                this.Person.Fullname,
                this.PersonRelationshipType.Name,
                this.RelatedPerson.Fullname,
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

        public int CompareTo(PersonRelationship other)
        {
            if (other == null) return 1;
            DateTime timeX = this.start_date.Value;
            DateTime timeY = other.start_date.Value;
            return timeY.CompareTo(timeX);
        }
    }
}

