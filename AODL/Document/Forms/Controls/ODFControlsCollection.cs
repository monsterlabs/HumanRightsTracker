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
using AODL.Document.Collections;
using AODL.Document.Forms.Controls;
using AODL.Document.Forms;

namespace AODL.Document.Forms.Controls
{
	/// <summary>
	/// Summary description for ODFControlsCollection.
	/// </summary>
	public class ODFControlsCollection: CollectionWithEvents<ODFFormControl>
	{
		/// <summary>
		/// Looks up a specific control by its id
		/// </summary>
		/// <param name="id">Control ID</param>
		/// <returns></returns>
		public AODL.Document.Forms.Controls.ODFFormControl FindControlById(string id)
		{
			foreach (ODFFormControl fc in this)
			{
				if (fc.ID == id)
					return fc;
			}
			return null;
		}
	}
}
