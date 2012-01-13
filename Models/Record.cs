using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using System.Collections;

namespace HumanRightsTracker.Models
{
    public class Record
    {
        int documentableId;
        String documentableType;

        public Record (int documentableId, String documentableType)
        {
            this.documentableId = documentableId;
            this.documentableType = documentableType;
        }

        public Byte[] Content {
            get {
                String hql = "select d.Content from Document d where d.DocumentableId = :DocumentableId and d.DocumentableType = :DocumentableType";
                HqlBasedQuery query = new HqlBasedQuery(typeof(Document), hql);
                query.SetParameter("DocumentableId", this.documentableId);
                query.SetParameter("DocumentableType", this.documentableType);

                ArrayList result = (ArrayList)ActiveRecordMediator.ExecuteQuery(query);
                if (result.Count != 0) {
                    return (Byte[])(result)[0];
                } else {
                    return null;
                }
            }
        }

        public Document Document {
            get {
                String hql = "select d from Document d where d.DocumentableId = :DocumentableId and d.DocumentableType = :DocumentableType";
                HqlBasedQuery query = new HqlBasedQuery(typeof(Document), hql);
                query.SetParameter("DocumentableId", this.documentableId);
                query.SetParameter("DocumentableType", this.documentableType);

                ArrayList result = (ArrayList)ActiveRecordMediator.ExecuteQuery(query);
                if (result.Count != 0) {
                    return (Document)(result)[0];
                } else {
                    return null;
                }
            }
        }
    }
}

