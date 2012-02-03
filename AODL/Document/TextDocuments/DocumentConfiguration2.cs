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

namespace AODL.Document.TextDocuments
{
	/// <summary>
	/// DocumentConfiguration2 represent the Configuration2 file.
	/// </summary>
	public class DocumentConfiguration2
	{
		/// <summary>
		/// The folder name.
		/// </summary>
		public static readonly string FolderName = "Configurations2";

		private string _filename;
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get { return this._filename; }
			set { this._filename = value; }
		}

		private string _configurations2Content;
		/// <summary>
		/// Gets or sets the content of the configurations2.
		/// </summary>
		/// <value>The content of the configurations2.</value>
		public string Configurations2Content
		{
			get { return this._configurations2Content; }
			set { this._configurations2Content = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentConfiguration2"/> class.
		/// </summary>
		public DocumentConfiguration2()
		{
		}

		/// <summary>
		/// Loads the specified filename.
		/// </summary>
		/// <param name="filename">The filename.</param>
		public void Load(string filename)
		{
			StreamReader sr		= new StreamReader(filename);
			string line			= null;
			while((line = sr.ReadLine()) != null)
			{
				this.Configurations2Content	+= line;
			}
			sr.Close();
		}
	}
}

/*
 * $Log: DocumentConfiguration2.cs,v $
 * Revision 1.2  2008/04/29 15:39:56  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:57  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.2  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.1  2006/01/29 11:28:30  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.3  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */