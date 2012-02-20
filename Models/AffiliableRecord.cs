using System;
using System.Collections.Generic;

namespace HumanRightsTracker.Models
{
    public interface AffiliableRecord
    {

        string[] AffiliationColumnData ();
        int Id {
            get;
        }
    }
}

