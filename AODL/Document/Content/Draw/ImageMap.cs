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
using System.IO;
using System.Drawing;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Content.Text;
using AODL.Document.Content;
using AODL.Document;

namespace AODL.Document.Content.Draw
{
	/// <summary>
	/// Summary for ImageMap.
	/// Example ImageMap
	/// </summary>
	/// <example>
	/// &lt;draw:image-map&gt;
	///		&lt;draw:area-rectangle
	///			svg:width="5cm" svg:height="5cm" svg:x="10.949cm" svg:y="5.724cm"&gt;
	///			&lt;office:event-listeners&gt;
	///             &lt;script:event-listener script:language="JavaScript"
	///					script:event-name="dom:onmouseover"
	///					xlink:href="setCursor('hand')"/&gt;
	///			&lt;/office:event-listeners&gt;
	///		&lt;draw:area-rectangle&gt;
	///	&lt;/draw:image-map&gt;
	/// </example>
	public class ImageMap : IContent, IContentContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageMap"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		public ImageMap(IDocument document, XmlNode node)
		{
			this.Document			= document;
			this.InitStandards();
			this.Node				= node;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageMap"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="drawareas">Array of drawareas.</param>
		public ImageMap(IDocument document, DrawArea[] drawareas)
		{
			this.Document			= document;
			this.InitStandards();
			this.NewXmlNode();

			if (drawareas != null)
			{
				foreach (DrawArea d in drawareas)
				{
					_content.Add(d);
				}
			}
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.Content				= new ContentCollection();
			this.Content.Inserted		+= Content_Inserted;
			this.Content.Removed		+= Content_Removed;
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		private void NewXmlNode()
		{			
			this.Node		= this.Document.CreateNode("image-map", "draw");
		}

		/// <summary>
		/// Content_s the inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void Content_Inserted(int index, object value)
		{
			if (this.Node != null)
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

		#region IContentCollection Member

		private ContentCollection _content;
		/// <summary>
		/// Gets or sets the content collection.
		/// </summary>
		/// <value>The content collection.</value>
		public ContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		#endregion 

		#region IContent Member		

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
		/// A draw:area-rectangle doesn't have a style-name.
		/// </summary>
		/// <value>The name</value>
		public string StyleName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>
		/// A draw:area-rectangle doesn't have a style.
		/// </summary>
		public IStyle Style
		{
			get
			{
				return null;
			}
			set
			{
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

		#region IContent Member old

//		private IDocument _document;
//		/// <summary>
//		/// The document to which this image-map is associated.
//		/// </summary>
//		public IDocument Document
//		{
//			get
//			{
//				return this._document;
//			}
//			set
//			{
//				this._document = value;
//			}
//		}
//
//		private XmlNode _node;
//		/// <summary>
//		/// The XmlNode.
//		/// </summary>
//		public XmlNode Node
//		{
//			get
//			{
//				return this._node;
//			}
//			set
//			{
//				this._node = value;
//			}
//		}
//
//		/// <summary>
//		/// An image-map doesn't have a style.
//		/// </summary>
//		/// <value>The name</value>
//		public string Stylename
//		{
//			get
//			{
//				return null;
//			}
//			set
//			{
//			}
//		}
//
//		/// <summary>
//		/// An image-mapp doesn't have a style-name.
//		/// </summary>
//		public IStyle Style
//		{
//			get
//			{
//				return null;
//			}
//			set
//			{
//			}
//		}
//
//		/// <summary>
//		/// An image-map doesn't have text. 
//		/// </summary>
//		public ITextCollection TextContent
//		{
//			get
//			{
//				return null;
//			}
//			set
//			{
//			}
//		}

		#endregion
	
	}
}
