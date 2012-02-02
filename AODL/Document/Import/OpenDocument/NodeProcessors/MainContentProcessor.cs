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
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Draw;
using AODL.Document.Content.OfficeEvents;
using AODL.Document.Content.Tables;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Exceptions;
using AODL.Document.Forms;
using AODL.Document.Forms.Controls;
using AODL.Document .Content .EmbedObjects;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	public class MainContentProcessor
	{
		/// <summary>
		/// If set to true all node content would be directed
		/// to Console.Out
		/// </summary>
		private bool _debugMode			= false;
		/// <summary>
		/// The textdocument
		/// </summary>
		private IDocument _document;
		/// <summary>
		/// Warning delegate
		/// </summary>
		public delegate void WarningHandler(AODLWarning warning);
		/// <summary>
		/// Warning event fired if something unexpected
		/// occour.
		/// </summary>
		public event WarningHandler Warning;

		private void OnWarning(AODLWarning warning)
		{
			if (Warning != null) {
				Warning(warning);
			}
		}
		
		private void AddToCollection(IContent content, ContentCollection coll)
		{
			coll.Add(content);

			if (content is ODFControlRef)
			{
				ODFControlRef ctrlRef = content as ODFControlRef;
				if (this._document is TextDocument)
				{
					
					TextDocument td = this._document as TextDocument;
					ODFFormControl fc = td.FindControlById(ctrlRef.DrawControl);
					
					if (fc != null)
					{
						fc.ContentCollection = coll;
						fc.ControlRef = ctrlRef;
					}

				}
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="MainContentProcessor"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public MainContentProcessor(IDocument document)
		{
			this._document			= document;
		}

		/// <summary>
		/// Reads the content nodes.
		/// </summary>
		public void ReadContentNodes()
		{
			try
			{
				//				this._document.XmlDoc	= new XmlDocument();
				//				this._document.XmlDoc.Load(contentFile);

				XmlNode node				= null;

				if (this._document is TextDocument)
					node	= this._document.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, this._document.NamespaceManager);
				else if (this._document is SpreadsheetDocument)
					node	= this._document.XmlDoc.SelectSingleNode(
						"/office:document-content/office:body/office:spreadsheet", this._document.NamespaceManager);

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
				throw new AODLException("Error while trying to load the content file!", ex);
			}
		}

		/// <summary>
		/// Creates the content.
		/// </summary>
		/// <param name="node">The node.</param>
		public void CreateMainContent(XmlNode node)
		{
			try
			{
				foreach(XmlNode nodeChild in node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild.CloneNode(true));

					if (iContent != null)
						AddToCollection(iContent, this._document.Content);
					//this._document.Content.Add(iContent);
					else
					{
						this.OnWarning(new AODLWarning("A couldn't create any content from an an first level node!.", nodeChild));
					}
				}
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while processing a content node.", ex);
			}
		}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <param name="node">The node to clone and create content from.</param>
		/// <returns></returns>
		public IContent CreateContent(XmlNode node)
		{
			try
			{
				switch(node.Name)
				{
					case "text:p":
						return CreateParagraph(node.CloneNode(true));
					case "text:list":
						return CreateList(node.CloneNode(true));
					case "text:list-item":
						return CreateListItem(node.CloneNode(true));
					case "table:table":
						return CreateTable(node.CloneNode(true));
					case "table:table-column":
						return CreateTableColumn(node.CloneNode(true));
					case "table:table-row":
						return CreateTableRow(node.CloneNode(true));
					case "table:table-header-rows":
						return CreateTableHeaderRow(node.CloneNode(true));
					case "table:table-cell":
						return CreateTableCell(node.CloneNode(true));
					case "table:covered-table-cell":
						return CreateTableCellSpan(node.CloneNode(true));
					case "text:h":
						return CreateHeader(node.CloneNode(true));
					case "text:table-of-content":
						//Possible?
						return CreateTableOfContents(node.CloneNode(true));
					case "draw:frame":
						return CreateFrame(node.CloneNode(true));
					case "draw:object":
						return CreateEmbedObject(node.CloneNode (true));
					case "draw:text-box":
						return CreateDrawTextBox(node.CloneNode(true));
					case "draw:image":
						return CreateGraphic(node.CloneNode(true));
						//@Liu Yuhua: What's that??? This is of course a image and not unknown!! Lars
						//return new UnknownContent(this._document, node.CloneNode(true));
					case "draw:area-rectangle":
						return CreateDrawAreaRectangle(node.CloneNode(true));
					case "draw:area-circle":
						return CreateDrawAreaCircle(node.CloneNode(true));
					case "draw:image-map":
						return CreateImageMap(node.CloneNode(true));
					case "office:event-listeners":
						return CreateEventListeners(node.CloneNode(true));
					case "script:event-listener":
						return CreateEventListeners(node.CloneNode(true));

					case "draw:control":
						return CreateControlRef(node.CloneNode(true));
					default:
						return new UnknownContent(this._document, node.CloneNode(true));
				}
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while processing a content node.", ex);
			}
		}

		/// <summary>
		/// Creates the table of contents.
		/// </summary>
		/// <param name="tocNode">The toc node.</param>
		/// <returns></returns>
		private TableOfContents CreateTableOfContents(XmlNode tocNode)
		{
			try
			{
				if (this._document is TextDocument)
				{
					//Create the TableOfContents object
					TableOfContents tableOfContents		= new TableOfContents(
						((TextDocument)this._document), tocNode);
					//Recieve the Section style
					IStyle sectionStyle					= this._document.Styles.GetStyleByName(tableOfContents.StyleName);

					if (sectionStyle != null)
						tableOfContents.Style				= sectionStyle;
					else
					{
						OnWarning(new AODLWarning("A SectionStyle for the TableOfContents object wasn't found.", tocNode));
					}
					
					//Create the text entries
					XmlNodeList paragraphNodeList	= tocNode.SelectNodes(
						"text:index-body/text:p", this._document.NamespaceManager);
					XmlNode indexBodyNode			= tocNode.SelectSingleNode("text:index-body",
					                                                   this._document.NamespaceManager);
					tableOfContents.IndexBodyNode	= indexBodyNode;
					ContentCollection pCollection	= new ContentCollection();

					foreach(XmlNode paragraphnode in paragraphNodeList)
					{
						Paragraph paragraph			= this.CreateParagraph(paragraphnode);
						if (indexBodyNode != null)
							indexBodyNode.RemoveChild(paragraphnode);
						//pCollection.Add(paragraph);
						AddToCollection(paragraph, pCollection);
					}

					foreach(IContent content in pCollection)
						AddToCollection(content, tableOfContents.Content);
					//tableOfContents.Content.Add(content);

					return tableOfContents;
				}

				return null;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a TableOfContents.", ex);
			}
		}

		/// <summary>
		/// Creates the paragraph.
		/// </summary>
		/// <param name="paragraphNode">The paragraph node.</param>
		public Paragraph CreateParagraph(XmlNode paragraphNode)
		{
			try
			{
				//Create a new Paragraph
				Paragraph paragraph				= new Paragraph(paragraphNode, this._document);
				//Recieve the ParagraphStyle
				IStyle paragraphStyle			= this._document.Styles.GetStyleByName(paragraph.StyleName);

				if (paragraphStyle != null)
				{
					paragraph.Style				= paragraphStyle;
				}
				else if (paragraph.StyleName != "Standard"
				        && paragraph.StyleName != "Table_20_Contents"
				        && paragraph.StyleName != "Text_20_body"
				        && this._document is TextDocument)
				{
					//Check if it's a user defined style
					IStyle commonStyle			= this._document.CommonStyles.GetStyleByName(paragraph.StyleName);
					if (commonStyle == null)
					{
						this.OnWarning(new AODLWarning(string.Format(
							"A ParagraphStyle '{0}' wasn't found.", paragraph.StyleName), paragraph.Node));
					}
				}

				return this.ReadParagraphTextContent(paragraph);
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Paragraph.", ex);
			}
		}

		/// <summary>
		/// Reads the content of the paragraph text.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <returns></returns>
		private Paragraph ReadParagraphTextContent(Paragraph paragraph)
		{
			try
			{
				if (this._debugMode)
					this.LogNode(paragraph.Node, "Log Paragraph node before");
				
				ArrayList mixedContent			= new ArrayList();
				foreach(XmlNode nodeChild in paragraph.Node.ChildNodes)
				{
					//Check for IText content first
					TextContentProcessor tcp	= new TextContentProcessor();
					IText iText					= tcp.CreateTextObject(this._document, nodeChild.CloneNode(true));
					
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
						if (this._debugMode)
							this.LogNode(((IText)ob).Node, "Log IText node read");
						paragraph.TextContent.Add(ob as IText);
					}
					else if (ob is IContent)
					{
						if (this._debugMode)
							this.LogNode(((IContent)ob).Node, "Log IContent node read");
						//paragraph.Content.Add(ob as IContent);
						AddToCollection(ob as IContent, paragraph.Content);
					}
					else
					{
						this.OnWarning(new AODLWarning("Couldn't determine the type of a paragraph child node!.",  paragraph.Node));
					}
				}

				if (this._debugMode)
					this.LogNode(paragraph.Node, "Log Paragraph node after");

				return paragraph;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create the Paragraph content.", ex);
			}
		}

		/// <summary>
		/// Creates the header.
		/// </summary>
		/// <param name="headernode">The headernode.</param>
		/// <returns></returns>
		public Header CreateHeader(XmlNode headernode)
		{
			try
			{
				if (this._debugMode)
					this.LogNode(headernode, "Log header node before");

				//Create a new Header
				Header header				= new Header(headernode, this._document);
				//Create a ITextCollection
				ITextCollection textColl	= new ITextCollection();
				//Recieve the HeaderStyle
				IStyle headerStyle			= this._document.Styles.GetStyleByName(header.StyleName);

				if (headerStyle != null)
					header.Style			= headerStyle;

				//Create the IText content
				foreach(XmlNode nodeChild in header.Node.ChildNodes)
				{
					TextContentProcessor tcp	= new TextContentProcessor();
					IText iText					= tcp.CreateTextObject(this._document, nodeChild);
					
					if (iText != null)
						textColl.Add(iText);
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create IText object from header child node!.", nodeChild));
					}
				}

				//Remove all
				header.Node.InnerXml		= "";

				foreach(IText iText in textColl)
				{
					if (this._debugMode)
						this.LogNode(iText.Node, "Log IText node read from header");
					header.TextContent.Add(iText);
				}

				return header;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Header.", ex);
			}
		}

		/// <summary>
		/// Creates the graphic.
		/// </summary>
		/// <param name="graphicnode">The graphicnode.</param>
		/// <returns>The Graphic object</returns>
		private Graphic CreateGraphic(XmlNode graphicnode)
		{
			try
			{
				Graphic graphic				= new Graphic(this._document, null, null);
				graphic.Node				= graphicnode;
				graphic.GraphicRealPath		= Path.Combine(_document.DirInfo.Dir, graphic.HRef);

				return graphic;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Graphic.", ex);
			}
		}

		/// <summary>
		/// Creates the draw text box.
		/// </summary>
		/// <param name="drawTextBoxNode">The draw text box node.</param>
		/// <returns></returns>
		private DrawTextBox CreateDrawTextBox(XmlNode drawTextBoxNode)
		{
			try
			{
				DrawTextBox drawTextBox		= new DrawTextBox(this._document, drawTextBoxNode);
				ContentCollection iColl	= new ContentCollection();

				foreach(XmlNode nodeChild in drawTextBox.Node.ChildNodes)
				{
					IContent iContent				= this.CreateContent(nodeChild);
					if (iContent != null)
						//iColl.Add(iContent);
						AddToCollection(iContent, iColl);
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create a IContent object for a DrawTextBox.", nodeChild));
					}
				}

				drawTextBox.Node.InnerXml					= "";

				foreach(IContent iContent in iColl)
					AddToCollection(iContent, drawTextBox.Content);
				//drawTextBox.Content.Add(iContent);

				return drawTextBox;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Graphic.", ex);
			}
		}

		/// <summary>
		/// Creates the draw area rectangle.
		/// </summary>
		/// <param name="drawAreaRectangleNode">The draw area rectangle node.</param>
		/// <returns></returns>
		private DrawAreaRectangle CreateDrawAreaRectangle(XmlNode drawAreaRectangleNode)
		{
			try
			{
				DrawAreaRectangle dAreaRec	= new DrawAreaRectangle(this._document, drawAreaRectangleNode);
				ContentCollection iCol		= new ContentCollection();

				if (dAreaRec.Node != null)
					foreach(XmlNode nodeChild in dAreaRec.Node.ChildNodes)
				{
					IContent iContent	= this.CreateContent(nodeChild);
					if (iContent != null)
						AddToCollection(iContent, iCol);
					//iCol.Add(iContent);
				}

				dAreaRec.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					AddToCollection(iContent, dAreaRec.Content);

				//dAreaRec.Content.Add(iContent);

				return dAreaRec;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a DrawAreaRectangle.", ex);
			}
		}

		/// <summary>
		/// Creates the draw area circle.
		/// </summary>
		/// <param name="drawAreaCircleNode">The draw area circle node.</param>
		/// <returns></returns>
		private DrawAreaCircle CreateDrawAreaCircle(XmlNode drawAreaCircleNode)
		{
			try
			{
				DrawAreaCircle dAreaCirc	= new DrawAreaCircle(this._document, drawAreaCircleNode);
				ContentCollection iCol		= new ContentCollection();

				if (dAreaCirc.Node != null)
					foreach(XmlNode nodeChild in dAreaCirc.Node.ChildNodes)
				{
					IContent iContent	= this.CreateContent(nodeChild);
					if (iContent != null)
						AddToCollection(iContent, iCol);
					//iCol.Add(iContent);
				}

				dAreaCirc.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					AddToCollection(iContent, dAreaCirc.Content);
				//dAreaCirc.Content.Add(iContent);

				return dAreaCirc;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a DrawAreaCircle.", ex);
			}
		}

		/// <summary>
		/// Creates the image map.
		/// </summary>
		/// <param name="imageMapNode">The image map node.</param>
		/// <returns></returns>
		private ImageMap CreateImageMap(XmlNode imageMapNode)
		{
			try
			{
				ImageMap imageMap			= new ImageMap(this._document, imageMapNode);
				ContentCollection iCol		= new ContentCollection();

				if (imageMap.Node != null)
					foreach(XmlNode nodeChild in imageMap.Node.ChildNodes)
				{
					IContent iContent	= this.CreateContent(nodeChild);
					if (iContent != null)
						AddToCollection(iContent, iCol);
					//iCol.Add(iContent);
				}

				imageMap.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					AddToCollection(iContent, imageMap.Content);
				//imageMap.Content.Add(iContent);

				return imageMap;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a ImageMap.", ex);
			}
		}

		/// <summary>
		/// Creates the event listener.
		/// </summary>
		/// <param name="eventListenerNode">The event listener node.</param>
		/// <returns></returns>
		public EventListener CreateEventListener(XmlNode eventListenerNode)
		{
			try
			{
				EventListener eventListener	= new EventListener(this._document, eventListenerNode);

				return eventListener;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a EventListener.", ex);
			}
		}

		/// <summary>
		/// Creates the event listeners.
		/// </summary>
		/// <param name="eventListenersNode">The event listeners node.</param>
		/// <returns></returns>
		public EventListeners CreateEventListeners(XmlNode eventListenersNode)
		{
			try
			{
				EventListeners eventList	= new EventListeners(this._document, eventListenersNode);
				ContentCollection iCol		= new ContentCollection();

				if (eventList.Node != null)
					foreach(XmlNode nodeChild in eventList.Node.ChildNodes)
				{
					IContent iContent	= this.CreateContent(nodeChild);
					if (iContent != null)
						AddToCollection(iContent,iCol);
					//iCol.Add(iContent);
				}

				eventList.Node.InnerXml		= "";

				foreach(IContent iContent in iCol)
					AddToCollection(iContent, eventList.Content);
				//eventList.Content.Add(iContent);

				return eventList;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a ImageMap.", ex);
			}
		}


		/// <summary>
		/// Creates the frame.
		/// </summary>
		/// <param name="frameNode">The framenode.</param>
		/// <returns>The Frame object.</returns>
		public ODFControlRef CreateControlRef(XmlNode refNode)
		{
			try
			{
				ODFControlRef controlRef = new ODFControlRef(this._document, refNode);
				return controlRef;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Control Reference.", ex);
			}
		}


		/// <summary>
		/// Creates the frame.
		/// </summary>
		/// <param name="frameNode">The framenode.</param>
		/// <returns>The Frame object.</returns>
		public Frame CreateFrame(XmlNode frameNode)
		{
			try
			{
				#region Old code Todo: delete
				//				Frame frame					= null;
				//				XmlNode graphicnode			= null;
				//				XmlNode graphicproperties	= null;
				//				string realgraphicname		= "";
				//				string stylename			= "";
				//				stylename					= this.GetStyleName(framenode.OuterXml);
				//				XmlNode stylenode			= this.GetAStyleNode("style:style", stylename);
				//				realgraphicname				= this.GetAValueFromAnAttribute(framenode, "@draw:name");
				//
				//				//Console.WriteLine("frame: {0}", framenode.OuterXml);
				//
				//				//Up to now, the only sopported, inner content of a frame is a graphic
				//				if (framenode.ChildNodes.Count > 0)
				//					if (framenode.ChildNodes.Item(0).OuterXml.StartsWith("<draw:image"))
				//						graphicnode			= framenode.ChildNodes.Item(0).CloneNode(true);
				//
				//				//If not graphic, it could be text-box, ole or something else
				//				//try to find graphic frame inside
				//				if (graphicnode == null)
				//				{
				//					XmlNode child		= framenode.SelectSingleNode("//draw:frame", this._document.NamespaceManager);
				//					if (child != null)
				//						frame		= this.CreateFrame(child);
				//					return frame;
				//				}
				//
				//				string graphicpath			= this.GetAValueFromAnAttribute(graphicnode, "@xlink:href");
				//
				//				if (stylenode != null)
				//					if (stylenode.ChildNodes.Count > 0)
				//						if (stylenode.ChildNodes.Item(0).OuterXml.StartsWith("<style:graphic-properties"))
				//							graphicproperties	= stylenode.ChildNodes.Item(0).CloneNode(true);
				//
				//				if (stylename.Length > 0 && stylenode != null && realgraphicname.Length > 0
				//					&& graphicnode != null && graphicpath.Length > 0 && graphicproperties != null)
				//				{
				//					graphicpath				= graphicpath.Replace("Pictures", "");
				//					graphicpath				= OpenDocumentTextImporter.dirpics+graphicpath.Replace("/", @"\");
				//
				//					frame					= new Frame(this._document, stylename,
				//												realgraphicname, graphicpath);
				//
				//					frame.Style.Node		= stylenode;
				//					frame.Graphic.Node		= graphicnode;
				//					((FrameStyle)frame.Style).GraphicProperties.Node = graphicproperties;
				//
				//					XmlNode nodeSize		= framenode.SelectSingleNode("@svg:height",
				//						this._document.NamespaceManager);
				//
				//					if (nodeSize != null)
				//						if (nodeSize.InnerText != null)
				//							frame.GraphicHeight	= nodeSize.InnerText;
				//
				//					nodeSize		= framenode.SelectSingleNode("@svg:width",
				//						this._document.NamespaceManager);
				//
				//					if (nodeSize != null)
				//						if (nodeSize.InnerText != null)
				//							frame.GraphicWidth	= nodeSize.InnerText;
				//				}
				#endregion
				
				//Create a new Frame
				Frame frame					= new Frame(this._document, null);
				frame.Node					= frameNode;
				ContentCollection iColl	= new ContentCollection();
				//Revieve the FrameStyle
				IStyle frameStyle			= this._document.Styles.GetStyleByName(frame.StyleName);

				if (frameStyle != null)
					frame.Style					= frameStyle;
				else
				{
					this.OnWarning(new AODLWarning("Couldn't recieve a FrameStyle.", frameNode));
				}

				//Create the frame content
				foreach(XmlNode nodeChild in frame.Node.ChildNodes)
				{
					IContent iContent				= this.CreateContent(nodeChild);
					if (iContent != null)
						AddToCollection(iContent, iColl);
					//iColl.Add(iContent);
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create a IContent object for a frame.", nodeChild));
					}
				}

				frame.Node.InnerXml					= "";

				foreach(IContent iContent in iColl)
				{
					AddToCollection(iContent, frame.Content);
					//frame.Content.Add(iContent);
					if (iContent is Graphic)
					{
						LoadFrameGraphic(frame, iContent as Graphic);
					}

					if (iContent is EmbedObject)
					{
						//(EmbedObject(iContent)).Frame  =frame;
						(iContent as EmbedObject).Frame = frame;
					}
				}
				return frame;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Frame.", ex);
			}
		}

		private void LoadFrameGraphic(Frame frame, Graphic content)
		{
			try
			{
				string graphicRealPath = Path.GetFullPath(content.GraphicRealPath);
				frame.LoadImageFromFile(graphicRealPath);
			}
			catch (AODLGraphicException e)
			{
				this.OnWarning(
					new AODLWarning("A couldn't create any content from an an first level node!.", content.Node, e));
				
			}
		}
		
		private EmbedObject CreateEmbedObject(XmlNode ObjNode)
		{
			try
			{
				XmlNode ObjectNode                  = ObjNode.CloneNode (true);

				XmlNode node                        = ObjectNode.SelectSingleNode ("@xlink:href",this._document .NamespaceManager );

				string ObjectFullPath               = node.InnerText.Substring (2)+"/" ;

				string ObjectRealPath               = node.InnerText .Substring (2);

				string ObjectName                   =ObjectRealPath;

				//ObjectRealPath                      = ObjectRealPath.Replace ("/","\\");


				ObjectRealPath                      = Path.Combine (_document.DirInfo.Dir, ObjectRealPath);
				
				string MediaType                    = GetMediaType(ObjectFullPath);

				EmbedObjectHandler embedobjhandler  = new EmbedObjectHandler (this._document );

				return embedobjhandler.CreateEmbedObject (ObjectNode,MediaType,ObjectRealPath,ObjectName);
			}
			
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Graphic.", ex);
			}
		}

		public string GetMediaType(string ObjectFullPath)
		{
			XmlDocument doc                 = ((SpreadsheetDocument)this._document).DocumentManifest.Manifest;
			XmlNode  node                   = doc.SelectSingleNode ("/manifest:manifest",this._document.NamespaceManager );
			
			foreach(XmlNode nodeChild in node.ChildNodes )
			{
				//XmlNode Entry  = nodeChild.SelectSingleNode ("@manifest:file-entry",this._document.NamespaceManager);
				XmlNode FullPath            = nodeChild.SelectSingleNode ("@manifest:full-path",this._document.NamespaceManager);

				if (FullPath.InnerText ==ObjectFullPath)
				{
					XmlNode MediaType       = nodeChild.SelectSingleNode ( "@manifest:media-type",this._document.NamespaceManager);

					if (MediaType!=null && MediaType.InnerText !="")
						
						return MediaType.InnerText ;
				}
			}

			return null;
		}

		/// <summary>
		/// Creates the list.
		/// </summary>
		/// <param name="listNode">The list node.</param>
		/// <returns>The List object</returns>
		private List CreateList(XmlNode listNode)
		{
			try
			{
				#region Old code Todo: delete
				//				string stylename				= null;
				//				XmlNode	stylenode				= null;
				//				ListStyles liststyles			= ListStyles.Bullet; //as default
				//				string paragraphstylename		= null;
				//
				//				if (outerlist == null)
				//				{
				//					stylename			= this.GetStyleName(listNode.OuterXml);
				//					stylenode			= this.GetAStyleNode("text:list-style", stylename);
				//					liststyles			= this.GetListStyle(listNode);
				//				}
				//				List list					= null;
				//
				//				if (listNode.ChildNodes.Count > 0)
				//				{
				//					try
				//					{
				//						paragraphstylename	= this.GetAValueFromAnAttribute(listNode.ChildNodes.Item(0).ChildNodes.Item(0), "@style:style-name");
				//					}
				//					catch(Exception ex)
				//					{
				//						paragraphstylename	= "P1";
				//					}
				//				}
				#endregion
				//Create a new List
				List list					= new List(this._document, listNode);
				ContentCollection iColl	= new ContentCollection();
				//Revieve the ListStyle
				IStyle listStyle			= this._document.Styles.GetStyleByName(list.StyleName);

				if (listStyle != null)
					list.Style				= listStyle;

				foreach(XmlNode nodeChild in list.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);

					if (iContent != null)
						AddToCollection(iContent, iColl);
					//iColl.Add(iContent);
				}

				list.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
					AddToCollection(iContent, list.Content);
				//list.Content.Add(iContent);
				
				return list;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a List.", ex);
			}
		}

		/// <summary>
		/// Creates the list item.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private ListItem CreateListItem(XmlNode node)
		{
			try
			{

				ListItem listItem			= new ListItem(this._document);
				ContentCollection iColl	= new ContentCollection();
				listItem.Node				= node;

				foreach(XmlNode nodeChild in listItem.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);
					if (iContent != null)
						AddToCollection(iContent, iColl);
					//iColl.Add(iContent);
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create a IContent object for a ListItem.", nodeChild));
					}
				}

				listItem.Node.InnerXml		= "";

				foreach(IContent iContent in iColl)
					//listItem.Content.Add(iContent);
					AddToCollection(iContent,listItem.Content);

				return listItem;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a ListItem.", ex);
			}
		}

		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <param name="tableNode">The tablenode.</param>
		/// <returns></returns>
		private Table CreateTable(XmlNode tableNode)
		{
			try
			{
				//Create a new table
				Table table					= new Table(this._document, tableNode);
				ContentCollection iColl	= new ContentCollection();
				//Recieve the table style
				IStyle tableStyle		= this._document.Styles.GetStyleByName(table.StyleName);

				if (tableStyle != null)
					table.Style				= tableStyle;
				else
				{
					this.OnWarning(new AODLWarning("Couldn't recieve a TableStyle.", tableNode));
				}
				
				

				//Create the table content
				foreach(XmlNode nodeChild in table.Node.ChildNodes)
				{
					IContent iContent				= this.CreateContent(nodeChild);
					
					if (iContent != null)
					{
						//iColl.Add(iContent);
						AddToCollection(iContent, iColl);
					}
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create IContent from a table node. Content is unknown table content!", iContent.Node));
					}
				}

				table.Node.InnerText					= "";

				foreach(IContent iContent in iColl)
				{
					if (iContent is Column)
					{
						((Column)iContent).Table	= table;
						table.ColumnCollection.Add(iContent as Column);
					}
					else if (iContent is Row)
					{
						((Row)iContent).Table		= table;
						table.Rows.Add(iContent as Row);
					}
					else if (iContent is RowHeader)
					{
						((RowHeader)iContent).Table	= table;
						table.RowHeader			= iContent as RowHeader;
					}
					else
					{
						table.Node.AppendChild(iContent.Node);
						this.OnWarning(new AODLWarning("Couldn't create IContent from a table node.", tableNode));
					}
				}
				
				return table;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Table.", ex);
			}
		}

		/// <summary>
		/// Creates the table row.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private Row CreateTableRow(XmlNode node)
		{
			try
			{
				//Create a new Row
				Row row						= new Row(this._document, node);
				ContentCollection iColl	= new ContentCollection();
				//Recieve RowStyle
				IStyle rowStyle				= this._document.Styles.GetStyleByName(row.StyleName);

				if (rowStyle != null)
					row.Style				= rowStyle;
				//No need for a warning

				//Create the cells
				foreach(XmlNode nodeChild in row.Node.ChildNodes)
				{
					// Phil Jollans 24-March-2008
					// Handle the attribute table:number-columns-repeated on cell nodes,
					// by inserting multiple nodes. CreateContent clones the nodes so this
					// seems fairly safe.
					int     iRepeatCount = 1;
					XmlNode xn           = nodeChild.SelectSingleNode ( "@table:number-columns-repeated", this._document.NamespaceManager );
					if ( xn != null )
					{
						iRepeatCount = int.Parse ( xn.InnerText );
						
						// Inetrnally, the node is no longer repeated, so it seems correct
						// to remove the the attribute table:number-columns-repeated.
						nodeChild.Attributes.RemoveNamedItem ( xn.Name ) ;
					}
					
					for ( int i = 0 ; i < iRepeatCount ; i++ )
					{
						IContent iContent		= this.CreateContent(nodeChild);

						if (iContent != null)
						{
							//iColl.Add(iContent);
							AddToCollection(iContent, iColl);
						}
						else
						{
							this.OnWarning(new AODLWarning("Couldn't create IContent from a table row.", nodeChild));
						}
					}
				}

				row.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
				{
					if (iContent is Cell)
					{
						((Cell)iContent).Row		= row;
						row.Cells.Add(iContent as Cell);
					}
					else if (iContent is CellSpan)
					{
						((CellSpan)iContent).Row	= row;
						row.CellSpanCollection.Add(iContent as CellSpan);
					}
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create IContent from a row node. Content is unknown table row content!", iContent.Node));
					}
				}

				return row;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Table Row.", ex);
			}
		}

		/// <summary>
		/// Creates the table header row.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private RowHeader CreateTableHeaderRow(XmlNode node)
		{
			try
			{
				//Create a new Row
				RowHeader rowHeader			= new RowHeader(this._document, node);
				ContentCollection iColl	= new ContentCollection();
				//Recieve RowStyle
				IStyle rowStyle				= this._document.Styles.GetStyleByName(rowHeader.StyleName);

				if (rowStyle != null)
					rowHeader.Style				= rowStyle;
				//No need for a warning

				//Create the cells
				foreach(XmlNode nodeChild in rowHeader.Node.ChildNodes)
				{
					IContent iContent			= this.CreateContent(nodeChild);

					if (iContent != null)
					{
						//iColl.Add(iContent);
						AddToCollection(iContent, iColl);
					}
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create IContent from a table row.", nodeChild));
					}
				}

				rowHeader.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
				{
					if (iContent is Row)
					{
						rowHeader.RowCollection.Add(iContent as Row);
					}
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create IContent from a row header node. Content is unknown table row header content!", iContent.Node));
					}
				}
				return rowHeader;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Table Row.", ex);
			}
		}

		/// <summary>
		/// Creates the table column.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private Column CreateTableColumn(XmlNode node)
		{
			try
			{
				//Create a new Row
				Column column				= new Column(this._document, node);
				//Recieve RowStyle
				IStyle columnStyle			= this._document.Styles.GetStyleByName(column.StyleName);

				if (columnStyle != null)
					column.Style			= columnStyle;
				//No need for a warning

				return column;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Table Column.", ex);
			}
		}

		/// <summary>
		/// Creates the table cell span.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private CellSpan CreateTableCellSpan(XmlNode node)
		{
			try
			{
				//Create a new CellSpan
				CellSpan cellSpan			= new CellSpan(this._document, node);
				
				//No need for a warnings or styles

				return cellSpan;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Table CellSpan.", ex);
			}
		}

		/// <summary>
		/// Creates the table cell.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private Cell CreateTableCell(XmlNode node)
		{
			try
			{
				//Create a new Cel
				Cell cell					= new Cell(this._document, node);
				ContentCollection iColl	= new ContentCollection();
				//Recieve CellStyle
				IStyle cellStyle			= this._document.Styles.GetStyleByName(cell.StyleName);

				if (cellStyle != null)
				{
					cell.Style				= cellStyle;
				}
				//No need for a warning

				//Create the cells content
				foreach(XmlNode nodeChild in cell.Node.ChildNodes)
				{
					IContent iContent		= this.CreateContent(nodeChild);

					if (iContent != null)
					{
						//iColl.Add(iContent);
						AddToCollection(iContent, iColl);
					}
					else
					{
						this.OnWarning(new AODLWarning("Couldn't create IContent from a table cell.", nodeChild));
					}
				}

				cell.Node.InnerXml			= "";

				foreach(IContent iContent in iColl)
					AddToCollection(iContent,cell.Content);
				//cell.Content.Add(iContent);
				return cell;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Table Row.", ex);
			}
		}

		/// <summary>
		/// Gets the A value from an attribute.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="attributname">The attributname.</param>
		/// <returns></returns>
		private string GetAValueFromAnAttribute(XmlNode node, string attributname)
		{
				Console.WriteLine(attributname);
				XmlNode nodeValue			= node.SelectSingleNode(attributname,
				                                            this._document.NamespaceManager);

				if (nodeValue != null)
					return nodeValue.InnerText;
			return "";
		}

		/// <summary>
		/// Gets the list style.
		/// </summary>
		/// <param name="node">The main list node.</param>
		/// <returns></returns>
		private ListStyles GetListStyle(XmlNode node)
		{
			try
			{
				if (node.ChildNodes.Count > 0)
				{
					XmlNode child		= node.ChildNodes.Item(0);
					string name			= child.Name;
					switch(name)
					{
						case "text:list-level-style-bullet":
							return ListStyles.Bullet;
						default:
							return ListStyles.Number;
					}
				}
			}
			catch(Exception ex)
			{
				throw new AODLException("error in GetListTyle", ex);
			}

			return ListStyles.Number;
		}

		/// <summary>
		/// Logs the node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="msg">The MSG.</param>
		private void LogNode(XmlNode node, string msg)
		{
			Console.WriteLine("\n#############################\n{0}", msg);
			XmlTextWriter writer	= new XmlTextWriter(Console.Out);
			writer.Formatting		= Formatting.Indented;
			node.WriteTo(writer);
		}
	}
}

//AODLTest.DocumentImportTest.SimpleLoadTest : System.IO.DirectoryNotFoundException : Could not find a part of the path "D:\OpenDocument\AODL\AODLTest\bin\Debug\GeneratedFiles\OpenOffice.net.odt.rel.odt".
/*
 * $Log: MainContentProcessor.cs,v $
 * Revision 1.10  2008/04/29 15:39:52  mt
 * new copyright header
 *
 * Revision 1.9  2008/04/10 17:33:23  larsbehr
 * - Added several bug fixes mainly for the table handling which are submitted by Phil  Jollans
 *
 * Revision 1.8  2008/02/08 07:12:20  larsbehr
 * - added initial chart support
 * - several bug fixes
 *
 * Revision 1.3  2007/05/29 13:43:25  yegorov
 * Issue number:  1.2
 * Submitted by:  Oleg Yegorov
 * Reviewed by:   Oleg Yegorov
 *
 * Revision 1.2  2007/04/08 16:51:37  larsbehr
 * - finished master pages and styles for text documents
 * - several bug fixes
 *
 * Revision 1.1  2007/02/25 08:58:45  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.6  2007/02/13 17:58:48  larsbm
 * - add first part of implementation of master style pages
 * - pdf exporter conversations for tables and images and added measurement helper
 *
 * Revision 1.5  2006/02/16 18:35:41  larsbm
 * - Add FrameBuilder class
 * - TextSequence implementation (Todo loading!)
 * - Free draing postioning via x and y coordinates
 * - Graphic will give access to it's full qualified path
 *   via the GraphicRealPath property
 * - Fixed Bug with CellSpan in Spreadsheetdocuments
 * - Fixed bug graphic of loaded files won't be deleted if they
 *   are removed from the content.
 * - Break-Before property for Paragraph properties for Page Break
 *
 * Revision 1.4  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.5  2006/01/05 10:28:06  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.4  2005/12/21 17:17:12  larsbm
 * - AODL new feature save gui settings
 * - Bugfixes, in MainContentProcessor
 *
 * Revision 1.3  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.2  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */