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
using System.Xml;

namespace AODL.Document .Styles .Properties
{
	/// <summary>
	/// Summary description for ChartProperties.
	/// </summary>
	public class ChartProperties : IProperty
	{

		/// <summary>
		/// gets and sets the direction
		/// </summary>
		public string Direction
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:direction",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:direction",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("direction", value, "style");
				this._node.SelectSingleNode("@style:direction",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        
		/// <summary>
		/// gets and sets the three dimensional
		/// </summary>
		public string ThreeDimensional
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:three-dimensional",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:three-dimensional",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("three-dimensional", value, "chart");
				this._node.SelectSingleNode("@chart:three-dimensional",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets candle stick
		/// </summary>

		public string CandleStick
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:japanese-candle-stick",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:japanese-candle-stick",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("japanese-candle-stick", value, "chart");
				this._node.SelectSingleNode("@chart:japanese-candle-stick",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the data label number
		/// </summary>

		public string DataLabelNumber
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:data-label-number",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:data-label-number",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("data-label-number", value, "chart");
				this._node.SelectSingleNode("@chart:data-label-number",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets data label text
		/// </summary>

		public string DataLabelText
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:data-label-text",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:data-label-text",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("data-label-text", value, "chart");
				this._node.SelectSingleNode("@chart:data-label-text",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets data label symbol
		/// </summary>

		public string DataLabelSymbol
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:data-label-symbol",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:data-label-symbol",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("data-label-symbol", value, "chart");
				this._node.SelectSingleNode("@chart:data-label-symbol",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets mean value
		/// </summary>

		public string MeanValue
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:mean-value",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:mean-value",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("mean-value", value, "chart");
				this._node.SelectSingleNode("@chart:mean-value",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets error category
		/// </summary>

		public string ErrorCategory
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-category",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-category",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-category", value, "chart");
				this._node.SelectSingleNode("@chart:error-category",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
		

		/// <summary>
		/// gets and sets error percentage
		/// </summary>
		public string ErrorPercentage
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-percentage",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-percentage",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-percentage", value, "chart");
				this._node.SelectSingleNode("@chart:error-percentage",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets error margin
		/// </summary>

		public string ErrorMargin
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-margin",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-margin",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-margin", value, "chart");
				this._node.SelectSingleNode("@chart:error-margin",
					this.Style.Document.NamespaceManager).InnerText = value;
			 }
		}

		/// <summary>
		/// gets and sets error lower limit
		/// </summary>

		
		public string ErrorLowerLimit
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-lower-limit",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-lower-limit",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-lower-limit", value, "chart");
				this._node.SelectSingleNode("@chart:error-lower-limit",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets error upper limit
		/// </summary>

		
		public string ErrorUpperLimit
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-upper-limit",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-upper-limit",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-upper-limit", value, "chart");
				this._node.SelectSingleNode("@chart:error-upper-limit",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets error upper indicator
		/// </summary>

		
		public string ErrorUpperIndicator
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-upper-indicator",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-upper-indicator",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-upper-indicator", value, "chart");
				this._node.SelectSingleNode("@chart:error-upper-indicator",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// gets and sets error lower indicator
		/// </summary>

		public string ErrorLowerIndicator
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:error-lower-indicator",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:error-lower-indicator",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("error-lower-indicator", value, "chart");
				this._node.SelectSingleNode("@chart:error-lower-indicator",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets vertical
		/// </summary>

		public string Vertical
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:vertical",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:vertical",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("vertical", value, "chart");
				this._node.SelectSingleNode("@chart:vertical",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets connect bars
		/// </summary>

		public string ConnectBars
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:connect-bars",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:connect-bars",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("connect-bars", value, "chart");
				this._node.SelectSingleNode("@chart:connect-bars",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets gap width
		/// </summary>

		public string GapWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:gap-width",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:gap-width",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("gap-width", value, "chart");
				this._node.SelectSingleNode("@chart:gap-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}


		}

		/// <summary>
		/// gets and sets over lap
		/// </summary>

		public string OverLap
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:overlap",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:overlap",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("overlap", value, "chart");
				this._node.SelectSingleNode("@chart:overlap",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		
		/// <summary>
		/// gets and sets deep
		/// </summary>
		public string Deep
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:deep",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:deep",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("deep", value, "chart");
				this._node.SelectSingleNode("@chart:deep",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets symbol type
		/// </summary>

		public string SymbolType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:symbol-type",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:symbol-type",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("symbol-type", value, "chart");
				this._node.SelectSingleNode("@chart:symbol-type",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		
		/*	public string SymbolType
			{
				get 
				{ 
					XmlNode xn = this._node.SelectSingleNode("@chart:symbol-type",
						this.Style.Document.NamespaceManager);
					if (xn != null)
						return xn.InnerText;
					return null;
				}
				set
				{
					XmlNode xn = this._node.SelectSingleNode("@chart:symbol-type",
						this.Style.Document.NamespaceManager);
					if (xn == null)
						this.CreateAttribute("symbol-type", value, "chart");
					this._node.SelectSingleNode("@chart:symbol-type",
						this.Style.Document.NamespaceManager).InnerText = value;
				}
			}*/

		/// <summary>
		/// gets and sets lines
		/// </summary>
            
		public string Lines
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:lines",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:lines",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("lines", value, "chart");
				this._node.SelectSingleNode("@chart:lines",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// gets and sets solid type
		/// </summary>

		public string SolidType
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:solid-type",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:solid-type",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("solid-type", value, "chart");
				this._node.SelectSingleNode("@chart:solid-type",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets stacked
		/// </summary>

		public string Stacked
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:stacked",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:stacked",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stacked", value, "chart");
				this._node.SelectSingleNode("@chart:stacked",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// gets and sets percentage
		/// </summary>

		public string Percentage
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:percentage",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:percentaged",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("percentage", value, "chart");
				this._node.SelectSingleNode("@chart:percentage",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets inter polation
		/// </summary>

		public string InterPolation
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:interpolation",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:interpolation",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("interpolation", value, "chart");
				this._node.SelectSingleNode("@chart:interpolation",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}






		protected void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Style.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}


		
		/// <summary>
		/// The Constructor, create new instance of ChartProperties
		/// </summary>
		/// <param name="style">The ColumnStyle</param>
		public ChartProperties(IStyle style)
		{
			this.Style			= style;
			this.NewXmlNode();
		}
		
		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		public void NewXmlNode()
		{
			this.Node		= this.Style.Document.CreateNode("chart-properties", "style");
		}


		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value>The node</value>
		public System.Xml.XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}
		#endregion
	}
}

