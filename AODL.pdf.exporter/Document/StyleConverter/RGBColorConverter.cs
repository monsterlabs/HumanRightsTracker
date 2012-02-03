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

namespace AODL.ExternalExporter.PDF.Document.StyleConverter
{
	/// <summary>
	/// Summary for RGBColorConverter.
	/// </summary>
	public class RGBColorConverter
	{
		public RGBColorConverter()
		{
		}

		/// <summary>
		/// Toes the color of the hex.
		/// </summary>
		/// <param name="rgbColor">Color of the RGB.</param>
		/// <returns></returns>
		public static string ToHexColor(string rgbColor)
		{
			try
			{
				//need leading zeros ?
				if (rgbColor.Length < 9)
					//fill up with leading zeros
					rgbColor      = rgbColor.PadLeft(9, '0');
				//our rgb values
				int rValue      = Convert.ToInt32(rgbColor.Substring(0,3));
				int gValue      = Convert.ToInt32(rgbColor.Substring(3,3));
				int bValue      = Convert.ToInt32(rgbColor.Substring(6));
				//convert into hex
				string srValue   = ((rValue!=0)?rValue.ToString("x"):"00");
				string sgValue   = ((gValue!=0)?gValue.ToString("x"):"00");
				string sbValue   = ((bValue!=0)?bValue.ToString("x"):"00");
				//build hex color
				string hexColor   = "#"
					//fill with leading zeros, if needed
					+ srValue.PadLeft(2, '0')
					+ sgValue.PadLeft(2, '0')
					+ sbValue.PadLeft(2, '0');
      
				return hexColor;
			}
			catch(Exception)
			{
				return "#000000"; // black as default,
			}
		} 

		/// <summary>
		/// Gets the color from hex.
		/// </summary>
		/// <param name="rgbColor">Color of the RGB.</param>
		/// <returns></returns>
		public static iTextSharp.text.Color GetColorFromHex(string rgbColor)
		{
			iTextSharp.text.Color color = new iTextSharp.text.Color(0,0,0);
			try
			{
				if (rgbColor != null && rgbColor.StartsWith("#"))
					color = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml(rgbColor));
			}
			catch(Exception)
			{}
			return color; // default black
		}
	}
}
