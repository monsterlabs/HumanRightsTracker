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
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles.Properties;

namespace AODL.Document.Styles
{
	public abstract class AbstractStyle : IStyle
	{
		private string m_styleNameCache = null;
		
		public AbstractStyle()
		{
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+= PropertyCollection_Inserted;
			this.PropertyCollection.Removed		+= PropertyCollection_Removed;
		}
		
		protected XmlNode _node;
		/// <summary>
		/// The Xml node which represent the
		/// style
		/// </summary>
		/// <value></value>
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
		
		/// <summary>
		/// Properties the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IProperty)value).Node);
		}

		/// <summary>
		/// Properties the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IProperty)value).Node);
		}
		
		
		private IDocument _document;
		/// <summary>
		/// The document to which this style
		/// belongs.
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
		
		private IPropertyCollection _propertyCollection;
		/// <summary>
		/// Collection of properties.
		/// </summary>
		/// <value></value>
		public IPropertyCollection PropertyCollection
		{
			get { return this._propertyCollection; }
			set { this._propertyCollection = value; }
		}
		
		private string GetStyleWithCache()
		{
			if (m_styleNameCache != null)
				return m_styleNameCache;
			
			m_styleNameCache = GetStyleName();
			return m_styleNameCache;
		}
		
		private string GetStyleName()
		{
			XmlNode xn = this.Node.SelectSingleNode("@style:name",
			                                        this.Document.NamespaceManager);
			if (xn != null)
				return xn.InnerText;
			return null;
		}
		
		/// <summary>
		/// The style name
		/// </summary>
		/// <value></value>
		public string StyleName
		{
			get
			{
				return GetStyleWithCache();
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:name",
				                                         this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("name", value, "style");
				this._node.SelectSingleNode("@style:name",
				                            this.Document.NamespaceManager).InnerText = value;
				m_styleNameCache = value;
			}
		}
		
		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}
	}
	
	/// <summary>
	/// All styles that can be used within an document
	/// in the OpenDocument format have to implement
	/// this interface.
	/// </summary>
	public interface IStyle
	{
		/// <summary>
		/// The Xml node which represent the
		/// style
		/// </summary>
		XmlNode Node {get; set;}
		/// <summary>
		/// The style name
		/// </summary>
		string StyleName {get;}
		/// <summary>
		/// The OpenDocument document to which this style
		/// belongs.
		/// </summary>
		IDocument Document {get; set;}
		/// <summary>
		/// Collection of properties.
		/// </summary>
		IPropertyCollection PropertyCollection {get; set;}
	}
}

/*
 * $Log: IStyle.cs,v $
 * Revision 1.2  2008/04/29 15:39:54  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:48  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */