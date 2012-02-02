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
using System.Diagnostics ;
using AODL.Document .Exceptions ;
using AODL.Document .Styles ;
using AODL.Document .Content .Text ;
using System.Collections ;
using AODL.Document .Import .OpenDocument.NodeProcessors ;

namespace AODL.Document.Content.Charts
{
	/// <summary>
	/// Summary description for ChartImporter.
	/// </summary>
	public class ChartImporter
	{

		public delegate void Warning(AODLWarning warning);

		public event Warning OnWarning;
		
		/// <summary>
		/// the chart which  is imported
		/// </summary>
		private Chart _chart;
		
		public Chart Chart
		{
			get
			{
				return this._chart ;
			}

			set
			{
				this._chart  = value;
			}
		}

		/// <summary>
		/// the constructor of the ChartImporter
		/// </summary>
		/// <param name="chart"></param>

		public ChartImporter(Chart chart)
		{
			this.Chart =chart;
			
		}

		/// <summary>
		/// import the content and style of the chart
		/// </summary>

		public void Import()
		{
			this.ReadContent ();
			ImportChartStyles();
		}

		/// <summary>
		/// import the style of the chart
		/// </summary>
		public void ImportChartStyles()
		{
			this.Chart .ChartStyles .Styles = new XmlDocument ();
			this.Chart .ChartStyles .Styles .Load (this.Chart .ObjectRealPath +@"\"+"\\styles.xml");
		}

		/// <summary>
		/// read the content of the chart
		/// </summary>

		public void ReadContent()
		{
			this.Chart .ChartDoc   = new System.Xml.XmlDocument ();

			this.Chart .ChartDoc.Load(this.Chart .ObjectRealPath +@"\"+"\\content.xml");

			ReadContentNodes();
		}

		public void ReadContentNodes()
		{
			try
			{
				//				this._document.XmlDoc	= new XmlDocument();
				//				this._document.XmlDoc.Load(contentFile);

				XmlNode node				    = null;
				
				node	=  this.Chart .ChartDoc. SelectSingleNode(
					"/office:document-content/office:body/office:chart", this.Chart .Document .NamespaceManager);

				if (node != null)
				{
					this.CreateMainContent(node);
				}
				else
				{
					throw new AODLException("Unknow content type.");
				}
				//Remove all existing content will be created new
				node.RemoveAll();
			}
			catch(Exception ex)
			{
				throw new AODLException("Error while trying to load the main content", ex);
			}

		}

		/// <summary>
		/// create the main content of the chart
		/// </summary>
		/// <param name="node"></param>

		public void CreateMainContent(XmlNode node)
		{
			try
			{
				foreach(XmlNode nodeChild in node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild.CloneNode(true));

					if (iContent != null)
						AddToCollection(iContent, this.Chart .Content);
					//this._document.Content.Add(iContent);
					else
					{
						if (this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("A couldn't create any content from an an first level node!.");
							//warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= nodeChild;
							this.OnWarning(warning);
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while processing a content node.", ex);
			}
		}

		/// <summary>
		/// create the content of the chart
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private  IContent CreateContent(XmlNode node)
		{
			try
			{
				switch(node.Name )
				{
					case "chart:chart":
						return CreateChart(node.CloneNode (true));
					case"chart:title":
						return CreateChartTitle(node.CloneNode (true));
					case"chart:legend":
						return CreateChartLegend(node.CloneNode (true));
					case"chart:plot-area":
						return CreateChartPlotArea(node.CloneNode (true));
					case"chart:axis":
						return CreateChartAxes(node.CloneNode (true));
					case"chart:categories":
						return CreateChartCategories(node.CloneNode (true));
					case"chart:grid":
						return CreateChartGrid(node.CloneNode (true));
					case"chart:series":
						return CreateChartSeries(node.CloneNode (true));
						case"chart:data-point":;
						return CreateChartDataPoint(node.CloneNode (true));
					case"chart:wall":
						return CreateChartWall(node.CloneNode (true));
					case"chart:floor":
						return CreateChartFloor(node.CloneNode (true));
					case"dr3d:light":
						return CreateDr3dLight(node.CloneNode (true));
					case "text:p":
						return CreateParagraph(node.CloneNode(true));
					case"table:table":
						//return CreateDataTable();


					default:
						// Phil Jollans 19-Feb-2008; break added
						break ;
				}
				return null;

				
			}
			

			catch(Exception ex)
			{
				throw new AODLException("Exception while processing a content node.", ex);
			}
			
		}


		/// <summary>
		/// create the chart
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private IContent CreateChart(XmlNode node)
		{
			try
			{
				this.Chart .Node             = node;
				ContentCollection iColl     = new ContentCollection ();
				ChartStyleProcessor csp      = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle            = csp.ReadStyleNode(this.Chart .StyleName);
				IStyle style                 = csp.ReadStyle (nodeStyle,"chart");

				if ( style!=null )
				{
					this.Chart .ChartStyle   = (ChartStyle)style;
					this.Chart .Styles .Add (style);

				}
				
				foreach(XmlNode nodeChild in this.Chart .Node .ChildNodes )
				{
					IContent icontent       = CreateContent(nodeChild);
					if (icontent!=null)

						AddToCollection(icontent,iColl );
				}

				this.Chart.Node .InnerXml ="";
				foreach(IContent icontent in iColl)
				{
					AddToCollection(icontent,this.Chart .Content );
				}

				return this.Chart ;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart!", ex);
			}


		}
		
		/// <summary>
		/// create the chart title
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private IContent CreateChartTitle(XmlNode node)
		{
			try
			{
				ChartTitle title             = new ChartTitle (this.Chart.Document ,node);
				title.Chart                  = this.Chart ;
				this.Chart .ChartTitle       =title;
				//title.Node                   = node;
				ChartStyleProcessor csp      = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle            = csp.ReadStyleNode(title .StyleName);
				IStyle style                 = csp.ReadStyle (nodeStyle,"title");
				ContentCollection iColl     = new ContentCollection ();

				if ( style!= null)
				{
					title.Style =style;
					this.Chart .Styles .Add (style);
				}

				foreach(XmlNode nodeChild in title.Node .ChildNodes )
				{
					IContent icontent       = CreateContent(nodeChild);

					if (icontent!= null)
						AddToCollection(icontent,iColl);
				}

				title.Node.InnerXml  ="";

				foreach(IContent icontent in iColl)
				{
					AddToCollection(icontent,title.Content );
				}
				
				return title;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart title!", ex);
			}
		}

		/// <summary>
		/// create the chart legend
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartLegend(XmlNode node)
		{
			try
			{
				ChartLegend legend           = new ChartLegend (this.Chart .Document ,node);
				legend.Chart                 = this.Chart ;
				//legend.Node                  = node;
				this.Chart .ChartLegend      =legend;
				ChartStyleProcessor csp      = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle            = csp.ReadStyleNode(legend.StyleName);
				IStyle style                 = csp.ReadStyle (nodeStyle,"legend");

				if (style != null)
				{
					legend.Style             =style;
					this.Chart .Styles .Add (style);
				}

				return  legend;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart legend!", ex);
			}
		}

		/// <summary>
		/// create the chart plotarea
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartPlotArea(XmlNode node)
		{
			try
			{
				ChartPlotArea plotarea      = new ChartPlotArea (this.Chart .Document ,node);
				plotarea.Chart              = this.Chart ;
				//plotarea.Node               = node;
				this.Chart .ChartPlotArea   = plotarea;

				ChartStyleProcessor csp     = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle           = csp.ReadStyleNode(plotarea .StyleName);
				IStyle style                = csp.ReadStyle (nodeStyle,"plotarea");

				ContentCollection iColl    = new ContentCollection ();

				if (style != null)
				{
					plotarea.Style          =style;
					this.Chart .Styles .Add (style);
				}

				foreach(XmlNode nodeChild in plotarea.Node .ChildNodes )
				{
					IContent icontent       = CreateContent(nodeChild);

					if (icontent!= null)
						AddToCollection(icontent,iColl);
				}

				plotarea.Node.InnerXml  ="";

				foreach(IContent icontent in iColl)
				{
					if (icontent is Dr3dLight)
					{
						((Dr3dLight)icontent).PlotArea =plotarea;
						plotarea.Dr3dLightCollection .Add (icontent as Dr3dLight );
					}

					else if (icontent is ChartAxis)
					{
						((ChartAxis)icontent).PlotArea=plotarea;
						plotarea.AxisCollection .Add (icontent as ChartAxis );
					}

					else if (icontent is ChartSeries )
					{
						((ChartSeries)icontent).PlotArea =plotarea;
						plotarea.SeriesCollection .Add (icontent as ChartSeries );
					}

					else
					{
						AddToCollection(icontent,plotarea.Content );
					}
				}
				
				return plotarea;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart plotarea!", ex);
			}
		}

		/// <summary>
		/// create the chart axes
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartAxes(XmlNode node)
		{
			try
			{
				ChartAxis axes              = new ChartAxis (this.Chart .Document ,node);
				axes.Chart                  = this.Chart ;
				//axes.Node                   = node;
				ChartStyleProcessor csp     = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle           = csp.ReadStyleNode(axes.StyleName);
				IStyle style                = csp.ReadStyle (nodeStyle,"axes");
				ContentCollection iColl    = new ContentCollection ();

				if (style !=null)
				{
					axes.Style              = style;
					this.Chart .Styles .Add (style);
				}


				foreach(XmlNode nodeChild in axes.Node .ChildNodes )
				{
					IContent icontent       = CreateContent(nodeChild);

					if (icontent!= null)
						AddToCollection(icontent,iColl);
				}

				
				axes.Node.InnerXml  ="";

				foreach(IContent icontent in iColl)
				{
					AddToCollection(icontent,axes.Content );
				}
				
				return axes;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart axes!", ex);
			}


		}

		/// <summary>
		/// create the chart category
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartCategories(XmlNode node)
		{
			try
			{
				ChartCategories categories  = new ChartCategories (this.Chart .Document ,node);
				//categories.Node             = node;
				categories.Chart            = this.Chart ;

				ChartStyleProcessor csp     = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle           = csp.ReadStyleNode(categories.StyleName);
				IStyle style                = csp.ReadStyle (nodeStyle,"categories");

				if (style != null)
				{
					categories.Style          =style;
					this.Chart .Styles .Add (style);
				}

				return categories;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart categories!", ex);
			}


		}

		/// <summary>
		/// create the chart grid
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartGrid(XmlNode node)
		{
			try
			{
				ChartGrid grid              = new ChartGrid  (this.Chart .Document ,node);
				//grid.Node                   = node;
				grid.Chart                  = this.Chart ;

				ChartStyleProcessor csp     = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle           = csp.ReadStyleNode(grid.StyleName);
				IStyle style                = csp.ReadStyle (nodeStyle,"grid");

				if (style != null)
				{
					grid.Style              =style;
					this.Chart .Styles .Add (style);
				}

				return grid;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart grid!", ex);
			}
		}

		/// <summary>
		/// create the chart series
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartSeries(XmlNode node)
		{
			try
			{
				ChartSeries series          = new ChartSeries(this.Chart .Document ,node);
				series.Chart                = this.Chart ;

				ChartStyleProcessor csp     = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle           = csp.ReadStyleNode(series.StyleName);
				IStyle style                = csp.ReadStyle (nodeStyle,"series");
				ContentCollection iColl    = new ContentCollection ();

				if (style != null)
				{
					series.Style            =style;
					this.Chart .Styles .Add (style);
				}

				foreach(XmlNode nodeChild in series.Node .ChildNodes )
				{
					IContent icontent       = CreateContent(nodeChild);

					if (icontent!= null)
						AddToCollection(icontent,iColl);
				}

				series.Node.InnerXml  ="";

				foreach(IContent iContent in iColl)
				{
					if (iContent is ChartDataPoint )
						series.DataPointCollection .Add (iContent as ChartDataPoint);
					
				}
				
				return series;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart series!", ex);
			}
		}

		/// <summary>
		/// create the chart data point
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartDataPoint(XmlNode node)
		{
			try
			{
				ChartDataPoint datapoint              = new ChartDataPoint(this.Chart .Document ,node);
				//grid.Node                   = node;
				datapoint.Chart                       = this.Chart ;

				ChartStyleProcessor csp               = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle                     = csp.ReadStyleNode(datapoint.StyleName);
				IStyle style                          = csp.ReadStyle (nodeStyle,"datapoint");

				if (style != null)
				{
					datapoint.Style                        = style;
					this.Chart .Styles .Add (style);
				}

				return datapoint;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart datapoint!", ex);
			}
		}

		/// <summary>
		/// create the chart wall
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartWall(XmlNode node)
		{
			try
			{
				ChartWall wall                 = new ChartWall(this.Chart .Document ,node);
				//grid.Node                   = node;
				wall.Chart                     = this.Chart ;

				ChartStyleProcessor csp        = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle              = csp.ReadStyleNode(wall.StyleName);
				IStyle style                   = csp.ReadStyle (nodeStyle,"wall");

				if (style != null)
				{
					wall.Style                 =style;
					this.Chart .Styles .Add (style);
				}

				return wall;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart wall!", ex);
			}
		}

		/// <summary>
		/// create the chart floor
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateChartFloor(XmlNode node)
		{
			try
			{
				ChartFloor floor                 = new ChartFloor(this.Chart .Document ,node);
				//grid.Node                   = node;
				floor.Chart                     = this.Chart ;

				ChartStyleProcessor csp        = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle              = csp.ReadStyleNode(floor.StyleName);
				IStyle style                   = csp.ReadStyle (nodeStyle,"floor");

				if (style != null)
				{
					floor.Style                 =style;
					this.Chart .Styles .Add (style);
				}

				return floor;
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart floor!", ex);
			}

		}

		/// <summary>
		/// create the dr3d light
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>

		private IContent CreateDr3dLight(XmlNode node)
		{
			try
			{
				Dr3dLight  light               = new Dr3dLight(this.Chart .Document ,node);
				//grid.Node                    = node;
				light.Chart                    = this.Chart ;

				ChartStyleProcessor csp        = new ChartStyleProcessor (this.Chart );
				XmlNode nodeStyle              = csp.ReadStyleNode(light.StyleName);
				IStyle style                   = csp.ReadStyle (nodeStyle,"dr3d");

				if (style != null)
				{
					light.Style                =style;
					this.Chart .Styles .Add (style);
				}

				return light;


			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while creating the chart dr3dlight!", ex);
			}
		}

		public Paragraph CreateParagraph(XmlNode paragraphNode)
		{
			try
			{
				//Create a new Paragraph
				Paragraph paragraph				= new Paragraph(paragraphNode, this.Chart.Document);
				//Recieve the ParagraphStyle
				return this.ReadParagraphTextContent(paragraph);
			}

			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Paragraph.", ex);
			}
		}

		private Paragraph ReadParagraphTextContent(Paragraph paragraph)
		{
			try
			{
				ArrayList mixedContent			= new ArrayList();
				foreach(XmlNode nodeChild in paragraph.Node.ChildNodes)
				{
					//Check for IText content first
					TextContentProcessor tcp	= new TextContentProcessor();
					IText iText					= tcp.CreateTextObject(this.Chart .Document, nodeChild.CloneNode(true));
					
					if (iText != null)
						mixedContent.Add(iText);
					else
					{
						//Check against IContent
						IContent iContent		= this.CreateContent(nodeChild);
						
						if (iContent != null)
							mixedContent.Add(iContent);
					}
				}

				//Remove all
				paragraph.Node.InnerXml			= "";

				foreach(Object ob in mixedContent)
				{
					if (ob is IText)
					{
						XmlNode node=this.Chart.ChartDoc .ImportNode (((IText)ob).Node,true);
						paragraph.Node.AppendChild(node);
					}
					else if (ob is IContent)
					{
						//paragraph.Content.Add(ob as IContent);
						AddToCollection(ob as IContent, paragraph.Content);
					}
					else
					{
						if (this.OnWarning != null)
						{
							AODLWarning warning			= new AODLWarning("Couldn't determine the type of a paragraph child node!.");
							//warning.InMethod			= AODLException.GetExceptionSourceInfo(new StackFrame(1, true));
							warning.Node				= paragraph.Node;
							this.OnWarning(warning);
						}
					}
				}
				return paragraph;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create the Paragraph content.", ex);
			}
		}

		

		private void AddToCollection(IContent content, ContentCollection coll)
		{
			coll.Add(content);
		}
	}
}


