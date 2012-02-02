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
	/// Summary description for AxesProperties.
	/// </summary>
	public class AxesProperties : ChartProperties
	{
		/// <summary>
		/// gets and sets the link data format
		/// </summary>

		public string LinkDataFormat
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:link-data-style-to-source",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:link-data-style-to-source",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("link-data-style-to-source", value, "chart");
				this.Node .SelectSingleNode("@chart:link-data-style-to-source",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the visibility
		/// </summary>

		public string Visibility
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:axis-visible",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:axis-visible",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("axis-visible", value, "chart");
				this.Node.SelectSingleNode("@chart:axis-visible",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        
		/// <summary>
		/// gets and sets the logarithmic
		/// </summary>
		public string Logarithmic
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:axis-logarithmic",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:axis-logarithmic",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("axis-logarithmic", value, "chart");
				this.Node.SelectSingleNode("@chart:axis-logarithmic",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets tick mark major inner
		/// </summary>

		public string TickMarkMajorInner
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-major-inner",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-major-inner",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("tick-marks-major-inner", value, "chart");
				this.Node.SelectSingleNode("@chart:tick-marks-major-inner",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets tick mark major outer
		/// </summary>

		public string TickMarkMajorOuter
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-minor-outer",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-major-inner",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("tick-marks-minor-outer", value, "chart");
				this.Node.SelectSingleNode("@chart:tick-marks-minor-outer",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        
		/// <summary>
		/// gets and set the maximum
		/// </summary>
		public string Maximum
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:maximum",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:maximum",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("maximum", value, "chart");
				this.Node.SelectSingleNode("@chart:maximum",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets minimum
		/// </summary>

		public string Minimum
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:minimum",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:minimum",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("minimum", value, "chart");
				this.Node.SelectSingleNode("@chart:minimum",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and set origin
		/// </summary>

		public string Origin
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:origin",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:origin",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("origin", value, "chart");
				this.Node.SelectSingleNode("@chart:origin",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the interval major
		/// </summary>

		public string IntervalMajor
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:interval-major",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:interval-major",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("interval-major", value, "chart");
				this.Node.SelectSingleNode("@chart:interval-major",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets interval minor
		/// </summary>

		public string IntervalMinor
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:interval-minor",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:interval-minor",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("interval-minor", value, "chart");
				this.Node.SelectSingleNode("@chart:interval-minor",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets tick mark minor inner
		/// </summary>

		public string TickMarkMinorInner
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-minor-inner",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-minor-inner",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("tick-marks-minor-inner", value, "chart");
				this.Node.SelectSingleNode("@chart:tick-marks-minor-inner",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets tick mark minor outer
		/// </summary>

		public string TickMarkMinorOuter
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-minor-outer",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:tick-marks-minor-outer",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("tick-marks-minor-outer", value, "chart");
				this.Node.SelectSingleNode("@chart:tick-marks-minor-outer",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the display label
		/// </summary>

		public string DisplayLabel
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:display-label",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:display-label",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("display-label", value, "chart");
				this.Node.SelectSingleNode("@chart:display-label",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets text overlap
		/// </summary>

		public string TextOverLap
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:text-overlap",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:text-overlap",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("text-overlap", value, "chart");
				this.Node.SelectSingleNode("@chart:text-overlap",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets line break
		/// </summary>

		public string LineBreak
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@text:line-break",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@text:line-break",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("line-break", value, "text");
				this.Node.SelectSingleNode("@text:line-break",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the label arragement
		/// </summary>

		public string LabelArrangement
		{
			get 
			{ 
				XmlNode xn = this.Node.SelectSingleNode("@chart:label-arrangement",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Node.SelectSingleNode("@chart:label-arrangement",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("label-arrangement", value, "chart");
				this.Node.SelectSingleNode("@chart:label-arrangement",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}



		private new void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Style.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}


		
		/// <summary>
		/// The Constructor, create new instance of ChartProperties
		/// </summary>
		/// <param name="style">The ColumnStyle</param>
		public AxesProperties(IStyle style): base(style)
		{
			//this.Style			= style;
			//this.NewXmlNode();
		}

		
		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		private new void NewXmlNode()
		{
			this.Node		= this.Style.Document.CreateNode("chart-properties", "style");
		}

		#region IProperty Member
	
		#endregion

 




	}
}

