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
using AODL.Document.Content.Text.TextControl;

namespace AODL.Document.Content.Text
{
	/// <summary>
	/// TextBuilder use this class to build TextCollection from
	/// text that contains text control character like whitespaces,
	/// tab stops and line breaks.
	/// </summary>
	public class TextBuilder
	{
		/// <summary>
		/// Builds the text collection.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static ITextCollection BuildTextCollection(IDocument document, string text)
		{
			string xmlStartTag				= "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
			ITextCollection txtCollection	= new ITextCollection();
			text							= WhiteSpaceHelper.GetWhiteSpaceXml(text);
			text							= text.Replace("\t", "<t/>");
			text							= text.Replace("\n", "<n/>");
			xmlStartTag						+= "<txt>"+text+"</txt>";

			XmlDocument xmlDoc				= new XmlDocument();
			xmlDoc.LoadXml(xmlStartTag);

			XmlNode nodeStart				= xmlDoc.DocumentElement;
			if (nodeStart != null)
				if (nodeStart.HasChildNodes)
				{
					foreach(XmlNode childNode in nodeStart.ChildNodes)
					{
						if (childNode.NodeType == XmlNodeType.Text)
							txtCollection.Add(new SimpleText(document, childNode.InnerText));
						else if (childNode.Name == "ws")
						{
							if (childNode.Attributes.Count == 1)
							{
								XmlNode nodeCnt = childNode.Attributes.GetNamedItem("id");
								if (nodeCnt != null)
									txtCollection.Add(new WhiteSpace(document, Convert.ToInt32(nodeCnt.InnerText)));
							}
						}
						else if (childNode.Name == "t")
						{
							txtCollection.Add(new TabStop(document));
						}
						else if (childNode.Name == "n")
						{
							txtCollection.Add(new LineBreak(document));
						}
					}
				}
				else
				{
					txtCollection.Add(new SimpleText(document, text));
				}
			return txtCollection;
		}
	}
}
