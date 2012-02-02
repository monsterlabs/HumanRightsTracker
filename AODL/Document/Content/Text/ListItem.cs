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
using AODL.Document.Styles;
using AODL.Document.Content;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// ListItem represent a list item which is used within a list.
	/// </summary>
	public class ListItem : IContent, IContentContainer, IHtml
	{
		private List _list;
		/// <summary>
		/// The List object to which this ListItem belongs.
		/// </summary>
		public List List
		{
			get { return this._list; }
			set { this._list = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListItem"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		public ListItem(IDocument document)
		{
			this.Document				= document;
			this.InitStandards();
		}

		/// <summary>
		/// Create a new ListItem
		/// </summary>
		/// <param name="list">The List object to which this ListItem belongs.</param>
		public ListItem(List list)
		{
			this.Document				= list.Document;
			this.List					= list;
			this.InitStandards();
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.NewXmlNode();
			this.Content				= new ContentCollection();

			this.Content.Inserted		+= Content_Inserted;
			this.Content.Removed		+= Content_Removed;
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node					= this.Document.CreateNode("list-item", "text");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa			= this.Document.CreateAttribute(name, prefix);
			xa.Value				= text;
			this.Node.Attributes.Append(xa);
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
				if (this._content != null)
					foreach(IContent content in this._content)
						this.Node.RemoveChild(content.Node);

				this._content = value;
				
				if (this._content != null)
					foreach(IContent content in this._content)
						this.Node.AppendChild(content.Node);
			}
		}
		#endregion

		#region IContent Member
		/// <summary>
		/// Gets or sets the name of the style.
		/// </summary>
		/// <value>The name of the style.</value>
		public string StyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@text:style-name",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("style-name", value, "text");
				this._node.SelectSingleNode("@text:style-name",
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

		#region IHtml Member

		/// <summary>
		/// Return the content as Html string
		/// </summary>
		/// <returns>The html string</returns>
		public string GetHtml()
		{
			string html			= "<li>\n";

			//Support for vers. < 1.1.1.0
//			if (this.Paragraph != null)
//			{
//				string pHtml	= this.Paragraph.GetHtml();
//				if (pHtml != "<p >\n&nbsp;</p>\n" 
//					&& !pHtml.StartsWith("<p >\n</p>")
//					&& pHtml != "<p >\n </p>\n"
//					&& pHtml != "<p >\n</p>\n")
//					html		+= pHtml+"\n";
//			}

			foreach(IContent content in this.Content)
				if (content is IHtml)
					html		+= ((IHtml)content).GetHtml();

			html				+= "</li>\n";

			return html;
		}

		#endregion
	}
}

/*
 * $Log: ListItem.cs,v $
 * Revision 1.2  2008/04/29 15:39:46  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:39  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.2  2006/02/05 20:02:25  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:22  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.3  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.2  2005/10/23 16:47:48  larsbm
 * - Bugfix ListItem throws IStyleInterface not implemented exeption
 * - now. build the document after call saveto instead prepare the do at runtime
 * - add remove support for IText objects in the paragraph class
 *
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */