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
using AODL.Document .Styles .Properties ;
using AODL.Document .Collections ;
using AODL.Document .Content .Charts ;

namespace AODL.Document.Styles
{
	/// <summary>
	/// Summary description for ChartStyles.
	/// </summary>
	public class ChartStyles
	{
		public ChartStyles(Chart chart)
		{
			this.Chart     = chart;
			this.Styles    = new XmlDocument ();

		}

		public static readonly string FileName		= "styles.xml";
		/// <summary>
		/// XPath to the document office styles
		/// </summary>
		//private static readonly string OfficeStyles	= "/office:document-style/office:styles";

		private XmlDocument _styles;
		/// <summary>
		/// Gets or sets the chart styles from the styles.xml file.
		/// </summary>
		/// <value>The styles.</value>
		public XmlDocument Styles
		{
			get { return this._styles; }
			set { this._styles = value; }
		}

		private Chart _chart;

		public Chart Chart
		{
			get    
			{ 
				return this._chart;
			}
			set    
			{
				this._chart=value;
			}
		}
	}
}

