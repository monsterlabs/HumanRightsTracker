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
using AODL.Document .Content ;
using AODL.Document ;
using AODL.Document .Content .Charts ;
using AODL.Document .Content .EmbedObjects ;



namespace AODL.Document.Content.EmbedObjects
{
	/// <summary>
	/// Summary description for EmbedObjectHandler.
	/// </summary>
	public class EmbedObjectHandler
	{
		/// <summary>
		/// the document which contains the embed object
		/// </summary>

		private IDocument _document;

		public IDocument Document
		{
			get
			{
				return this._document ;
			}

			set
			{
				this._document =value;
			}
		}

		/// <summary>
		/// the constructor
		/// </summary>
		/// <param name="document"></param>

		public EmbedObjectHandler(IDocument document)
		{
			this.Document =document;
		}

		/// <summary>
		/// create a embed object
		/// </summary>
		/// <param name="ParentNode"></param>
		/// <param name="MediaType"></param>
		/// <param name="ObjectRealPath"></param>
		/// <returns></returns>

		public EmbedObject CreateEmbedObject(XmlNode ParentNode,string MediaType,string ObjectRealPath,string ObjectName)
		{
			switch(MediaType)
			{
				case "application/vnd.oasis.opendocument.chart":
					return CreateChart(ParentNode,ObjectRealPath,ObjectName);
				case"application/vnd.oasis.opendocument.text":
					return null;
				case "application/vnd.oasis.opendocument.formula":
					return null;
				case "application/vnd.oasis.opendocument.presentation":
					return null;
				default:
					return null;
			}
		}

		/// <summary>
		/// create the chart
		/// </summary>
		/// <param name="ParentNode"></param>
		/// <param name="ObjectRealPath"></param>
		/// <returns></returns>

		public  Chart CreateChart(XmlNode ParentNode, string ObjectRealPath,string ObjectName)
		{
			Chart chart                 = new Chart (this._document ,null,ParentNode);

			chart.ObjectType            = "chart";

			chart.ObjectName            = ObjectName;

			chart.ObjectRealPath        = ObjectRealPath;

			ChartImporter chartimporter = new ChartImporter (chart);

			chartimporter.Import ();

			return chart;
		}

	}
}

