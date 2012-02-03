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
using System.Text;
using AODL.Document.Content;
using AODL.Document;
using System.Xml;
using AODL.Document.Forms;
using AODL.Document.Styles;
using AODL.Document.Helper;

namespace AODL.Document.Content.Fields
{
	public enum SelectPage {Previous, Next, Current, NotSet };
	
	public class PageNumber : Field
	{
		/// <summary>
		/// The value of a page number field can be adjusted by a specified number, allowing the display of
		/// page numbers of following or preceding pages.
		/// </summary>
		public int PageAdjustment
		{
			get
			{
				return int.Parse(this._node.SelectSingleNode("@text:page-adjust",
				                                             this._document.NamespaceManager).InnerText);
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@text:page-adjust",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("page-adjust", "text"));
				nd.InnerText = value.ToString();
			}
		}

		/// <summary>
		///  Is used to display the number of the previous or the following page rather than the number of the current page.
		/// </summary>
		public SelectPage SelectPage
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@text:select-page",
				                                         this._document.NamespaceManager);
				if (xn == null) return SelectPage.NotSet;
				string s = xn.InnerText;
				SelectPage at;
				switch (s)
				{
					case "current":
						at = SelectPage.Current;
						break;
					case "next":
						at = SelectPage.Next;
						break;
					case "previous":
						at = SelectPage.Previous;
						break;
					default:
						at = SelectPage.NotSet;
						break;
				}
				return at;
			}
			set
			{
				string s = string.Empty;
				switch (value)
				{
					case SelectPage.Current:
						s = "current";
						break;
					case SelectPage.Next:
						s = "next";
						break;
					case SelectPage.Previous:
						s = "previous";
						break;
					default:
						return;
				}
				XmlNode nd = this._node.SelectSingleNode("@text:select-page",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("select-page", "text"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies whether or not the value of a field element is fixed
		/// </summary>
		public XmlBoolean Fixed
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@text:fixed",
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
				XmlNode nd = this._node.SelectSingleNode("@text:fixed",
				                                         this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("fixed", "text"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the format of the number in the same way as the [XSLT] format attribute
		/// </summary>
		public string NumFormat
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@style:num-format",
				                                         this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@style:num-format",
				                                         this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("num-format", "style"));
				nd.InnerText = value;
			}
		}

		public PageNumber(IDocument document)
		{
			_document = document;
			_node = document.CreateNode("page-number", "text");
		}

		public PageNumber(IDocument document, int pageAdjustment)
		{
			_document = document;
			_node = document.CreateNode("page-number", "text");
			PageAdjustment = pageAdjustment;
		}

		private PageNumber(IDocument document, XmlNode node)
		{
			_document = document;
			_node = node;
		}
	}
}
