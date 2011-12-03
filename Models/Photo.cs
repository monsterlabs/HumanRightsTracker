using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using System.Collections;

namespace HumanRightsTracker.Models
{
    public class Photo
    {
        int imageableId;
        String imageableType;

        public Photo (int imageableId, String imageableType)
        {
            this.imageableId = imageableId;
            this.imageableType = imageableType;
        }


        public Byte[] Original {
            get {
                String hql = "select i.Original from Image i where i.ImageableId = :ImageableId and i.ImageableType = :ImageableType";
                HqlBasedQuery query = new HqlBasedQuery(typeof(Image), hql);
                query.SetParameter("ImageableId", this.imageableId);
                query.SetParameter("ImageableType", this.imageableType);

                ArrayList result = (ArrayList)ActiveRecordMediator.ExecuteQuery(query);

                if (result.Count != 0) {
                    return (Byte[])(result)[0];
                } else {
                    return null;
                }
            }
        }

        public Byte[] Thumbnail {
            get {
                String hql = "select i.Thumbnail from Image i where i.ImageableId = :ImageableId and i.ImageableType = :ImageableType";
                HqlBasedQuery query = new HqlBasedQuery(typeof(Image), hql);
                query.SetParameter("ImageableId", this.imageableId);
                query.SetParameter("ImageableType", this.imageableType);

                ArrayList result = (ArrayList)ActiveRecordMediator.ExecuteQuery(query);

                if (result.Count != 0) {
                    return (Byte[])(result)[0];
                } else {
                    return null;
                }
            }
        }
        public Byte[] Icon {
            get {
                String hql = "select i.Icon from Image i where i.ImageableId = :ImageableId and i.ImageableType = :ImageableType";
                HqlBasedQuery query = new HqlBasedQuery(typeof(Image), hql);
                query.SetParameter("ImageableId", this.imageableId);
                query.SetParameter("ImageableType", this.imageableType);

                ArrayList result = (ArrayList)ActiveRecordMediator.ExecuteQuery(query);

                if (result.Count != 0) {
                    return (Byte[])(result)[0];
                } else {
                    return null;
                }
            }
        }

        public Image Image {
            get {
                String hql = "select i from Image i where i.ImageableId = :ImageableId and i.ImageableType = :ImageableType";
                HqlBasedQuery query = new HqlBasedQuery(typeof(Image), hql);
                query.SetParameter("ImageableId", this.imageableId);
                query.SetParameter("ImageableType", this.imageableType);

                ArrayList result = (ArrayList)ActiveRecordMediator.ExecuteQuery(query);

                if (result.Count != 0) {
                    return (Image)(result)[0];
                } else {
                    return null;
                }
            }
        }
    }
}

