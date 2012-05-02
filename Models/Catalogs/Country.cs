using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Queries;
using Castle.Components.Validator;
using NHibernate.Criterion;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("countries")]
    public class Country : ActiveRecordValidationBase<Country>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Name { get; set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Citizen { get; set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Code { get; set; }

        public static Country FindDefault () {
            return  Country.FindOne (new ICriterion[] { Restrictions.Eq("Code", "MX") });
        }
    }
}
