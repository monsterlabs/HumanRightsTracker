using System;
using System.Collections.Generic;


namespace HumanRightsTracker.Models
{
    public interface ListableRecord
    {

        string[] ColumnData ();
        int Id {
            get;
        }
    }
}

