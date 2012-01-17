using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("tracking_information")]
    public class TrackingInformation : ActiveRecordValidationBase<TrackingInformation>, ListableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("record_id")]
        public int RecordId { get; set; }

        [Property("title")]
        public String Title { get; set; }

        [Property("date_of_receipt")]
        public DateTime? DateOfReceipt { get; set; }

        [Property("comments")]
        public String Comments { get; set; }

        [BelongsTo("case_id")]
        public Case Case { get; set; }

        [BelongsTo("case_status_id")]
        public CaseStatus CaseStatus { get; set; }

        [BelongsTo("date_type_id")]
        public DateType DateType { get; set; }

        public Record Record
        {
            get {
                return new Record (this.Id, "TrackingInformation");
            }
        }

        public string[] ColumnData ()
        {
            string[] data = {
                this.RecordId.ToString (),
                this.Title,
                ""
            };

            if (this.DateOfReceipt.HasValue)
                data[2] = this.DateOfReceipt.Value.ToShortDateString ();

            return data;
        }
    }
}

