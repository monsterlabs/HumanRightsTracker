using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("documents")]
    public class Document : ActiveRecordLinqBase<Document>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("content")]
        public Byte[] Content { get; set; }

        [Property("filename")]
        public String Filename { get; set; }

        [Property("content_type")]
        public String ContentType { get; set; }

        [Property("documentable_type")]
        public String DocumentableType { get; set; }

        [Property("documentable_id")]
        public int DocumentableId { get; set; }
    }
}