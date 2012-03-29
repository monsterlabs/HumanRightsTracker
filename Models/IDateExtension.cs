using System;

namespace HumanRightsTracker.Models
{
    public interface IDateExtension
    {

        int Id {
            get;
        }

        string StartDateAsString {
            get;
        }

        string EndDateAsString {
            get;
        }
    }
}

