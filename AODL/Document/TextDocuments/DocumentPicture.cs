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

namespace AODL.Document.TextDocuments
{
	/// <summary>
	/// DocumentPicture represent a picture resp. graphic which used within
	/// a file in the OpenDocument format.
	/// </summary>
	public class DocumentPicture 
	{
		private Image _image;
		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public Image Image
		{
			get { return this._image; }
			set { this._image = value; }
		}

		private string _imageName;
		/// <summary>
		/// Gets or sets the name of the image.
		/// </summary>
		/// <value>The name of the image.</value>
		public string ImageName
		{
			get { return this._imageName; }
			set { this._imageName = value; }
		}

		private string _imagePath;
		/// <summary>
		/// Gets or sets the path of the image.
		/// </summary>
		/// <value>The path of the image.</value>
		public string ImagePath
		{
			get { return this._imagePath; }
			set { this._imagePath = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentPicture"/> class.
		/// </summary>
		public DocumentPicture()
		{			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentPicture"/> class.
		/// </summary>
		/// <param name="file">The file.</param>
		public DocumentPicture(string file)
		{
			try
			{
//				if (!File.Exists(file))
//					throw new Exception("The imagefile "+file+" doesn't exist!");
//				this.Image		= Image.FromFile(file);
				FileInfo fi		= new FileInfo(file);
				this.ImageName	= fi.Name;
				this.ImagePath	= fi.FullName;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
