using System;
using System.Collections.Generic;

namespace HumanRightsTracker.Models
{
    public interface AffiliatedRecord
    {

        string[] AffiliatedColumnData ();
        int Id {
            get;
        }
    }
}

