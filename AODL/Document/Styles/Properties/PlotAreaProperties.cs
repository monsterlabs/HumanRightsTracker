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
using System.Xml ;

namespace AODL.Document.Styles.Properties
{
	/// <summary>
	/// Summary description for PlotAreaProperties.
	/// </summary>
	public class PlotAreaProperties :ChartProperties
	{
		/// <summary>
		/// gets and sets series source
		/// </summary>
		public string SeriesSource
		{
			get 
			{ 
				XmlNode xn = this.Node .SelectSingleNode("@chart:series-source",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node .SelectSingleNode("@chart:series-source",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("series-source", value, "chart");
				this.Node .SelectSingleNode("@chart:series-source",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets regression type
		/// </summary>

		public string RegressionType
		{
			get 
			{ 
				XmlNode xn = this.Node .SelectSingleNode("@chart:regression-type",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node .SelectSingleNode("@chart:regression-type",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("regression-type", value, "chart");
				this.Node .SelectSingleNode("@chart:regression-type",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// the constructor of plotarea property
		/// </summary>
		/// <param name="style"></param>

		public PlotAreaProperties(IStyle style):base(style)
		{


		}
	}
}

