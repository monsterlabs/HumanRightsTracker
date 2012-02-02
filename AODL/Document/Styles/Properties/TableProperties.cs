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

namespace AODL.Document.Styles.Properties
{
	/// <summary>
	/// TableProperties represent the table properties.
	/// </summary>
	public class TableProperties : IProperty, IHtmlStyle
	{
		/// <summary>
		/// Set the table width -> 16.99cm
		/// </summary>
		public string Width
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:width",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:width",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("width", value, "style");
				this._node.SelectSingleNode("@style:width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set the table align -> margin
		/// </summary>
		public string Align
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@table:align",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@table:align",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("align", value, "table");
				this._node.SelectSingleNode("@table:align",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Set the table shadow
		/// </summary>
		public string Shadow
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:shadow",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:shadow",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("shadow", value, "style");
				this._node.SelectSingleNode("@style:shadow",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Constructor create a new TableProperties instance.
		/// </summary>
		/// <param name="style"></param>
		public TableProperties(IStyle style)
		{
			this.Style			= style;
			this.NewXmlNode();
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		private void NewXmlNode()
		{
			IStyle style = this.Style;
			object doc = style.Document;
			IDocument document = doc as IDocument;
			System.Xml.XmlNode node = document.CreateNode("table-properties", "style");
			this.Node		= node;
			//Set default properties
			this.Width		= "16.99cm";
			this.Align		= "margin";
			this.Shadow		= "none";
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Style.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
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

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}
		#endregion

		#region IHtmlStyle Member

		/// <summary>
		/// Get the css style fragement
		/// </summary>
		/// <returns>The css style attribute</returns>
		public string GetHtmlStyle()
		{
			string style		= "style=\"";

			if (this.Width != null)
				style	+= "width: "+this.Width.Replace(",", ".")+"; ";

			if (!style.EndsWith("; "))
				style	= "";
			else
				style	+= "\"";

			//style		+= " border=\"1\"";

			return style;
		}

		#endregion
	}
}

/*
 * $Log: TableProperties.cs,v $
 * Revision 1.2  2008/04/29 15:39:56  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:56  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.2  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.2  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.1  2005/10/12 19:52:56  larsbm
 * - start table implementation
 * - added uml diagramm
 *
 */