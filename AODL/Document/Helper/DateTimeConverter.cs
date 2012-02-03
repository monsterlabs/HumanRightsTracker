/*************************************************************************
 *
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER
 * 
 * Copyright 2008 Sun Microsystems, Inc. All rights reserved.
 * 
 * Use is subject to license terms.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0. You can also
 * obtain a copy of the License at http://odftoolkit.org/docs/license.txt
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * 
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 ************************************************************************/

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AODL.Document.Helper
{
    public class DateTimeConverter
    {
        public static DateTime GetDateTimeFromString (string val)
        {
            Regex regex = new Regex(@"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})T(?<hour>\d{2}):(?<minute>\d{2}):(?<second>\d{2}).(?<millisecond>\d{2})");
            Match m = regex.Match(val);
            if (m.Success)
            {
                DateTime res = new DateTime(Int32.Parse(m.Groups["year"].Value),Int32.Parse(m.Groups["month"].Value), Int32.Parse(m.Groups["day"].Value), Int32.Parse(m.Groups["hour"].Value), Int32.Parse(m.Groups["minute"].Value), Int32.Parse(m.Groups["second"].Value), Int32.Parse(m.Groups["millisecond"].Value));
                return res;
            }
            return new DateTime();
        }

        public static string GetStringFromDateTime(DateTime val)
        {
            string res = String.Format("{0}-{1}-{2}T{3}:{4}:{5}.{6}", val.Year, val.Month, val.Day, val.Hour, val.Minute, val.Second, val.Millisecond);
            return res;
        }
    }
}

