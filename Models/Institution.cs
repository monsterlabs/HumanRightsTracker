using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using NHibernate.Criterion;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("institutions")]
    public class Institution : ActiveRecordValidationBase<Institution>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property]
        public String Abbrev { get; set; }

        [Property]
        public String Location { get; set; }

        [Property]
        public String Phone { get; set; }

        [Property]
        public String Fax { get; set; }

        [Property]
        public String Email { get; set; }

        [Property]
        public String Url { get; set; }

        [BelongsTo("institution_type_id")]
        public InstitutionType InstitutionType { get; set; }

        [BelongsTo("institution_category_id")]
        public InstitutionCategory InstitutionCategory { get; set; }

        [BelongsTo("country_id"), ValidateNonEmpty]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }

        public Image Photo
        {
            get
            {
                Image photo = Image.FindOne (new ICriterion[] { Restrictions.And (
                        Restrictions.Eq ("ImageableId", this.Id),
                        Restrictions.Eq ("ImageableType", "Institution")
                )});
                return photo;
            }

        }
    }
}
