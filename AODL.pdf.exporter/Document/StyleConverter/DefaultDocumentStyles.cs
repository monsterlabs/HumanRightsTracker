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
using AODL.Document.TextDocuments;
using AODL.Document;
using iTextSharp.text;
using System.IO;

namespace AODL.ExternalExporter.PDF.Document.StyleConverter
{
	/// <summary>
	/// Summary for DefaultDocumentStyles
	/// </summary>
	/// <remarks>Singleton</remarks>
	public class DefaultDocumentStyles
	{
		/// <summary>
		/// The content document.
		/// </summary>
		private IDocument _document;
		/// <summary>
		/// document styles 
		/// </summary>
		private DocumentStyles _styleDocument;
		/// <summary>
		/// Gets the style document.
		/// </summary>
		/// <value>The style document.</value>
		public DocumentStyles StyleDocument
		{
			get { return this._styleDocument; }
		}

		/// <summary>
		/// default text font
		/// </summary>
		private Font _defaultTextFont;
		/// <summary>
		/// Gets the default text font.
		/// </summary>
		/// <value>The default text font.</value>
		public Font DefaultTextFont
		{
			get { return this._defaultTextFont; }
		}

		/// <summary>
		/// Singleton instance.
		/// </summary>
		private static DefaultDocumentStyles _instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultTextFont"/> class.
		/// </summary>
		private DefaultDocumentStyles()
		{
		}

		/// <summary>
		/// Instances this instance.
		/// </summary>
		/// <returns></returns>
		public static DefaultDocumentStyles Instance(DocumentStyles documentStyles, IDocument document)
		{
			if (_instance == null)
			{
				_instance = new DefaultDocumentStyles();
				_instance._styleDocument = documentStyles;
				_instance._document = document;
			}
			return _instance;
		}

		/// <summary>
		/// Instances this instance.
		/// </summary>
		/// <returns>The instance or null.</returns>
		public static DefaultDocumentStyles Instance()
		{
			return _instance;
		}

		/// <summary>
		/// Invoke necessary methods.
		/// </summary>
		public void Init()
		{
			try
			{
				this.LoadInstalledFonts();
				this.LoadDefaultTextFont();
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Loads the default text font.
		/// </summary>
		private void LoadDefaultTextFont()
		{
			try
			{
				if (this._styleDocument != null && this._styleDocument.Styles != null)
				{
					XmlNode defaultParagraphStyle = this._styleDocument.Styles.SelectSingleNode(
						"//style:default-style[@style:family='paragraph']",
						this._document.NamespaceManager);
					if (defaultParagraphStyle != null)
					{
						XmlNode defaultTextProperties = defaultParagraphStyle.SelectSingleNode("style:text-properties", 
							this._document.NamespaceManager);
						if (defaultTextProperties != null)
						{
							XmlNode fontName = defaultTextProperties.SelectSingleNode("@style:font-name",
								this._document.NamespaceManager);
							if (fontName != null && fontName.InnerText != null)
							{
								if (FontFactory.Contains(fontName.InnerText))
								{
									this._defaultTextFont = FontFactory.GetFont(fontName.InnerText);
								}
								else
								{
									// todo: do it better!
									this._defaultTextFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12.0f);
								}
							}
						}
					}
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Loads the installed fonts.
		/// </summary>
		public void LoadInstalledFonts()
		{
			try
			{
				string windowsFontDir = @"C:\Windows\Fonts\";
				if (System.IO.Directory.Exists(windowsFontDir))
					FontFactory.RegisterDirectory(windowsFontDir);
			}
			catch(Exception)
			{
				// Currently do nothing maybe we are running under a linux system
			}
		}
	}
}
