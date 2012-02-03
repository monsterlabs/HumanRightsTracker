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
using System.IO;
using System.Reflection;
using System.Xml;

using AODL.Document.Content.Draw;
using AODL.Document.Content.EmbedObjects;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles;

namespace AODL.Document.Content.Charts
{

	
	/// <summary>
	/// chart respresents a chart object which embed into the spresdsheet document.
	/// It is inherited from the EmbedObject
	/// </summary>
	public class Chart : EmbedObject
	{
		#region  chart content
		private ChartTitle _charttitle;
		
		/// <summary>
		/// Gets or Sets the chart Title
		/// 
		/// <value>The chart Title.</value>
		/// </summary>
		
		public ChartTitle ChartTitle
		{
			get
			{
				return this._charttitle ;
			}

			set
			{
				this._charttitle =value;
			}
			
		}

		private ChartLegend _chartlegend;

		/// <summary>
		/// Gets or Sets the chart Legend
		/// 
		/// <value>The chart Legend.</value>
		/// </summary>
		public ChartLegend ChartLegend
		{
			get
			{
				return this._chartlegend ;
			}

			set
			{
				this._chartlegend=value;
			}
		}

		private ChartPlotArea _chartplotarea;

		/// <summary>
		/// Gets or Sets the chart PlotArea
		/// 
		/// <value>The chart PlotArea.</value>
		/// </summary>


		public ChartPlotArea ChartPlotArea
		{
			get
			{
				return this._chartplotarea ;
			}

			set
			{
				this._chartplotarea =value;
			}
		}

		/// <summary>
		/// Gets or sets the chart style.
		/// </summary>
		/// <value>The chart style.</value>


		public ChartStyle ChartStyle
		{
			get { return (ChartStyle)this.Style; }
			set { this.Style = value; }
		}

		private StyleCollection _styles;
		/// <summary>
		/// Collection of local styles used with the chart.
		/// </summary>
		/// <value></value>
		public StyleCollection Styles
		{
			get { return this._styles; }
			set { this._styles = value; }
		}

		/// <summary>
		/// styles from the styles.xml file
		/// </summary>
		private ChartStyles _chartstyles;

		public ChartStyles  ChartStyles
		{
			get
			{
				return this._chartstyles ;
			}

			set
			{
				this._chartstyles =value;
			}
		}

		private bool _isNewed;
		/// <summary>
		/// If this file was loaded
		/// </summary>
		/// <value></value>
		public bool IsNewed
		{
			get { return this._isNewed; }

			set {this._isNewed=value;}
		}

		#endregion

		#region chart propertity

		/// <summary>
		/// Gets or sets the width of the chart
		/// </summary>
		/// <value>The name of the width of the chart</value>
		
		public string SvgWidth
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:width",
				                                         this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:width",
				                                         this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("width", value, "svg");
				this._node.SelectSingleNode("@svg:width",
				                            this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the chart
		/// </summary>
		/// <value>The name of the height of the chart</value>
		public string SvgHeight
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:height",
				                                         this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:height",
				                                         this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("height", value, "svg");
				this._node.SelectSingleNode("@svg:height",
				                            this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the chart
		/// </summary>
		/// <value>The name of the type of the chart</value>
		
		public string ChartType
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
				                            this.Document.NamespaceManager).InnerText = "chart:"+value;
			}
		}

		/// <summary>
		/// Gets or sets the cellrange of the data table
		/// It sets or gets from the parentNode
		/// </summary>
		/// <value>the cellrange of the data table</value>

		public string CellRange
		{
			get
			{
				XmlNode xn = this.ParentNode.SelectSingleNode("@draw:notify-on-update-of-ranges",
				                                              this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.ParentNode.SelectSingleNode("@draw:notify-on-update-of-ranges",
				                                              this.Document.NamespaceManager);
				if (xn == null)
					base.CreateAttribute("notify-on-update-of-ranges", value, "draw");
				this.ParentNode.SelectSingleNode("@draw:notify-on-update-of-ranges",
				                                 this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// sets the title of the xaxis
		/// </summary>
		/// <value>the title of the xaxis</value>
		public string XAxisName
		{
			get
				
			{
				foreach(ChartAxis axis in this.ChartPlotArea .AxisCollection )
				{
					if (axis.Dimension =="x")

					{
						foreach(IContent iContent in axis.Content )
						{
							if (iContent is ChartTitle )
							{
								Paragraph  para= (Paragraph)((ChartTitle)iContent).Content [0];
								return para.TextContent [0].Node .InnerText ;
							}
						}
					}

					else
						return null;
				}

				return null;
			}
			set
			{
				ChartAxis  Xaxis=null;
				bool flag=false;
				foreach(ChartAxis axis in this.ChartPlotArea .AxisCollection)
				{
					if (axis.Dimension =="x")
						Xaxis=axis;
				}
				if (Xaxis!=null)
				{
					
					this.Styles .Remove (Xaxis.Style);
					this.ChartPlotArea.AxisCollection .Remove (Xaxis);
					flag=true;
				}

				
				Xaxis = new ChartAxis (this,"ch5");
				Xaxis.Dimension ="x";
				Xaxis.AxisName  ="primary-x";
				Xaxis.AxesStyle .AxesProperties .DisplayLabel ="true";
				Xaxis.IsModified =flag;
				ChartTitle  axisTitle= new ChartTitle (this);
				Paragraph   para = new Paragraph (this.Document );
				para.TextContent .Add (new SimpleText (this.Document ,value));
				axisTitle.Content .Add (para);
				Xaxis.Content .Add (axisTitle);
				this.ChartPlotArea .AxisCollection .Add (Xaxis);
			}
		}

		/// <summary>
		/// sets the title of the yaxis
		/// </summary>
		/// <value>the title of the yaxis</value>
		public string YAxisName
		{
			get
				
			{
				foreach(ChartAxis axis in this.ChartPlotArea .AxisCollection )
				{
					if (axis.Dimension =="y")

					{
						foreach(IContent iContent in axis.Content )
						{
							if (iContent is ChartTitle )
							{
								Paragraph  para= (Paragraph)((ChartTitle)iContent).Content [0];
								return para.TextContent [0].Node .InnerText ;
							}

						}
					}

					else
						return null;
				}

				return null;
			}
			set
				
			{
				ChartAxis  Yaxis=null;

				bool flag=false;

				foreach(ChartAxis axis in this.ChartPlotArea .AxisCollection )
				{
					if (axis.Dimension =="y")
						Yaxis=axis;
					
				}

				if (Yaxis!=null)
				{
					this.Styles .Remove (Yaxis.Style);
					this.ChartPlotArea .AxisCollection .Remove (Yaxis);
					flag=true;
				}


				Yaxis = new ChartAxis (this,"ch6");
				Yaxis.Dimension ="y";
				Yaxis.AxisName  ="primary-y";
				
				Yaxis.IsModified =flag;
				Yaxis.AxesStyle .AxesProperties .DisplayLabel ="true";
				ChartTitle  axisTitle= new ChartTitle (this);
				Paragraph   para = new Paragraph (this.Document );
				para.TextContent .Add (new SimpleText (this.Document ,value));
				axisTitle.Content .Add (para);
				Yaxis.Content .Add (axisTitle);
				this.ChartPlotArea .AxisCollection .Add (Yaxis);

			}
		}


		/// <summary>
		/// gets and sets the end cell address of the chart
		/// It sets and gets from the frame node which contains the embed object
		/// <value> the end cell range of the frame</value>
		/// </summary>

		public string EndCellAddress
		{
			get
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@table:end-cell-address",
				                                                this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@table:end-cell-address",
				                                                this.Document.NamespaceManager);
				if (xn == null)
					this.Frame .CreatePubAttr("end-cell-address", value, "table");
				this.Frame.Node .SelectSingleNode("@table:end-cell-address",
				                                  this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the end x of  the frame which chart in
		/// It sets and gets from the frame node which contains the embed object
		/// <value> the end x of the frame</value>
		/// </summary>

		public string EndX
		{
			get
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@table:end-x",
				                                                this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@table:end-x",
				                                                this.Document.NamespaceManager);
				if (xn == null)
					this.Frame .CreatePubAttr("end-x", value, "table");
				this.Frame .Node .SelectSingleNode("@table:end-x",
				                                   this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the end y of  the frame which chart in
		/// It sets and gets from the frame node which contains the embed object
		/// <value> the end y of the frame</value>
		/// </summary>
		public string EndY
		{
			get
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@table:end-y",
				                                                this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@table:end-y",
				                                                this.Document.NamespaceManager);
				if (xn == null)
					this.Frame.CreatePubAttr("end-y", value, "table");
				this.Frame .Node .SelectSingleNode("@table:end-y",
				                                   this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the horizontal position where
		/// the chart should be
		/// anchored.
		/// </summary>
		/// <example>myFrame.SvgX = "1.5cm"</example>
		/// <value>The SVG X.</value>
		public string SvgX
		{
			get
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@svg:x",
				                                                this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@svg:x",
				                                                this.Document.NamespaceManager);
				if (xn == null)
					this.Frame.CreatePubAttr("x", value, "svg");
				this.Frame .Node .SelectSingleNode("@svg:x",
				                                   this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the vertical position where
		/// the chart should be
		/// anchored.
		/// </summary>
		/// <example>myFrame.SvgY = "1.5cm"</example>
		/// <value>The SVG Y.</value>
		public string SvgY
		{
			get
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@svg:y",
				                                                this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this.Frame .Node .SelectSingleNode("@svg:y",
				                                                this.Document.NamespaceManager);
				if (xn == null)
					this.Frame.CreatePubAttr("y", value, "svg");
				this.Frame .Node .SelectSingleNode("@svg:y",
				                                   this.Document.NamespaceManager).InnerText = value;
			}
		}


		#endregion
		
		
		public  CellRanges     TableData;

		//public  CellRanges     DisplayArea;
		
		private XmlDocument _chartdoc;
		/// <summary>
		/// the xml document which save the content of the chart
		/// </summary>

		public XmlDocument ChartDoc
		{
			get
			{
				return this._chartdoc ;
			}

			set
			{
				this._chartdoc =value;
			}
		}



		#region chart constructor

		public Chart(IDocument document,XmlNode node,XmlNode ParentNode):base(ParentNode,document)
		{
			this.Node                = node;
			this.Styles              = new StyleCollection ();
			this.ChartStyles         = new ChartStyles (this);
			this.Document .EmbedObjects .Add (this);
			this.Document .DocumentMetadata .ObjectCount +=1;
			this.InitStandards ();
		}

		public Chart(Table table,string StyleName):base(table.Document)
		{
			this.TableData .table   = table;
			this.Document           = table.Document ;
			this.Styles             = new StyleCollection ();
			this.ChartStyles        = new ChartStyles (this);
			this.ChartDoc           = new XmlDocument ();
			this.NewXmlNode (StyleName);
			this.ObjectType         = "chart";
			this.Document .DocumentMetadata .ObjectCount +=1;
			this.ObjectName ="Object "+this.Document .DocumentMetadata .ObjectCount .ToString ();
			this.New ();
			this.Document .EmbedObjects .Add (this);
		}

		#endregion

		#region create the new chart

		public void New()
		{
			this.IsNewed =true;
			this.LoadBlankContent ();
			this.LoadBlankStyles ();
			this.InitStandards ();
			this.ChartLegend     = new ChartLegend(this,"ch2");
			this.ChartTitle      = new ChartTitle (this,"ch3");
			this.ChartTitle .InitTitle ();
			this.Content .Add (this.ChartTitle );
			this.ChartPlotArea   = new ChartPlotArea (this,"ch4");
			if (this.Frame==null)
			{
				this.Frame           = new Frame (this.Document ,"gr1");
				//this.CreateParentNode (null);
				this.Frame.Content .Add (this);
			}
			this.InitChart ();
		}

		/// <summary>
		/// Load the blank content
		/// </summary>


		private void LoadBlankContent()
		{
			Assembly ass		= Assembly.GetExecutingAssembly();
			Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.chartcontent.xml");
			this.ChartDoc .Load (str);
		}

		/// <summary>
		/// Load the blank style
		/// </summary>

		private void LoadBlankStyles()
		{
			Assembly ass		= Assembly.GetExecutingAssembly();
			Stream str			= ass.GetManifestResourceStream("AODL.Resources.OD.chartstyle.xml");
			this.ChartStyles .Styles.Load(str);

		}

		#endregion


		private void NewXmlNode(string styleName)
		{
			this.Node		= this.Document.CreateNode("chart", "chart");

			if (styleName!=null)
			{

				XmlAttribute xa = this.Document.CreateAttribute("style-name", "chart");
				xa.Value		= styleName;
				this.Node.Attributes.Append(xa);
				this.ChartStyle = new ChartStyle (this.Document ,styleName);
				this.Styles .Add (this.ChartStyle);
			}
		}


		private void InitStandards()
		{
			this.Content			= new ContentCollection();
			//this.Content.Inserted	+=new CollectionWithEvents.CollectionChange(Content_Inserted);
			//this.Content.Removed	+=new CollectionWithEvents.CollectionChange(Content_Removed);
		}

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
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public override string StyleName
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

		/// <summary>
		/// create the parentnode of the chart
		/// </summary>
		/// <param name="objectlink"></param>
		public XmlNode CreateParentNode(string objectlink)
		{
			XmlAttribute xa = this.Document.CreateAttribute("href", "xlink");
			xa.Value		= objectlink;
			this.ParentNode .Attributes .Append (xa);
			return this.ParentNode ;
		}

		[Obsolete("Use chart builder instead")]
		public void CreateTableFromCellRange()
		{
			
		}
		/// <summary>
		/// create the chart from the cell range of the data table
		/// </summary>
		/// <param name="CellRange"></param>
		public void CreateFromCellRange(string CellRange)
		{
			int index1               = CellRange.IndexOf (":",0);
			int length               = CellRange.Length ;
			string  startCell        = CellRange.Substring (0,index1);
			int index2               = index1+1;
			string  endCell          = CellRange.Substring(index2,length-index2);
			string  tableName        = TableData.table .TableName ;
			string  range            = tableName+"."+startCell+":"+tableName+"."+endCell;
			
			this.CellRange           = range;
			CellPos    StartCell     = GetCellPos(startCell,TableData.table );


			TableData.startcell      = StartCell;

			
			CellPos   EndCell        = GetCellPos(endCell,TableData.table );
			
			
			TableData.endcell        = EndCell;
			

			this.CellRange           = range;

			if (this.IsNewed)
				this.CreateSeries ();
		}

		/// <summary>
		/// analyse the xml string which
		/// contains the cell range of the data table
		/// </summary>
		/// <returns>CellRange</returns>
		public CellRanges GetCellRangeFromXMLString()
		{
			CellRanges chartData = new CellRanges ();

			string cellrange   = this.CellRange ;

			#region
			
			if (cellrange!=null&&cellrange!="")
			{
				int index1         = cellrange.IndexOf (".",0);

				if (index1!=-1)
				{
					
					string tableName        = cellrange.Substring (0,index1);
					Table  dataTable        = GetTable(tableName);

					if (dataTable!=null)
						chartData.table     =dataTable;

					int index2              = cellrange.IndexOf (":",0);
					string startCell        = cellrange.Substring ((index1+1),index2-index1-1);
					CellPos   StartCell        = GetCellPos(startCell,dataTable);

					
					chartData.startcell =StartCell;

					int index3              = cellrange.IndexOf (".",(index1+1));
					int length              = cellrange.Length ;
					string endCell          = cellrange.Substring ((index3+1),length-index3-1);
					CellPos   EndCell          = GetCellPos(endCell,dataTable);

					
					chartData.endcell   = EndCell;
					TableData           = chartData;
				}

				
			}
			#endregion
			return chartData;
		}

		/*
		public CellRanges  GetDisplayCellRangeFromXMLString()
		{
			try
			{
				CellRanges chartDisplay = new CellRanges ();
				string endcellAddress      = this.EndCellAddress ;
				

				if (endcellAddress!=null&&endcellAddress!="")
				{
			    
					int index1             = endcellAddress.IndexOf (".",0);
					int length             = endcellAddress.Length ;
					string tableName       = endcellAddress.Substring (0,index1);
					Table tableDisplay     = GetTable(tableName);
					if (tableDisplay!=null)
						chartDisplay.table = tableDisplay;
					string endCell         = endcellAddress.Substring (index1,length-index1);
					CellPos   EndCell         = GetCellPos(tableName,tableDisplay);
					
					chartDisplay.endcell =EndCell;
				}

				return chartDisplay;
			}
			catch(Exception ex)
			{
				throw;

			}
		}
		 
		 */


		/// <summary>
		/// gets the table according to the name of the table
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public Table GetTable(string tableName)
		{
			IDocument doc = this.Document ;
			
			if (doc is SpreadsheetDocument )
			{
				foreach(Table table in ((SpreadsheetDocument)doc).TableCollection )
				{
					if (table.TableName ==tableName)
						return table;
				}
			}

			return  null;
		}

		/// <summary>
		/// gets the rowIndex and colIndex of the chart according to the name of the cell
		/// </summary>
		/// <param name="cellName"></param>
		/// <param name="tableData"></param>
		/// <returns></returns>


		public CellPos  GetCellPos(string cellName,Table tableData)
		{
			int  i = 0;
			int  columnIndex = 0;
			int  rowIndex    = 0;
			char[] columnStr = new char [2];
			string rowStr    = null;
			
			foreach(char c in cellName)
			{
				if (c>='A'&&c<='Z')
				{
					columnStr[i]=c;
					i++;
				}
			}

			if (i==1)
				columnIndex = columnStr[0]-'A';
			if (i==2)
				columnIndex=(columnStr[0]-'A')*26 +(columnStr[1]-'A');
			columnIndex    = columnIndex+1;

			rowStr    = cellName.Substring (i);
			rowIndex  = Int32.Parse (rowStr);

			Cell cell = null;

			if (rowIndex<=tableData.Rows .Count )
			{
				Row row = tableData.Rows [rowIndex-1];
				if (columnIndex<=row.Cells.Count )
				{
					cell= tableData.Rows [rowIndex-1].Cells [columnIndex-1];
				}
			}

			if (cell==null)
			{
				cell = new Cell(_document);
				tableData.InsertCellAt (rowIndex,columnIndex,cell);
			}

			CellPos    pos = new CellPos ();

			pos.cell =cell;
			pos.columnIndex = columnIndex;
			pos.rowIndex    = rowIndex;
			return pos;
		}

		/// <summary>
		/// Init the chart
		/// </summary>

		private void  InitChart()
		{
			this.EndX       ="0.096cm";
			this.EndY       ="0.45cm";
			this.SvgWidth   ="8cm";
			this.SvgHeight  ="7cm";
			this.SvgX       ="1.128cm";
			this.SvgY       ="0.225cm";

		}

		/// <summary>
		/// Creates the content body
		/// </summary>

		private void CreateContentBody()
		{
			XmlNode nodeRoot		= this.ChartDoc .SelectSingleNode(
				"/office:document-content/office:body/office:chart", this.Document .NamespaceManager );

			
			nodeRoot.RemoveAll ();
			XmlNode nodeChart =this.ChartDoc .ImportNode (this.Node,true );
			nodeRoot.AppendChild (nodeChart);
			
			
			foreach(IContent iContent in this.Content)
			{
				
				//if (iContent is Chart)
				//nodeChart.AppendChild(((Chart)iContent).Node );
				if (iContent is ChartLegend )
				{
					XmlNode  nodeLegend = this.ChartDoc .ImportNode (((ChartLegend)iContent).Node ,true);
					nodeChart.AppendChild (nodeLegend);
				}
				
				if (iContent is ChartTitle )
				{
					XmlNode  nodeTitle = this.ChartDoc .ImportNode (((ChartTitle)iContent).Node ,true);
					nodeChart.AppendChild (nodeTitle);
				}
				
				if (iContent is ChartPlotArea )
				{
					XmlNode  nodePlotArea = this.ChartDoc .ImportNode (((ChartPlotArea)iContent).BuildNode () ,true);
					nodeChart.AppendChild (nodePlotArea);
				}
				
				if (iContent is Table )
				{
					XmlNode  nodeTable = this.ChartDoc .ImportNode (((Table)iContent).BuildNode() ,true);
					nodeChart.AppendChild (nodeTable);
				}
				
			}

			this.CreateLocalStyleContent();
			//this.CreateCommonStyleContent();
		}

		

		
		/// <summary>
		/// add  data table to the content of the chart
		/// </summary>

		private void AdddDataTableToChart()
		{
			GetCellRangeFromXMLString();
			Table table = new ChartBuilderHelper(_document, ChartPlotArea, TableData)
				.CreateTableFromCellRange();
			//this.CreateTableFromCellRange ();
			this.Content .Add (table);
		}

		/// <summary>
		/// save the content of the chart
		/// </summary>
		/// <param name="dir"></param>

		public void SaveTo(string dir)
		{
			AdddDataTableToChart();
			CreateContentBody();
			ChartExporter export = new ChartExporter ();
			export.Export (this.Document,dir);
		}

		/// <summary>
		/// Creates the content of the local style.
		/// </summary>

		private void CreateLocalStyleContent()
		{
			XmlNode nodeAutomaticStyles		= this.ChartDoc.SelectSingleNode(
				"/office:document-content/office:automatic-styles", this.Document .NamespaceManager);

			nodeAutomaticStyles.RemoveAll ();
			foreach (IStyle style in this.Styles.ToValueList())
			{
				XmlNode node =this.ChartDoc .ImportNode (style.Node ,true);
				nodeAutomaticStyles.AppendChild(node);
			}
		}

		/// <summary>
		///  Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name</param>
		/// <param name="text">The attribute value</param>
		/// <param name="prefix">The namespace prefix</param>
		private new void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// create the series of the chart
		/// </summary>

		private void CreateSeries()
		{
			
			int rowStart=this.TableData .startcell .rowIndex ;
			int rowEnd  = this.TableData .endcell .rowIndex ;
			int colStart= this.TableData .startcell .columnIndex ;
			int colEnd  = this.TableData .endcell .columnIndex ;

			if (this.ChartPlotArea .PlotAreaStyle .PlotAreaProperties .SeriesSource =="rows")
			{
				CreateSingleSeries(rowStart,rowEnd,colStart,colEnd);
			}

			if (this.ChartPlotArea .PlotAreaStyle .PlotAreaProperties .SeriesSource =="columns")
			{
				CreateSingleSeries(colStart,colEnd,rowStart,rowEnd);
			}
		}

		/// <summary>
		/// create a single series
		/// </summary>
		/// <param name="rowStartIndex">the start row index</param>
		/// <param name="rowEndINdex">the end row index</param>
		/// <param name="colStartIndex">the start col index</param>
		/// <param name="colEndIndex">the end col index</param>
		private void CreateSingleSeries(int rowStartIndex,int rowEndINdex,int colStartIndex,int colEndIndex)
		{
			for(int i=rowStartIndex; i<= rowEndINdex; i++)
			{
				string styleRowname= "ch1"+i.ToString ();
				ChartSeries series = new ChartSeries (this,styleRowname);
				
				if (this.ChartType =="Circle")
				{
					for(int j=colStartIndex; j<=colEndIndex; j++)
					{
						string styleColname = "ch2"+j.ToString ();
						ChartDataPoint dataPoint = new ChartDataPoint (this,styleColname);
						series.DataPointCollection .Add (dataPoint);
					}
				}
				else
				{
					string  pointStylename = "ch3"+i.ToString ();
					ChartDataPoint dataPoint= new ChartDataPoint (this,pointStylename);
					int Repeated = colEndIndex-colStartIndex+1;
					dataPoint.Repeated =Repeated.ToString ();
					series.DataPointCollection .Add (dataPoint);
				}

				this.ChartPlotArea .SeriesCollection .Add (series);
			}


		}

	}

}
