using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("person_relationship_types")]
    public class PersonRelationshipType : ActiveRecordValidationBase<PersonRelationshipType>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property]
        public String Notes { get; set; }
    }
}