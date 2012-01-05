using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("administrative_information")]
    public class AdministrativeInformation : ActiveRecordValidationBase<AdministrativeInformation>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("date_of_receipt")]
        public DateTime? DateOfReceipt { get; set; }

        [Property("project_name")]
        public String ProjectName { get; set; }

        [Property("project_description")]
        public String ProjectDescription { get; set; }

        [Property("comments")]
        public String Comments { get; set; }

        [Property("records")]
        public String Records { get; set; }

        [BelongsTo("case_id")]
        public Case Case { get; set; }

        [BelongsTo("case_status_id")]
        public CaseStatus CaseStatus { get; set; }

        [BelongsTo("date_type_id")]
        public DateType DateType { get; set; }
    }
}

