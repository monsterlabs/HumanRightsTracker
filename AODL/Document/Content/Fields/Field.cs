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
using AODL.Document.Styles;

namespace AODL.Document.Content.Fields
{
	/// <summary>
	/// A base abstract class for all the fields
	/// </summary>
	public abstract class Field: IContent
	{
		protected IDocument _document;
		protected XmlNode _node;
		private ContentCollection _contentCollection;
		#region IContent Members

		public IDocument Document
		{
			get
			{
				return _document;
			}
			set
			{
				_document = value;
			}
		}

		public System.Xml.XmlNode Node
		{
			get
			{
				return _node;
			}
			set
			{
				_node = value;
			}
		}

		/// <summary>
		/// The stylename wihich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		public string StyleName
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:style-name",
					this.Document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@draw:style-name",
					this.Document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this.Document.CreateAttribute("style-name", "draw"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// A Style class wich is referenced with the content object.
		/// If no style is available this is null.
		/// </summary>
		public IStyle Style
		{
			get
			{
				return this._document.Styles.GetStyleByName(StyleName);
			}
			set
			{
				StyleName = value.StyleName;
			}
		}

		#endregion

		public ContentCollection ContentCollection {
			get { return _contentCollection; }
			set { _contentCollection = value; }
		}
		
		/// <summary>
		/// The inner content of the field
		/// </summary>
		public string Value
		{
			get
			{
				return _node.InnerText;
			}
			set
			{
				_node.InnerText = value;
			}
		}

        public Field()
		{
		}
	}
}
