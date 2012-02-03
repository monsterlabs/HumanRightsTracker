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
using AODL.Document .Styles ;
using AODL.Document .Collections ;

namespace AODL.Document.Content.Charts
{
	/// <summary>
	/// Summary description for ChartSeries.
	/// </summary>
	public class ChartSeries :IContent
	{
		/// <summary>
		/// the chart which contains the series
		/// </summary>
		private Chart _chart;

		public Chart Chart
		{
			get { return this._chart; }
			set { this._chart = value; }
		}

		/// <summary>
		/// the plotarea which contains the series
		/// </summary>

		private ChartPlotArea _plotarea;

		public ChartPlotArea PlotArea
		{
			get { return this._plotarea; }
			set { this._plotarea = value; }
		}

		/// <summary>
		/// the style of the series
		/// </summary>

		public SeriesStyle SeriesStyle
		{
			get
			{
				return (SeriesStyle)this.Style;
			}
			set
			{
				this.StyleName	= ((SeriesStyle)value).StyleName;
				this.Style = value;
			}
		}

		/// <summary>
		/// the collection of the data point which the series contains
		/// </summary>

		private DataPointCollection _datapointcollection;

		public DataPointCollection  DataPointCollection
		{
			get
			{
				return this._datapointcollection;
			}

			set
			{
				this._datapointcollection =value;
			}
		}

		/// <summary>
		/// gets and sets the value cell range address
		/// </summary>

		public string ValuesCellRangeAddress
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:values-cell-range-address",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:values-cell-range-address",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("values-cell-range-address", value, "chart");
				this._node.SelectSingleNode("@chart:values-cell-range-address",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the label cell address
		/// </summary>

		public string LabelCellAddress
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:label-cell-address",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:label-cell-address",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("label-cell-address", value, "chart");
				this._node.SelectSingleNode("@chart:label-cell-address",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the class 
		/// </summary>

		public string Class
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:class",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:class",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("class", value, "chart");
				this._node.SelectSingleNode("@chart:class",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the attached axis
		/// </summary>

		public string AttachAxis
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:attached-axis",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:attached-axis",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("attached-axis", value, "chart");
				this._node.SelectSingleNode("@chart:attached-axis",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// the constructor of the chartseries
		/// </summary>
		/// <param name="document"></param>
		/// <param name="node"></param>

		public ChartSeries(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
			this.DataPointCollection =new DataPointCollection ();
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the ChartPlotArea class.
		/// This will create an empty cell that use the default cell style
		/// </summary>
		/// <param name="table">The table.</param>
		public ChartSeries(Chart chart)
		{
			this.Chart				= chart;
			this.Document			= chart.Document;
			this.DataPointCollection =new DataPointCollection ();	
			this.NewXmlNode(null);
			this.SeriesStyle =new SeriesStyle (this.Document);
			this.Chart .Styles .Add (SeriesStyle);
		
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the ChartPlotArea class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="styleName">The style name.</param>
		public ChartSeries(Chart chart, string styleName)
		{
			this.Chart				= chart;
			this.Document			= chart.Document;
			this.NewXmlNode(styleName);
			this.InitStandards();
			this.DataPointCollection  = new DataPointCollection ();

			if (styleName != null)
			{
				this.StyleName		= styleName;
				this.SeriesStyle		= new SeriesStyle(this.Document, styleName);
				this.Chart.Styles.Add(this.SeriesStyle);
			}
		}

		private void NewXmlNode(string styleName)
		{
			this.Node		= this.Document.CreateNode("series", "chart");

			XmlAttribute xa = this.Document.CreateAttribute("style-name", "chart");
			xa.Value		= styleName;
			this.Node.Attributes.Append(xa);
		}

		private void InitStandards()
		{
			this.Content			= new ContentCollection();
			this.Content.Inserted	+= Content_Inserted;
			this.Content.Removed	+= Content_Removed;
		}


		
		#region IContent Member
		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@chart:style-name",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@chart:style-name",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("style-name", value, "chart");
				this._node.SelectSingleNode("@chart:style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		private IDocument _document;
		/// <summary>
		/// Every object (typeof(IContent)) have to know his document.
		/// </summary>
		/// <value></value>
		public IDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName	= value.StyleName;
				this._style = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		#endregion

		#region IContentContainer Member

		private ContentCollection _content;
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public ContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		/// <summary>
		/// Content_s the removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// builds the content
		/// </summary>
		/// <returns></returns>

		public XmlNode BuildNode()
		{
			foreach(ChartDataPoint DataPoint in DataPointCollection)
			{
				//XmlNode node = this.Chart .ChartDoc .ImportNode (DataPoint.Node ,true);
				this.Node .AppendChild (DataPoint.Node );
			}
			return this.Node ;
		}
	
	}
}

