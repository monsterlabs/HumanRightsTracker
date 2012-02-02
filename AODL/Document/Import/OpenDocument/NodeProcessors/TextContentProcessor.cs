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
using System.Diagnostics;
using System.Xml;
using System.Text.RegularExpressions;
using AODL.Document;
using AODL.Document.Styles;
using AODL.Document.Exceptions;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Text.TextControl;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	/// <summary>
	/// Represent a Text Content Processor.
	/// </summary>
	public class TextContentProcessor
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextContentProcessor"/> class.
		/// </summary>
		public TextContentProcessor()
		{
		}

		/// <summary>
		/// Creates the text object.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="aTextNode">A text node.</param>
		/// <returns></returns>
		public IText CreateTextObject(IDocument document, XmlNode aTextNode)
		{
			//aTextNode.InnerText				= this.ReplaceSpecialCharacter(aTextNode.InnerText);
			int i=0;
			if (aTextNode.OuterXml.IndexOf("Contains state ") > -1)
				i++;

			switch(aTextNode.Name)
			{
				case "#text":
					return new SimpleText(document, aTextNode.InnerText);
				case "text:span":
					return CreateFormatedText(document, aTextNode);
				case "text:bookmark":
					return CreateBookmark(document, aTextNode , BookmarkType.Standard);
				case "text:bookmark-start":
					return CreateBookmark(document, aTextNode , BookmarkType.Start);
				case "text:bookmark-end":
					return CreateBookmark(document, aTextNode , BookmarkType.End);
				case "text:a":
					return CreateXLink(document, aTextNode);
				case "text:note":
					return CreateFootnote(document, aTextNode);
				case "text:line-break":
					return new LineBreak(document);
				case "text:s":
					return new WhiteSpace(document, aTextNode.CloneNode(true));
				case "text:tab":
					return new TabStop(document);
				default:
					return null;
			}
		}
		
		/// <summary>
		/// Creates the formated text.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public FormatedText CreateFormatedText(IDocument document, XmlNode node)
		{
			//Create a new FormatedText object
			FormatedText formatedText		= new FormatedText(document, node);
			ITextCollection iTextColl		= new ITextCollection();
			formatedText.Document			= document;
			formatedText.Node				= node;
			//Recieve a TextStyle
			
			IStyle textStyle				= document.Styles.GetStyleByName(formatedText.StyleName);

			if (textStyle != null)
				formatedText.Style			= textStyle;
			//else
			//{
			//	IStyle iStyle				= document.CommonStyles.GetStyleByName(formatedText.StyleName);
			//}
			
			//Ceck for more IText object
			foreach(XmlNode iTextNode in node.ChildNodes)
			{
				IText iText						= this.CreateTextObject(document, iTextNode.CloneNode(true));
				if (iText != null)
				{
					iTextColl.Add(iText);
				}
				else
					iTextColl.Add(new UnknownTextContent(document, iTextNode) as IText);
			}

			formatedText.Node.InnerText			= "";

			foreach(IText iText in iTextColl)
				formatedText.TextContent.Add(iText);

			return formatedText;
		}
		
		/// <summary>
		/// Creates the bookmark.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public Bookmark CreateBookmark(IDocument document,XmlNode node, BookmarkType type)
		{
			try
			{
				Bookmark bookmark		= null;
				if (type == BookmarkType.Standard)
					bookmark			= new Bookmark(document, BookmarkType.Standard, "noname");
				else if (type == BookmarkType.Start)
					bookmark			= new Bookmark(document, BookmarkType.Start, "noname");
				else
					bookmark			= new Bookmark(document, BookmarkType.End, "noname");

				bookmark.Node			= node.CloneNode(true);

				return bookmark;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Bookmark.", ex);
			}
		}

		/// <summary>
		/// Creates the X link.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public XLink CreateXLink(IDocument document, XmlNode node)
		{
			try
			{
				XLink xlink				= new XLink(document);
				xlink.Node				= node.CloneNode(true);
				ITextCollection iTxtCol	= new ITextCollection();

				foreach(XmlNode nodeText in xlink.Node.ChildNodes)
				{
					IText iText			= this.CreateTextObject(xlink.Document, nodeText);
					if (iText != null)
						iTxtCol.Add(iText);
				}

				xlink.Node.InnerXml		= "";

				foreach(IText iText in iTxtCol)
					xlink.TextContent.Add(iText);

				return xlink;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a XLink.", ex);
			}
		}

		/// <summary>
		/// Creates the footnote.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public Footnote CreateFootnote(IDocument document,XmlNode node)
		{
			try
			{
				Footnote fnote			= new Footnote(document);
				fnote.Node				= node.CloneNode(true);

				return fnote;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a Footnote.", ex);
			}
		}

		/// <summary>
		/// Creates the text sequence.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		public TextSequence CreateTextSequence(IDocument document, XmlNode node)
		{
			try
			{
				TextSequence textSequence	= new TextSequence(document, node);

				return textSequence;
			}
			catch(Exception ex)
			{
				throw new AODLException("Exception while trying to create a TextSequence.", ex);
			}
		}

		/// <summary>
		/// Replaces the special character.
		/// </summary>
		/// <param name="nodeInnerText">The node inner text.</param>
		/// <returns></returns>
		private string ReplaceSpecialCharacter(string nodeInnerText)
		{
			nodeInnerText					= nodeInnerText.Replace("<", "&lt;");
			nodeInnerText					= nodeInnerText.Replace(">", "&gt;");

			return nodeInnerText;
		}
	}
}
