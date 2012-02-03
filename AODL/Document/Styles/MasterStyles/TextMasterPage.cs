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
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles.MasterStyles
{
	/// <summary>
	/// Summary for TextMasterPage.
	/// </summary>
	public class TextMasterPage
	{
		private TextPageLayout _textPageLayout;
		/// <summary>
		/// Gets or sets the text page layout.
		/// </summary>
		/// <value>The text page layout.</value>
		public TextPageLayout TextPageLayout
		{
			get { return this._textPageLayout; }
			set { this._textPageLayout = value; }
		}

		private TextPageHeader _textPageHeader;
		/// <summary>
		/// Gets or sets the text page header.
		/// </summary>
		/// <value>The text page header.</value>
		public TextPageHeader TextPageHeader
		{
			get { return this._textPageHeader; }
			set { this._textPageHeader = value; }
		}

		private TextPageFooter _textPageFooter;
		/// <summary>
		/// Gets or sets the text page footer.
		/// </summary>
		/// <value>The text page footer.</value>
		public TextPageFooter TextPageFooter
		{
			get { return this._textPageFooter; }
			set { this._textPageFooter = value; }
		}

		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:name",
					this.TextDocument.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:name",
					this.TextDocument.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("name", value, "style");
				this._node.SelectSingleNode("@style:name",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the page layout.
		/// </summary>
		/// <value>The name of the page layout.</value>
		public string PageLayoutName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:page-layout-name",
					this.TextDocument.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:page-layout-name",
					this.TextDocument.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("page-layout-name", value, "style");
				this._node.SelectSingleNode("@style:page-layout-name",
					this.TextDocument.NamespaceManager).InnerText = value;
			}
		}

		private TextDocument _textDocument;
		/// <summary>
		/// Gets or sets the text document.
		/// </summary>
		/// <value>The text document.</value>
		public TextDocument TextDocument
		{
			get { return this._textDocument; }
			set { this._textDocument = value; }
		}

		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the master layout element.
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

		/// <summary>
		/// Initializes a new instance of the <see cref="TextMasterPage"/> class.
		/// </summary>
		public TextMasterPage()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextMasterPage"/> class.
		/// </summary>
		/// <param name="ownerTextDocument">The owner text document.</param>
		/// <param name="masterPageNode">The master page node.</param>
		public TextMasterPage(TextDocument ownerTextDocument, XmlNode masterPageNode)
		{
			this._textDocument = ownerTextDocument;
			this._node = masterPageNode;
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.TextDocument.CreateAttribute(prefix, name);
			xa.Value		= text;
			this._node.Attributes.Append(xa);
		}

		/// <summary>
		/// Activate usage of the page header.
		/// </summary>
		public void ActivatePageHeader()
		{
			if (this._textPageHeader != null)
			{
				this.TextPageHeader.Activate();
			}
		}

		/// <summary>
		/// Activate usage of the page footer.
		/// </summary>
		public void ActivatePageFooter()
		{
			if (this._textPageFooter != null)
			{
				this.TextPageFooter.Activate();
			}
		}

		/// <summary>
		/// Activate usage of the page header and page footer.
		/// </summary>
		public void ActivatePageHeaderAndFooter()
		{
			this.ActivatePageHeader();
			this.ActivatePageFooter();			
		}
	}
}
