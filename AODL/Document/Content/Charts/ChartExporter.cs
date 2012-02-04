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
using System.Xml ;
using System.Diagnostics ;
using System.Collections ;
using System.IO ;
using ICSharpCode.SharpZipLib ;
using ICSharpCode.SharpZipLib .GZip;
using ICSharpCode.SharpZipLib .Checksums;
using ICSharpCode.SharpZipLib .Zip;
using ICSharpCode.SharpZipLib .BZip2 ;
using AODL.Document .Content.EmbedObjects;
using AODL.Document .SpreadsheetDocuments ;
using AODL.Document .Styles ;

namespace AODL.Document.Content.Charts
{
	/// <summary>
	/// Summary description for OpenDocumentTextExporter.
	/// </summary>
	public class ChartExporter
	{
		private string[] _directories			= {"ObjectReplacements"};
		private IDocument _document				= null;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentTextExporter"/> class.
		/// </summary>
		public ChartExporter()
		{

		}

		#region IExporter Member

		/// <summary>
		/// Exports the specified document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		public void Export(IDocument document,string dir)
		{
			try
			{
				this._document			= document;
				PrepareDirectory(dir);
				
				foreach ( EmbedObject eo in document.EmbedObjects)
				{
					if (eo.ObjectType.Equals("chart"))
					{
						this.WriteSingleFiles(((Chart)eo).ChartStyles.Styles,dir+eo.ObjectName+"\\"+ChartStyles.FileName);
						this.WriteSingleFiles(((Chart)eo).ChartDoc,dir+eo.ObjectName+"\\"+"content.xml");
						this.WriteFileEntry( ((Chart)eo).ObjectName );

					}
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Writes the single files.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="filename">The filename.</param>
		private void WriteSingleFiles(System.Xml.XmlDocument document, string filename)
		{
			try
			{
				//document.Save(filename);
				XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
				writer.Formatting = Formatting.None;
				document.WriteContentTo( writer );
				writer.Flush();
				writer.Close();
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion

		/// <summary>
		/// Create an output directory with all necessary subfolders.
		/// </summary>
		/// <param name="directory">The directory.</param>
		private void PrepareDirectory(string directory)
		{
			foreach(EmbedObject eo in this._document.EmbedObjects)
				if (eo.ObjectType.Equals("chart"))
				Directory.CreateDirectory(Path.Combine(directory,eo.ObjectName));

			foreach(string d in this._directories)
				Directory.CreateDirectory(directory+@"\"+d);
		}

		private void WriteFileEntry(string objectName)
		{
			XmlNode  manifest = ((SpreadsheetDocument)this._document).DocumentManifest .Manifest .SelectSingleNode ("manifest:manifest",this._document.NamespaceManager );
			
			XmlNode  node =((SpreadsheetDocument)this._document).CreateNode("file-entry","manifest");
			XmlAttribute xa = this._document.CreateAttribute ("media-type","manifest");
			xa.Value ="text/xml";
			node.Attributes .Append (xa);

			xa = this._document.CreateAttribute ("full-path","manifest");
			xa.Value = Path.Combine (objectName, "content.xml");
			node.Attributes .Append (xa);
			
			node  = ((SpreadsheetDocument)this._document).DocumentManifest .Manifest.ImportNode (node,true);
			manifest.AppendChild (node);

			node = this._document .CreateNode ("file-entry","manifest");
			
			xa = this._document.CreateAttribute ("media-type","manifest");
			xa.Value ="text/xml";
			node.Attributes .Append (xa);

			xa = this._document.CreateAttribute ("full-path","manifest");
			xa.Value = Path.Combine (objectName, "styles.xml");
			node.Attributes .Append (xa);

			node  = ((SpreadsheetDocument)this._document).DocumentManifest .Manifest.ImportNode (node,true);
			manifest.AppendChild (node);

			node = this._document .CreateNode ("file-entry","manifest");
			
			xa = this._document.CreateAttribute ("media-type","manifest");
			xa.Value ="application/vnd.oasis.opendocument.chart";
			node.Attributes .Append (xa);

			xa = this._document.CreateAttribute ("full-path","manifest");
			xa.Value =objectName+@"/";
			node.Attributes .Append (xa);

			node  = ((SpreadsheetDocument)this._document).DocumentManifest .Manifest.ImportNode (node,true);
			manifest.AppendChild (node);
		}
	}
	
}

