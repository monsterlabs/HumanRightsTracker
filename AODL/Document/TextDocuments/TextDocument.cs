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
using System.Reflection;
using System.IO;
using System.Xml;
using AODL.Document.Export;
using AODL.Document.Import;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Exceptions;
using AODL.Document;
using AODL.Document.Styles;
using AODL.Document.Styles.MasterStyles;
using AODL.Document.Content;
using AODL.Document.Content.Tables;
using AODL.Document.Forms;
using AODL.Document.Forms.Controls;
using AODL.Document.Content.Fields;
using AODL.Document .Content .EmbedObjects ;

namespace AODL.Document.TextDocuments
{
	/// <summary>
	/// Represent a opendocument text document.
	/// </summary>
	/// <example>
	/// <code>
	/// TextDocument td = new TextDocument();
	/// td.New();
	/// Paragraph p = new Paragraph(td, "P1");
	/// //add text
	/// p.TextContent.Add(new SimpleText(p, "Hello"));
	/// //Add the Paragraph
	/// td.Content.Add((IContent)p);
	/// //Blank para
	/// td.Content.Add(new Paragraph(td, ParentStyles.Standard.ToString()));
	/// // new para
	/// p = new Paragraph(td, "P2");
	/// p.TextContent.Add(new SimpleText(p, "Hello again"));
	/// td.Content.Add(p);
	/// td.SaveTo("parablank.odt");
	/// </code>
	/// </example>
	public class TextDocument : IDisposable, IDocument //AODL.Document.TextDocuments.Content.IContentContainer,
	{
		private DirInfo m_dirInfo = null;
		private StyleFactory m_styleFactory = null;
		
		private IImporter m_importer = null;
		
		public DirInfo DirInfo {
			get { return m_dirInfo; }
		}
		
		public StyleFactory StyleFactory {
			get { return m_styleFactory; }
		}
		
		
		private int _tableOfContentsCount			= 0;
		/// <summary>
		/// Gets the tableof contents count.
		/// </summary>
		/// <value>The tableof contents count.</value>
		public int TableofContentsCount
		{
			get { return this._tableOfContentsCount; }
		}

		private int _tableCount						= 0;
		/// <summary>
		/// Gets the tableof contents count.
		/// </summary>
		/// <value>The tableof contents count.</value>
		public int TableCount
		{
			get { return this._tableCount; }
		}

		private XmlDocument _xmldoc;
		/// <summary>
		/// The xmldocument the textdocument based on.
		/// </summary>
		public XmlDocument XmlDoc
		{
			get { return this._xmldoc; }
			set { this._xmldoc = value; }
		}

		private bool _isLoadedFile;
		/// <summary>
		/// If this file was loaded
		/// </summary>
		/// <value></value>
		public bool IsLoadedFile
		{
			get { return this._isLoadedFile; }
		}

		private ArrayList _graphics;
		/// <summary>
		/// Gets the graphics.
		/// </summary>
		/// <value>The graphics.</value>
		public ArrayList Graphics
		{
			get { return this._graphics; }
		}

		private DocumentStyles _documentStyles;
		/// <summary>
		/// Gets or sets the document styes.
		/// </summary>
		/// <value>The document styes.</value>
		public DocumentStyles DocumentStyles
		{
			get { return this._documentStyles; }
			set { this._documentStyles = value; }
		}

		private DocumentSetting _documentSetting;
		/// <summary>
		/// Gets or sets the document setting.
		/// </summary>
		/// <value>The document setting.</value>
		public DocumentSetting DocumentSetting
		{
			get { return this._documentSetting; }
			set { this._documentSetting = value; }
		}

		private DocumentMetadata _documentMetadata;
		/// <summary>
		/// Gets or sets the document metadata.
		/// </summary>
		/// <value>The document metadata.</value>
		public DocumentMetadata DocumentMetadata
		{
			get { return this._documentMetadata; }
			set { this._documentMetadata = value; }
		}

		private ODFFormCollection _formCollection;

		public ODFFormCollection Forms
		{
			get { return this._formCollection; }
			set { this._formCollection = value; }
		}

		private DocumentManifest _documentManifest;
		/// <summary>
		/// Gets or sets the document manifest.
		/// </summary>
		/// <value>The document manifest.</value>
		public DocumentManifest DocumentManifest
		{
			get { return this._documentManifest; }
			set { this._documentManifest = value; }
		}

		private DocumentConfiguration2 _documentConfigurations2;
		/// <summary>
		/// Gets or sets the document configurations2.
		/// </summary>
		/// <value>The document configurations2.</value>
		public DocumentConfiguration2 DocumentConfigurations2
		{
			get { return this._documentConfigurations2; }
			set { this._documentConfigurations2 = value; }
		}

		private DocumentPictureCollection _documentPictures;
		/// <summary>
		/// Gets or sets the document pictures.
		/// </summary>
		/// <value>The document pictures.</value>
		public DocumentPictureCollection DocumentPictures
		{
			get { return this._documentPictures; }
			set { this._documentPictures = value; }
		}

		private DocumentPictureCollection _documentThumbnails;
		/// <summary>
		/// Gets or sets the document thumbnails.
		/// </summary>
		/// <value>The document thumbnails.</value>
		public DocumentPictureCollection DocumentThumbnails
		{
			get { return this._documentThumbnails; }
			set { this._documentThumbnails = value; }
		}

		private string _mimeTyp		= "application/vnd.oasis.opendocument.text";
		/// <summary>
		/// Gets the MIME typ.
		/// </summary>
		/// <value>The MIME typ.</value>
		public string MimeTyp
		{
			get { return this._mimeTyp; }
		}


		/// <summary>
		/// gets and sets the embed objects
		/// </summary>

		private ArrayList _embedobjects;
		/// <summary>
		/// Gets the graphics.
		/// </summary>
		/// <value>The graphics.</value>
		public ArrayList EmbedObjects
		{
			get { return this._embedobjects; }

			set { this._embedobjects =value; }
		}

		private XmlNamespaceManager _namespacemanager;
		/// <summary>
		/// The namespacemanager to access the documents namespace uris.
		/// </summary>
		public XmlNamespaceManager NamespaceManager
		{
			get { return this._namespacemanager; }
			set { this._namespacemanager = value; }
		}

		private AODL.Document.Content.ContentCollection _content;
		/// <summary>
		/// Collection of contents used by this document.
		/// </summary>
		/// <value></value>
		public AODL.Document.Content.ContentCollection Content
		{
			get { return this._content; }
			set { this._content = value; }
		}

		private AODL.Document.Styles.StyleCollection _styles;
		/// <summary>
		/// Collection of local styles used with this document.
		/// </summary>
		/// <value></value>
		public AODL.Document.Styles.StyleCollection Styles
		{
			get { return this._styles; }
			set { this._styles = value; }
		}

		private StyleCollection _commonStyles;
		/// <summary>
		/// Collection of common styles used with this document.
		/// </summary>
		/// <value></value>
		public StyleCollection CommonStyles
		{
			get { return this._commonStyles; }
			set { this._commonStyles = value; }
		}

		private TextMasterPageCollection _textMasterPageCollection;
		/// <summary>
		/// Gets or sets the text master page collection.
		/// </summary>
		/// <value>The text master page collection.</value>
		public TextMasterPageCollection TextMasterPageCollection
		{
			get { return this._textMasterPageCollection; }
			set { this._textMasterPageCollection = value; }
		}

		private ArrayList _fontList;
		/// <summary>
		/// Gets or sets the font list.
		/// </summary>
		/// <value>The font list.</value>
		public ArrayList FontList
		{
			get { return this._fontList; }
			set { this._fontList = value; }
		}

		/// <summary>
		/// Create a new TextDocument object.
		/// </summary>
		public TextDocument()
		{
			this._fields = new FieldsCollection();
			this.Content			= new ContentCollection();
			this.Styles				= new StyleCollection();
			this.m_styleFactory       = new StyleFactory(this);
			this.CommonStyles		= new StyleCollection();
			this.FontList			= new ArrayList();
			this._graphics			= new ArrayList();
			
			this._formCollection = new ODFFormCollection();
			this._formCollection.Clearing += FormsCollection_Clear;
			this._formCollection.Removed += FormsCollection_Removed;

			this.VariableDeclarations = new VariableDeclCollection();
		}

		/// <summary>
		/// Create a blank new document.
		/// </summary>
		public TextDocument New()
		{
			this._xmldoc					= new XmlDocument();
			this.Styles = new StyleCollection();
			this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());
			this.NamespaceManager			= TextDocumentHelper.NameSpace(this._xmldoc.NameTable);

			this.DocumentConfigurations2	= new DocumentConfiguration2();

			this.DocumentManifest			= new DocumentManifest();
			this.DocumentManifest.New();

			this.DocumentMetadata			= new DocumentMetadata(this);
			this.DocumentMetadata.New();

			this.DocumentPictures			= new DocumentPictureCollection();

			this.DocumentSetting			= new DocumentSetting();
			this.DocumentSetting.New();

			this.DocumentStyles				= new DocumentStyles();
			this.DocumentStyles.New(this);
			this.ReadCommonStyles();

			this.Forms = new ODFFormCollection();
			this._formCollection.Clearing += FormsCollection_Clear;
			this._formCollection.Removed += FormsCollection_Removed;

			this.Fields.Clear();
			this.Content.Clear();
			
			
			this.VariableDeclarations = new VariableDeclCollection();
			
			this.DocumentThumbnails			= new DocumentPictureCollection();

			MasterPageFactory.RenameMasterStyles(
				this.DocumentStyles.Styles,
				this.XmlDoc, this.NamespaceManager);
			// Read the moved and renamed styles
			LocalStyleProcessor lsp = new LocalStyleProcessor(this, false);
			lsp.ReReadKnownAutomaticStyles();
			new MasterPageFactory(m_dirInfo).FillFromXMLDocument(this);
			return this;
		}

		/// <summary>
		/// Reads the common styles.
		/// </summary>
		private void ReadCommonStyles()
		{
			OpenDocumentImporter odImporter	= new OpenDocumentImporter();
			odImporter.Document			= this;
			odImporter.ImportCommonStyles();

			LocalStyleProcessor lsp			= new LocalStyleProcessor(this, true);
			lsp.ReadStyles();
		}

		public void DeleteUnpackedFiles()
		{
			if (m_importer != null)
				m_importer.DeleteUnpackedFiles();
		}
		
		/// <summary>
		/// Loads the document by using the specified importer.
		/// </summary>
		/// <param name="file">The the file.</param>
		public void Load(string file)
		{
			this._isLoadedFile				= true;

			this.Styles = new StyleCollection();
			this._fields = new FieldsCollection();
			this.Content = new ContentCollection();
			

			this._xmldoc					= new XmlDocument();
			this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());
			this.NamespaceManager			= TextDocumentHelper.NameSpace(this._xmldoc.NameTable);

			ImportHandler importHandler		= new ImportHandler();
			m_importer				= importHandler.GetFirstImporter(DocumentTypes.TextDocument, file);
			m_dirInfo = m_importer.DirInfo;
			if (m_importer != null)
			{
				if (m_importer.NeedNewOpenDocument)
					this.New();
				m_importer.Import(this,file);

				if (m_importer.ImportError != null)
					if (m_importer.ImportError.Count > 0)
					foreach(object ob in m_importer.ImportError)
					if (ob is AODLWarning)
				{
					if (((AODLWarning)ob).Message != null)
						Console.WriteLine("Err: {0}", ((AODLWarning)ob).Message);
					if (((AODLWarning)ob).Node != null)
					{
						XmlTextWriter writer	= new XmlTextWriter(Console.Out);
						writer.Formatting		= Formatting.Indented;
						((AODLWarning)ob).Node.WriteContentTo(writer);
					}
				}
			}
			this._formCollection.Clearing += FormsCollection_Clear;
			this._formCollection.Removed += FormsCollection_Removed;
		}

		/// <summary>
		/// Create a new XmlNode for this document.
		/// </summary>
		/// <param name="name">The elementname.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The new XmlNode.</returns>
		public XmlNode CreateNode(string name, string prefix)
		{
			if (this.XmlDoc == null)
				throw new ArgumentNullException(string.Format(
					"There is no XmlDocument loaded"));
			string nuri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateElement(prefix, name, nuri);
		}

		/// <summary>
		/// Create a new XmlAttribute for this document.
		/// </summary>
		/// <param name="name">The attributename.</param>
		/// <param name="prefix">The prefixname.</param>
		/// <returns>The new XmlAttribute.</returns>
		public XmlAttribute CreateAttribute(string name, string prefix)
		{
			if (this.XmlDoc == null)
				throw new NullReferenceException("There is no XmlDocument loaded. Couldn't create Attribue "+name+" with Prefix "+prefix+". "+this.GetType().ToString());
			string nuri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateAttribute(prefix, name, nuri);
		}

		/// <summary>
		/// Save the TextDocument as OpenDocument textdocument.
		/// </summary>
		/// <param name="filename">The filename. With or without full path. Without will save the file to application path!</param>
		public void SaveTo(string filename)
		{
			try
			{
				//Build document first
				foreach(string font in this.FontList)
					this.AddFont(font);
				this.CreateContentBody();

				//Call the ExportHandler for the first matching IExporter
				ExportHandler exportHandler		= new ExportHandler();
				IExporter iExporter				= exportHandler.GetFirstExporter(
					DocumentTypes.TextDocument, filename);
				iExporter.Export(this, filename);
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Save the document by using the passed IExporter
		/// with the passed file name.
		/// </summary>
		/// <param name="filename">The name of the new file.</param>
		/// <param name="iExporter"></param>
		public void SaveTo(string filename, IExporter iExporter)
		{
			try
			{
				this.CreateContentBody();
				iExporter.Export(this, filename);
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Adds a font to the document. All fonts that you use
		/// within your text must be added to the document.
		/// The class FontFamilies represent all available fonts.
		/// </summary>
		/// <param name="fontname">The fontname take it from class FontFamilies.</param>
		private void AddFont(string fontname)
		{
			try
			{
				Assembly ass		= Assembly.GetExecutingAssembly();
				Stream stream		= ass.GetManifestResourceStream("AODL.Resources.OD.fonts.xml");

				XmlDocument fontdoc	= new XmlDocument();
				fontdoc.Load(stream);

				XmlNode exfontnode	= this.XmlDoc.SelectSingleNode(
					"/office:document-content/office:font-face-decls/style:font-face[@style:name='"+fontname+"']", this.NamespaceManager);

				if (exfontnode != null)
					return; //Font exist;

				XmlNode newfontnode	= fontdoc.SelectSingleNode(
					"/office:document-content/office:font-face-decls/style:font-face[@style:name='"+fontname+"']", this.NamespaceManager);

				if (newfontnode != null)
				{
					XmlNode fontsnode	= this.XmlDoc.SelectSingleNode(
						"/office:document-content", this.NamespaceManager);
					if (fontsnode != null)
					{
						foreach(XmlNode xn in fontsnode)
							if (xn.Name == "office:font-face-decls")
						{
							XmlNode node		= this.CreateNode("font-face", "style");
							foreach(XmlAttribute xa in newfontnode.Attributes)
							{
								XmlAttribute xanew	= this.CreateAttribute(xa.LocalName, xa.Prefix);
								xanew.Value			= xa.Value;
								node.Attributes.Append(xanew);
							}
							xn.AppendChild(node);
							break;
						}
					}
				}
			}
			catch(Exception)
			{
				//Should never happen
				throw;
			}
		}

		/// <summary>
		/// Return the namespaceuri for the given prefixname.
		/// </summary>
		/// <param name="prefix">The prefixname.</param>
		/// <returns></returns>
		public string GetNamespaceUri(string prefix)
		{
			foreach (string prefixx in NamespaceManager)
				if (prefix == prefixx)
				return NamespaceManager.LookupNamespace(prefixx);
			return null;
		}

		/// <summary>
		/// Creates the content body.
		/// </summary>
		private void CreateContentBody()
		{
			XmlNode nodeText		= this.XmlDoc.SelectSingleNode(
				TextDocumentHelper.OfficeTextPath, this.NamespaceManager);
			
			if (Forms.Count != 0)
			{
				XmlNode nodeForms = nodeText.SelectSingleNode("office:forms", this.NamespaceManager);
				if (nodeForms == null)
				{
					nodeForms = this.CreateNode("forms","office");
				}
				
				foreach (ODFForm f in Forms)
				{
					nodeForms.AppendChild(f.Node);
				}
				nodeText.AppendChild(nodeForms);
			}

			if (_varDecls.Count != 0)
			{
				XmlNode nodeVarDecls = nodeText.SelectSingleNode("text:variable-decls", this.NamespaceManager);
				if (nodeVarDecls == null)
				{
					nodeVarDecls = this.CreateNode("variable-decls","text");
				}
				
				foreach (VariableDecl vd in _varDecls)
				{
					nodeVarDecls.AppendChild(this.XmlDoc.ImportNode(vd.Node, true));
				}

				nodeText.AppendChild(nodeVarDecls);
			}
			
			foreach(IContent content in this.Content)
			{
				if (content is Table)
					((Table)content).BuildNode();
				nodeText.AppendChild(content.Node);
			}
			
			
			this.CreateLocalStyleContent();
			this.CreateCommonStyleContent();
		}

		/// <summary>
		/// Creates the content of the local style.
		/// </summary>
		private void CreateLocalStyleContent()
		{
			XmlNode nodeAutomaticStyles		= this.XmlDoc.SelectSingleNode(
				TextDocumentHelper.AutomaticStylePath, this.NamespaceManager);

			foreach(IStyle style in this.Styles.ToValueList())
			{
				bool exist					= false;
				if (style.StyleName != null)
				{
					XmlNode node				= nodeAutomaticStyles.SelectSingleNode("style:style[@style:name='"+style.StyleName+"']",
					                                                       this.NamespaceManager);
					if (node != null)
						exist				= true;
				}
				if (!exist)
					nodeAutomaticStyles.AppendChild(style.Node);
			}
		}

		/// <summary>
		/// Creates the content of the common style.
		/// </summary>
		private void CreateCommonStyleContent()
		{
			XmlNode nodeCommonStyles		= this.DocumentStyles.Styles.SelectSingleNode(
				"office:document-styles/office:styles", this.NamespaceManager);
			nodeCommonStyles.InnerXml	= "";

			foreach(IStyle style in this.CommonStyles.ToValueList())
			{
				XmlNode nodeStyle			= this.DocumentStyles.Styles.ImportNode(style.Node, true);
				nodeCommonStyles.AppendChild(nodeStyle);
			}

			//Remove styles node
			nodeCommonStyles				= this.XmlDoc.SelectSingleNode(
				"office:document-content/office:styles", this.NamespaceManager);
			if (nodeCommonStyles != null)
				this.XmlDoc.SelectSingleNode(
					"office:document-content", this.NamespaceManager).RemoveChild(nodeCommonStyles);
		}

		/// <summary>
		/// Looks for a specific control through all the forms by its ID
		/// </summary>
		/// <param name="id">Control ID</param>
		/// <returns>The control</returns>
		public ODFFormControl FindControlById(string id)
		{
			foreach (ODFForm f in Forms)
			{
				ODFFormControl fc = f.FindControlById(id, true);
				if (fc !=null)
					return fc;
			}
			return null;
		}

		/// <summary>
		/// Looks for a specific control through all the forms by its name
		/// </summary>
		/// <param name="id">Control name</param>
		/// <returns>The control</returns>
		public ODFFormControl FindControlByName(string name)
		{
			foreach (ODFForm f in Forms)
			{
				ODFFormControl fc = f.FindControlByName(name, true);
				if (fc !=null)
					return fc;
			}
			return null;
		}

		/// <summary>
		/// Adds new form to the forms collection
		/// </summary>
		/// <param name="name">Form name</param>
		/// <returns></returns>
		public ODFForm AddNewForm(string name)
		{
			ODFForm f = new ODFForm(this, name);
			Forms.Add(f);
			return f;
		}

		private void FormsCollection_Clear()
		{
			for (int i=0; i< _formCollection.Count; i++)
			{
				ODFForm f = _formCollection[i];
				f.Controls.Clear();
			}
		}
		
		private void FormsCollection_Removed(int index, object value)
		{
			ODFForm f = value as ODFForm;
			f.Controls.Clear();
		}

		private FieldsCollection _fields;

		public FieldsCollection Fields
		{
			get
			{
				return _fields;
			}
			set
			{
				if (value != _fields)
				{
					throw new Exception("Cannot assign a new value to Fields property!");
				}
				value = _fields;
			}
		}

		private VariableDeclCollection _varDecls;

		public VariableDeclCollection VariableDeclarations
		{
			get
			{
				return _varDecls;
			}
			set
			{
				_varDecls = value;
			}
		}

		public EmbedObject  GetObjectByName(string ObjectName)
		{
			for(int i=0; i<EmbedObjects.Count ; i++)
			{
				if (((EmbedObject)EmbedObjects[i]).ObjectName==ObjectName)

					return (EmbedObject)EmbedObjects[i];

			}

			return null;
		}


		#region IDisposable Member

		private bool _disposed = false;

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// is reclaimed by garbage collection.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					try
					{
						DeleteUnpackedFiles();
					}
					catch(Exception ex)
					{
						throw ex;
					}
				}
			}
			_disposed = true;
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="AODL.Document.TextDocuments.TextDocument"/> is reclaimed by garbage collection.
		/// </summary>
		~TextDocument()
		{
			Dispose();
		}

		#endregion
	}
}

/*
 * $Log: TextDocument.cs,v $
 * Revision 1.10  2008/04/29 15:39:57  mt
 * new copyright header
 *
 * Revision 1.9  2008/02/08 07:12:21  larsbehr
 * - added initial chart support
 * - several bug fixes
 *
 * Revision 1.8  2007/07/15 09:30:28  yegorov
 * Issue number:
 * Submitted by:
 * Reviewed by:
 *
 * Revision 1.5  2007/06/20 17:37:19  yegorov
 * Issue number:
 * Submitted by:
 * Reviewed by:
 *
 * Revision 1.2  2007/04/08 16:51:24  larsbehr
 * - finished master pages and styles for text documents
 * - several bug fixes
 *
 * Revision 1.1  2007/02/25 08:58:59  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.7  2007/02/13 17:58:49  larsbm
 * - add first part of implementation of master style pages
 * - pdf exporter conversations for tables and images and added measurement helper
 *
 * Revision 1.6  2007/02/04 22:52:58  larsbm
 * - fixed bug in resize algorithm for rows and cells
 * - extending IDocument, overload SaveTo to accept external exporter impl.
 * - initial version of AODL PDF exporter add on
 *
 * Revision 1.5  2006/02/21 19:34:56  larsbm
 * - Fixed Bug text that contains a xml tag will be imported  as UnknowText and not correct displayed if document is exported  as HTML.
 * - Fixed Bug [ 1436080 ] Common styles
 *
 * Revision 1.4  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:51  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:30  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.18  2006/01/05 10:31:10  larsbm
 * - AODL merged cells
 * - AODL toc
 * - AODC batch mode, splash screen
 *
 * Revision 1.17  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.16  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.15  2005/11/23 19:18:17  larsbm
 * - New Textproperties
 * - New Paragraphproperties
 * - New Border Helper
 * - Textproprtie helper
 *
 * Revision 1.14  2005/11/22 21:09:19  larsbm
 * - Add simple header and footer support
 *
 * Revision 1.13  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.12  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 * Revision 1.11  2005/10/23 16:47:48  larsbm
 * - Bugfix ListItem throws IStyleInterface not implemented exeption
 * - now. build the document after call saveto instead prepare the do at runtime
 * - add remove support for IText objects in the paragraph class
 *
 * Revision 1.10  2005/10/23 09:17:20  larsbm
 * - Release 1.0.3.0
 *
 * Revision 1.9  2005/10/22 15:52:10  larsbm
 * - Changed some styles from Enum to Class with statics
 * - Add full support for available OpenOffice fonts
 *
 * Revision 1.8  2005/10/22 10:47:41  larsbm
 * - add graphic support
 *
 * Revision 1.7  2005/10/16 08:36:29  larsbm
 * - Fixed bug [ 1327809 ] Invalid Cast Exception while insert table with cells that contains lists
 * - Fixed bug [ 1327820 ] Cell styles run into loop
 *
 * Revision 1.6  2005/10/15 12:13:20  larsbm
 * - fixed bug in add pargraph to cell
 *
 * Revision 1.5  2005/10/15 11:40:31  larsbm
 * - finished first step for table support
 *
 * Revision 1.4  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 * Revision 1.3  2005/10/08 12:31:33  larsbm
 * - better usabilty of paragraph handling
 * - create paragraphs with text and blank paragraphs with one line of code
 *
 * Revision 1.2  2005/10/08 07:50:15  larsbm
 * - added cvs tags
 *
 */
