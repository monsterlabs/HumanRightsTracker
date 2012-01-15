using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("case_relationships")]
    public class CaseRelationship : ActiveRecordValidationBase<CaseRelationship>, ListableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set; }

        [BelongsTo("related_case_id")]
        public Case RelatedCase { get; set; }

        [BelongsTo("relationship_type_id")]
        public RelationshipType RelationshipType { get; set; }

        public string[] ColumnData ()
        {
            string[] data = {
                this.RelationshipType.Name,
                this.RelatedCase.Name,
                "",
                ""
            };

            if (this.RelatedCase.start_date.HasValue)
                data[2] = this.RelatedCase.start_date.Value.ToShortDateString ();
            if (this.RelatedCase.end_date.HasValue)
                data[3] = this.RelatedCase.end_date.Value.ToShortDateString ();

            return data;
        }

    }
}

