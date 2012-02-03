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
using System.Collections;
using AODL.Document;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using AODL.Document.SpreadsheetDocuments;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	/// <summary>
	/// LocalStyleProcessor.
	/// </summary>
	public class LocalStyleProcessor
	{
		/// <summary>
		/// The document
		/// </summary>
		private IDocument _document;
		/// <summary>
		/// Is true if the common styles are read.
		/// </summary>
		private bool _common;
		/// <summary>
		/// XPath for the common styles.
		/// </summary>
		private string _xmlPathCommonStyle = "/office:document-content/office:styles";

		private XmlDocument _currentXmlDocument;
		/// <summary>
		/// Gets or sets the current XML document.
		/// </summary>
		/// <value>The current XML document.</value>
		public XmlDocument CurrentXmlDocument
		{
			get { return this._currentXmlDocument; }
			set { this._currentXmlDocument = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalStyleProcessor"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="readCommonStyles">if set to <c>true</c> [read common styles].</param>
		public LocalStyleProcessor(IDocument document, bool readCommonStyles)
		{
			this._document				= document;
			this._currentXmlDocument	= this._document.XmlDoc;
			this._common				= readCommonStyles;
		}

		/// <summary>
		/// Reads the styles.
		/// </summary>
		public void ReadStyles()
		{
			XmlNode automaticStyleNode	= null;
			
			if (!this._common)
				automaticStyleNode	= this._document.XmlDoc.SelectSingleNode(
					TextDocumentHelper.AutomaticStylePath, this._document.NamespaceManager);
			else
				automaticStyleNode	= this._document.XmlDoc.SelectSingleNode(
					this._xmlPathCommonStyle, this._document.NamespaceManager);
			
			if (automaticStyleNode != null)
			{
				foreach(XmlNode styleNode in automaticStyleNode)
				{
					XmlNode family			= styleNode.SelectSingleNode("@style:family",
						this._document.NamespaceManager);

					if (family != null)
					{
						if (family.InnerText == "table")
						{
							this.CreateTableStyle(styleNode);
						}
						else if (family.InnerText == "table-column")
						{
							this.CreateColumnStyle(styleNode);
						}
						else if (family.InnerText == "table-row")
						{
							this.CreateRowStyle(styleNode);
						}
						else if (family.InnerText == "table-cell")
						{
							this.CreateCellStyle(styleNode);
						}
						else if (family.InnerText == "paragraph")
						{
							this.CreateParagraphStyle(styleNode);
						}
						else if (family.InnerText == "graphic")
						{
							this.CreateFrameStyle(styleNode);
						}
						else if (family.InnerText == "section")
						{
							this.CreateSectionStyle(styleNode);
						}
						else if (family.InnerText == "text")
						{
							this.CreateTextStyle(styleNode);
						}
					}
					else if (styleNode.Name == "text:list-style")
					{
						this.CreateListStyle(styleNode);
					}
					else
					{
						this.CreateUnknownStyle(styleNode);
					}
				}
			}
			automaticStyleNode.RemoveAll();
		}


		/// <summary>
		/// Re read known automatic styles.
		/// </summary>
		/// <remarks>
		/// NOTICE: The re read nodes will be deleted.
		/// </remarks>
		public void ReReadKnownAutomaticStyles()
		{
			XmlNode contentAutomaticStyles = this._document.XmlDoc.SelectSingleNode(
				"//office:automatic-styles", this._document.NamespaceManager);
			if (contentAutomaticStyles != null && contentAutomaticStyles.HasChildNodes)
			{
				ArrayList deleteNodes = new ArrayList();
				foreach(XmlNode styleNode in contentAutomaticStyles.ChildNodes)
				{
					XmlNode family			= styleNode.SelectSingleNode("@style:family",
						this._document.NamespaceManager);

					if (family != null)
					{
						if (family.InnerText == "table")
						{
							this.CreateTableStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "table-column")
						{
							this.CreateColumnStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "table-row")
						{
							this.CreateRowStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "table-cell")
						{
							this.CreateCellStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "paragraph")
						{
							this.CreateParagraphStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "graphic")
						{
							this.CreateFrameStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "section")
						{
							this.CreateSectionStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
						else if (family.InnerText == "text")
						{
							this.CreateTextStyle(styleNode);
							deleteNodes.Add(styleNode);
						}
					}
					else if (styleNode.Name == "text:list-style")
					{
						this.CreateListStyle(styleNode);
						deleteNodes.Add(styleNode);
					}
				}

				foreach(XmlNode deleteNode in deleteNodes)
				{
					contentAutomaticStyles.RemoveChild(deleteNode);
				}
			}
		}

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private IProperty GetProperty(IStyle style, XmlNode propertyNode)
		{
			if (propertyNode != null && style != null)
			{
				if (propertyNode.Name == "style:table-cell-properties")
					return this.CreateCellProperties(style, propertyNode);
				else if (propertyNode.Name == "style:table-column-properties")
					return this.CreateColumnProperties(style, propertyNode);
				if (propertyNode.Name == "style:graphic-properties")
					return this.CreateGraphicProperties(style, propertyNode);
				if (propertyNode.Name == "style:paragraph-properties")
					return this.CreateParagraphProperties(style, propertyNode);
				if (propertyNode.Name == "style:table-row-properties")
					return this.CreateRowProperties(style, propertyNode);
				if (propertyNode.Name == "style:section-properties")
					return this.CreateSectionProperties(style, propertyNode);
				if (propertyNode.Name == "style:table-properties")
					return this.CreateTableProperties(style, propertyNode);
				if (propertyNode.Name == "style:text-properties")
					return this.CreateTextProperties(style, propertyNode);
				else
					return this.CreateUnknownProperties(style, propertyNode);
			}
			else
				return null;
		}

		/// <summary>
		/// Creates the unknown style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateUnknownStyle(XmlNode styleNode)
		{
			if (!this._common)
				this._document.Styles.Add(new UnknownStyle(this._document, styleNode.CloneNode(true)));
			else
				this._document.CommonStyles.Add(new UnknownStyle(this._document, styleNode.CloneNode(true)));			
		}

		/// <summary>
		/// Creates the table style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateTableStyle(XmlNode styleNode)
		{
			TableStyle tableStyle				= new TableStyle(this._document);
			tableStyle.Node						= styleNode.CloneNode(true);
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(tableStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			tableStyle.Node.InnerXml			= "";

			foreach(IProperty property in pCollection)
				tableStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(tableStyle);
			else
				this._document.CommonStyles.Add(tableStyle);
		}

		/// <summary>
		/// Creates the column style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateColumnStyle(XmlNode styleNode)
		{
			ColumnStyle columnStyle				= new ColumnStyle(this._document);
			columnStyle.Node						= styleNode.CloneNode(true);
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(columnStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			columnStyle.Node.InnerXml			= "";

			foreach(IProperty property in pCollection)
				columnStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(columnStyle);
			else
				this._document.CommonStyles.Add(columnStyle);
		}

		/// <summary>
		/// Creates the row style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateRowStyle(XmlNode styleNode)
		{
			RowStyle rowStyle					= new RowStyle(this._document);
			rowStyle.Node						= styleNode.CloneNode(true);
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(rowStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			rowStyle.Node.InnerXml				= "";

			foreach(IProperty property in pCollection)
				rowStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(rowStyle);
			else
				this._document.CommonStyles.Add(rowStyle);
		}

		/// <summary>
		/// Creates the cell style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateCellStyle(XmlNode styleNode)
		{
			CellStyle cellStyle					= new CellStyle(this._document);
			cellStyle.Node						= styleNode.CloneNode(true);
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(cellStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			cellStyle.Node.InnerXml				= "";

			foreach(IProperty property in pCollection)
				cellStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(cellStyle);
			else
				this._document.CommonStyles.Add(cellStyle);
		}

		/// <summary>
		/// Creates the list style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateListStyle(XmlNode styleNode)
		{
			ListStyle listStyle					= new ListStyle(this._document, styleNode);
			
			if (!this._common)
				this._document.Styles.Add(listStyle);
			else
				this._document.CommonStyles.Add(listStyle);
		}

		/// <summary>
		/// Creates the paragraph style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateParagraphStyle(XmlNode styleNode)
		{
			ParagraphStyle paragraphStyle			= new ParagraphStyle(this._document, styleNode);
			IPropertyCollection pCollection			= new IPropertyCollection();			

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(paragraphStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);

				}
			}

			paragraphStyle.Node.InnerXml		= "";

			foreach(IProperty property in pCollection)
				paragraphStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(paragraphStyle);
			else
				this._document.CommonStyles.Add(paragraphStyle);
		}

		/// <summary>
		/// Creates the text style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateTextStyle(XmlNode styleNode)
		{
			TextStyle textStyle					= new TextStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(textStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			textStyle.Node.InnerXml				= "";

			foreach(IProperty property in pCollection)
				textStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(textStyle);
			else
				this._document.CommonStyles.Add(textStyle);
		}

		/// <summary>
		/// Creates the frame style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateFrameStyle(XmlNode styleNode)
		{
			FrameStyle frameStyle				= new FrameStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(frameStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			frameStyle.Node.InnerXml			= "";

			foreach(IProperty property in pCollection)
				frameStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(frameStyle);
			else
				this._document.CommonStyles.Add(frameStyle);
		}

		/// <summary>
		/// Creates the section style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		private void CreateSectionStyle(XmlNode styleNode)
		{
			SectionStyle sectionStyle			= new SectionStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection pCollection		= new IPropertyCollection();

			if (styleNode.HasChildNodes)
			{
				foreach(XmlNode node in styleNode.ChildNodes)
				{
					IProperty property			= this.GetProperty(sectionStyle, node.CloneNode(true));
					if (property != null)
						pCollection.Add(property);
				}
			}

			sectionStyle.Node.InnerXml			= "";

			foreach(IProperty property in pCollection)
				sectionStyle.PropertyCollection.Add(property);

			if (!this._common)
				this._document.Styles.Add(sectionStyle);
			else
				this._document.CommonStyles.Add(sectionStyle);
		}

		/// <summary>
		/// Creates the tab stop style.
		/// </summary>
		/// <param name="styleNode">The style node.</param>
		/// <returns></returns>
		private TabStopStyle CreateTabStopStyle(XmlNode styleNode)
		{
			TabStopStyle tabStopStyle			= new TabStopStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection pCollection		= new IPropertyCollection();

			return tabStopStyle;
		}

		/// <summary>
		/// Creates the table properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private TableProperties CreateTableProperties(IStyle style, XmlNode propertyNode)
		{
			TableProperties tableProperties		= new TableProperties(style);
			tableProperties.Node				= propertyNode;

			return tableProperties;
		}

		/// <summary>
		/// Creates the column properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private ColumnProperties CreateColumnProperties(IStyle style, XmlNode propertyNode)
		{
			ColumnProperties columnProperties	= new ColumnProperties(style);
			columnProperties.Node				= propertyNode;

			return columnProperties;
		}

		/// <summary>
		/// Creates the row properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private RowProperties CreateRowProperties(IStyle style, XmlNode propertyNode)
		{
			RowProperties rowProperties			= new RowProperties(style);
			rowProperties.Node					= propertyNode;

			return rowProperties;
		}

		/// <summary>
		/// Creates the cell properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private CellProperties CreateCellProperties(IStyle style, XmlNode propertyNode)
		{
			CellProperties cellProperties		= new CellProperties(style as CellStyle);
			cellProperties.Node					= propertyNode;

			return cellProperties;
		}

		/// <summary>
		/// Creates the graphic properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private GraphicProperties CreateGraphicProperties(IStyle style, XmlNode propertyNode)
		{
			GraphicProperties graphicProperties	= new GraphicProperties(style);
			graphicProperties.Node				= propertyNode;

			return graphicProperties;
		}

		/// <summary>
		/// Creates the paragraph properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private ParagraphProperties CreateParagraphProperties(IStyle style, XmlNode propertyNode)
		{
			ParagraphProperties paragraphProperties	= new ParagraphProperties(style);
			paragraphProperties.Node				= propertyNode;
			TabStopStyleCollection tabCollection	= new TabStopStyleCollection(this._document);

			if (propertyNode.HasChildNodes)
				foreach(XmlNode node in propertyNode.ChildNodes)
					if (node.Name == "style:tab-stops")
						foreach(XmlNode nodeTab in node.ChildNodes)
							if (nodeTab.Name == "style:tab-stop")
								tabCollection.Add(this.CreateTabStopStyle(nodeTab));

			if (tabCollection.Count > 0)
				paragraphProperties.TabStopStyleCollection = tabCollection;

			return paragraphProperties;
		}

		/// <summary>
		/// Creates the text properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private TextProperties CreateTextProperties(IStyle style, XmlNode propertyNode)
		{
			TextProperties textProperties		= new TextProperties(style);
			textProperties.Node					= propertyNode;

			return textProperties;
		}

		/// <summary>
		/// Creates the section properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private SectionProperties CreateSectionProperties(IStyle style, XmlNode propertyNode)
		{
			SectionProperties sectionProperties	= new SectionProperties(style);
			sectionProperties.Node				= propertyNode;

			return sectionProperties;
		}
		
		/// <summary>
		/// Creates the unknown properties.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="propertyNode">The property node.</param>
		/// <returns></returns>
		private UnknownProperty CreateUnknownProperties(IStyle style, XmlNode propertyNode)
		{
			return new UnknownProperty(style, propertyNode);
		}
	}
}
