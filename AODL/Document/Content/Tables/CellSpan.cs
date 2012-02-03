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
using AODL.Document;

namespace AODL.Document.Content.Tables
{
	/// <summary>
	/// CellSpan used when cells are merged.
	/// </summary>
	public class CellSpan : IContent
	{
		private Row _row;
		/// <summary>
		/// Gets or sets the row.
		/// </summary>
		/// <value>The row.</value>
		public Row Row
		{
			get { return this._row; }
			set { this._row = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellSpan"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public CellSpan(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.Node				= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellSpan"/> class.
		/// The CellSpan class is only usable in TextDocuments.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="document">The document.</param>
		public CellSpan(Row row, IDocument document)
		{
			this.Document	= document;
			this.NewXmlNode();
		}

		/// <summary>
		/// News the XML node.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("covered-table-cell", "table");
		}

		#region IContent Member

		/// <summary>
		/// The Xml node
		/// </summary>
		private XmlNode _node;
		/// <summary>
		/// Gets or sets the node.
		/// </summary>
		/// <value>The node.</value>
		public XmlNode Node
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

		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get
			{
				// TODO:  Getter-Implementierung für CellSpan.StyleName hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für CellSpan.StyleName hinzufügen
			}
		}

		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.IStyle Style
		{
			get
			{
				// TODO:  Getter-Implementierung für CellSpan.Style hinzufügen
				return null;
			}
			set
			{
				// TODO:  Getter-Implementierung für CellSpan.Style hinzufügen
			}
		}

		#endregion
	}
}

/*
 * $Log: CellSpan.cs,v $
 * Revision 1.2  2008/04/29 15:39:45  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:36  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.2  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.1  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 */
