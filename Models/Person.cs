using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("people")]
    public class Person : ActiveRecordValidationBase<Person>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Firstname { get; set; }

        [Property]
        [ValidateNonEmpty]
        public String Lastname { get; set; }

        [Property]
        [ValidateNonEmpty]
        public Boolean Gender { get; set; }

        [Property]
        public String Alias { get; set; }

        [Property]
        [ValidateNonEmpty]
        public DateTime Birthday { get; set; }

        [Property]
        public String Settlement { get; set; }

        [BelongsTo("country_id"), ValidateNonEmpty]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }
        [BelongsTo("marital_status_id"), ValidateNonEmpty]
        public MaritalStatus MaritalStatus { get; set; }

        private IList details = new ArrayList();
        [HasMany(typeof(PersonDetail))]
        public IList PersonDetails
        {
            get { return details; }
            set { details = value; }
        }

        public String Fullname
        {
            get
            {
                return Lastname + " " +  Firstname;
            }
        }

        public Image Photo
        {
            get
            {
                Image photo = Image.FindOne (new ICriterion[] { Restrictions.And (
                        Restrictions.Eq ("ImageableId", this.Id),
                        Restrictions.Eq ("ImageableType", "People")
                )});
                return photo;
            }

        }
    }
}
