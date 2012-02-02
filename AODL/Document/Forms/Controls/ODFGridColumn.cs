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
using AODL.Document;
using AODL.Document.Forms;
using System.Xml;
using AODL.Document.Content;

namespace AODL.Document.Forms.Controls
{
	#region ODFGridColumn class
	
	public class ODFGridColumn
	{
		protected XmlNode _node;
		protected IDocument _document;

		public XmlNode Node
		{
			get { return this._node; }
			set { this._node = value; }
		}
		/// <summary>
		/// The document
		/// </summary>
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
		/// Specifies the name of the column
		/// </summary>
		public string Name
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:name", 
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:name", 
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("name", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies the label of the column
		/// </summary>
		public string Label
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:label", 
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:label", 
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("label", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies the style of the column
		/// </summary>
		public string ColumnStyle
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:text-style-name", 
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:text-style-name", 
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("text-style-name", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Creates an ODFGridColumn
		/// </summary>
		/// <param name="document">Main document</param>
		/// <param name="name">Column name</param>
		/// <param name="label">Column label</param>
		/// <param name="style">Column style</param>
		public ODFGridColumn(IDocument document, string name, string label, string style)
		{
			Document = document;
			Node = document.CreateNode("column", "form");
			Name = name;
			Label = label;
			ColumnStyle = style;
		}

		/// <summary>
		/// Creates an ODFGridColumn
		/// </summary>
		/// <param name="document">Main document</param>
		/// <param name="name">Column name</param>
		/// <param name="label">Column label</param>
		public ODFGridColumn(IDocument document, string name, string label)
		{
			Document = document;
			Node = document.CreateNode("column", "form");
			Name = name;
			Label = label;
		}

		/// <summary>
		/// Creates an ODFGridColumn
		/// </summary>
		/// <param name="document">Main document</param>
		public ODFGridColumn(IDocument document)
		{
			Document = document;
			Node = document.CreateNode("column", "form");
		}

		public ODFGridColumn(IDocument document, XmlNode node)
		{
			Document = document;
			Node = node;
		}
	}
	#endregion
}
