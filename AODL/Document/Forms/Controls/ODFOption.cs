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
	#region ODFOption class
	
	public class ODFOption
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
		/// Specifies if the option is currently selected
		/// </summary>
		public XmlBoolean CurrentSelected
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:current-selected", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:current-selected", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("current-selected", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the default state of a radio button or option
		/// </summary>
		public XmlBoolean Selected
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:selected", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:selected", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("selected", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the default value of the control
		/// </summary>
		public string Value
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:value", 
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:value", 
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("value", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Contains a label for the control
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
		/// Creates an ODFOption
		/// </summary>
		/// <param name="document">Main document</param>
		/// <param name="label">Option label</param>
		public ODFOption(IDocument document, string label)
		{
			Document = document;
			Node = document.CreateNode("option", "form");
			Label = label;
		}

		/// <summary>
		/// Creates an ODFOption
		/// </summary>
		/// <param name="document">Main document</param>
		/// <param name="label">Option label</param>
		/// <param name="val">Option value</param>
		public ODFOption(IDocument document, string label, string val)
		{
			Document = document;
			Node = document.CreateNode("option", "form");
			Value = val;
			Label = label;
		}

		/// <summary>
		/// Creates an ODFOption
		/// </summary>
		/// <param name="document">Main document</param>
		/// <param name="label">Option label</param>
		/// <param name="val">Option value</param>
		/// <param name="currentSelected">Is it currently selected?</param>
		public ODFOption(IDocument document, string label, string val, XmlBoolean currentSelected)
		{
			Document = document;
			Node = document.CreateNode("option", "form");
			CurrentSelected = currentSelected;
			Value = val;
			Label = label;
			CurrentSelected = currentSelected;
		}

		/// <summary>
		/// Creates an ODFOption
		/// </summary>
		/// <param name="document">Main document</param>
		public ODFOption(IDocument document)
		{
			Document = document;
			Node = document.CreateNode("option", "form");
		}

		public ODFOption(IDocument document, XmlNode node)
		{
			Document = document;
			Node = node;
		}
	}
	#endregion
}
