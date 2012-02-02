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

using System.Collections;
using AODL.Document;
using AODL.Document.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Represent a TabStopStyle collection which could be
	/// used within a ParagraphStyle object.
	/// Notice: A TabStopStyleCollection will not work
	/// within a Standard Paragraph!
	/// </summary>
	public class TabStopStyleCollection : CollectionWithEvents<TabStopStyle>
	{
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

		private IDocument _document;
		/// <summary>
		/// Gets or sets the document.
		/// </summary>
		/// <value>The document.</value>
		public IDocument Document
		{
			get { return this._document; }
			set { this._document = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TabStopStyleCollection"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public TabStopStyleCollection(IDocument document)
		{
			this.Document		= document;
			this.Node			= this.Document.CreateNode("tab-stops", "style");
		}

		/// <summary>
		/// Adds the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public new TabStopStyleCollection Add(AODL.Document.Styles.TabStopStyle value)
		{
			this.Node.AppendChild(((TabStopStyle)value).Node);
			base.Add(value);
			return this;
		}

		/// <summary>
		/// Removes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public new void Remove(AODL.Document.Styles.TabStopStyle value)
		{
			this.Node.RemoveChild(((TabStopStyle)value).Node);
			base.Remove(value);
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public new void Insert(int index, AODL.Document.Styles.TabStopStyle value)
		{
			//It's not necessary to know the postion of the child node
			this.Node.AppendChild(((TabStopStyle)value).Node);
			base.Insert(index, value);
		}
	}
}

/*
 * $Log: TabStopStyleCollection.cs,v $
 * Revision 1.2  2008/04/29 15:39:54  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:50  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.1  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 */