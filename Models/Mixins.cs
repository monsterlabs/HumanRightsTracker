using System;
using HumanRightsTracker.Models;
namespace Mixins
{
    public static class DateExtension {

        public static string DateAsString(this IDateExtension leftObject, DateType dateType, DateTime? date) {
            string date_string = "";

            if (date != null ) {

                date_string = date.Value.ToShortDateString ();

                if (dateType != null) {
                    if (dateType.Id == 3)
                    {
                        // YearMonth
                        date_string =String.Format("{0:y}", date);
                    }
                    else if (dateType.Id == 4)
                    {
                        date_string = date.Value.Year.ToString ();
                    }
                }
            }

            return date_string;
        }


    }
}
