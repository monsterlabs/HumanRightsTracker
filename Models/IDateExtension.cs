using System;

namespace HumanRightsTracker.Models
{
    public interface IDateExtension
    {
        string StartDateAsString {
            get;
        }

        string EndDateAsString {
            get;
        }
    }
}

