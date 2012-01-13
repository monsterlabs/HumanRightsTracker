using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("case_relationships")]
    public class CaseRelationship : ActiveRecordValidationBase<CaseRelationship>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set; }

        [BelongsTo("related_case_id")]
        public Case RelatedCase { get; set; }

        [BelongsTo("relationship_type_id")]
        public RelationshipType RelationshipType { get; set; }
    }
}

