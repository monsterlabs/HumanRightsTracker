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
        public String Name {
            get { return (Name == null) ? "" : Name; }
            set { Name = value; }
        }

        [Property]
        public String Abbrev {
            get { return (Abbrev == null) ? "" : Abbrev; }
            set { Abbrev = value; }
        }

        [Property]
        public String Location {
           get { return (Location == null) ? "" : Location; }
           set { Location = value; }
        }

        [Property]
        public String Phone {
            get { return (Phone == null) ? "" : Phone; }
            set { Phone = value; }
        }

        [Property]
        public String Fax {
            get { return (Fax == null) ? "" : Fax; }
            set { Fax = value; }
        }

        [Property]
        public String Email {
            get { return (Email == null) ? "" : Email; }
            set { Email = value; }
        }

        [Property]
        public String Url {
            get { return (Url == null) ? "" : Url; }
            set { Url = value; }
        }

        [BelongsTo("institution_type_id")]
        public InstitutionType InstitutionType { get; set; }


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
