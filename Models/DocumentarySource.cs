using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("documentary_sources")]
    public class DocumentarySource : ActiveRecordValidationBase<DocumentarySource>, ListableRecord, IComparable<InformationSource>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set;}

        [BelongsTo("reported_person_id")]
        public Person ReportedPerson { get; set; }

        [BelongsTo("reported_institution_id")]
        public Institution ReportedInstitution { get; set; }

        [BelongsTo("reported_job_id")]
        public Job ReportedJob { get; set; }

        [Property("name")]
        public String Name { get; set;}

        [Property("additional_info")]
        public String AdditionalInfo { get; set;}

        [Property("date")]
        public DateTime? Date { get; set; }

        [BelongsTo("source_information_type_id")]
        public SourceInformationType SourceInformationType { get; set; }

        [Property("site_name")]
        public String SiteName { get; set;}

        [Property("url")]
        public String Url { get; set;}

        [Property("access_date")]
        public DateTime? AccessDate { get; set; }

        [BelongsTo("language_id")]
        public Language Language { get; set; }

        [BelongsTo("indigenous_language_id")]
        public IndigenousLanguage IndigenousLanguage { get; set;}

        [Property("observations")]
        public String Observations { get; set;}

        [BelongsTo("reliability_level_id")]
        public ReliabilityLevel ReliabilityLevel { get; set; }

        [Property("comments")]
        public String Comments { get; set;}


        public String reportedName
        {
            get {
                if (ReportedPerson!= null)
                    return ReportedPerson.Fullname;
                else if (ReportedInstitution != null)
                    return ReportedInstitution.Name;
                else
                    return "Not defined";
            }
        }

        public string[] ColumnData ()
        {
            string[] data = {
                this.Name,
                this.SourceInformationType.Name,
                "",
                "",
            };

            if (this.Date.HasValue)
                data[2] = this.Date.Value.ToShortDateString ();

            if (this.AccessDate.HasValue)
                data[3] = this.AccessDate.Value.ToShortDateString ();

            return data;
        }


        public int CompareTo(InformationSource other)
        {
            if (other == null) return 1;
            DateTime timeX = this.Date.Value;
            DateTime timeY = other.Date.Value;
            return timeY.CompareTo(timeX);
        }
   }
}