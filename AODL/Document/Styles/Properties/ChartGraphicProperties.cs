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

namespace AODL.Document .Styles .Properties
{
	/// <summary>
	/// Summary description for ChartGraphicProperties.
	/// </summary>
	public class ChartGraphicProperties : IProperty
	{
		/// <summary>
		/// gets and sets the draw stroke
		/// </summary>
		public string DrawStroke
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke", value, "draw");
				this._node.SelectSingleNode("@draw:stroke",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets stroke dash
		/// </summary>

		public string StrokeDash
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke-dash",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke-dash",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke-dash", value, "draw");
				this._node.SelectSingleNode("@draw:stroke-dash",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets stroke dash name
		/// </summary>

		public string StrokeDashNames
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke-dash-names",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke-dash-names",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke-dash-names", value, "draw");
				this._node.SelectSingleNode("@draw:stroke-dash-names",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets the stroke width
		/// </summary>

		public string StrokeWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:stroke-width",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:stroke-width",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke-width", value, "draw");
				this._node.SelectSingleNode("@svg:stroke-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets stroke color
		/// </summary>

		public string StrokeColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:stroke-color",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:stroke-color",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke-color", value, "draw");
				this._node.SelectSingleNode("@svg:stroke-color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets marker start
		/// </summary>

		public string MarkerStart
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-start",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-start",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("marker-start", value, "draw");
				this._node.SelectSingleNode("@draw:marker-start",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

        /// <summary>
        /// gets and sets marker end
        /// </summary>
		
		public string MarkerEnd
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-end",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-end",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("marker-end", value, "draw");
				this._node.SelectSingleNode("@draw:marker-end",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets marker start width
		/// </summary>

		public string MarkerStartWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-start-width",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-start-width",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("marker-start-width", value, "draw");
				this._node.SelectSingleNode("@draw:marker-start-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        
		/// <summary>
		/// gets and set marker end width
		/// </summary>
		public string MarkerEndWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-end-width",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-end-width",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("marker-end-width", value, "draw");
				this._node.SelectSingleNode("@draw:marker-end-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets marker end center
		/// </summary>

		public string MarkerEndCenter
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-end-center",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-end-center",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("marker-end-center", value, "draw");
				this._node.SelectSingleNode("@draw:marker-end-center",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets marker start center
		/// </summary>
 
		public string MarkerStartCenter
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-start-center",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:marker-start-center",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("marker-start-center", value, "draw");
				this._node.SelectSingleNode("@draw:marker-start-center",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets stroke opacity
		/// </summary>
		public string StrokeOpacity
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:stroke-opacity",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:stroke-opacity",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke-opacity", value, "draw");
				this._node.SelectSingleNode("@svg:stroke-opacity",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// gets and sets stroke line join
		/// </summary>
		public string StrokeLineJoin
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke-linejoin",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:stroke-linejoin",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("stroke-linejoin", value, "draw");
				this._node.SelectSingleNode("@draw:stroke-linejoin",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill color
		/// </summary>
		public string FillColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-color",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-color",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-color", value, "draw");
				this._node.SelectSingleNode("@draw:fill-color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
  
		/// <summary>
		/// gets and sets secondary fill color
		/// </summary>
		public string SecondaryFillColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:secondary-fill-color",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:secondary-fill-color",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("secondary-fill-color", value, "draw");
				this._node.SelectSingleNode("@draw:secondary-fill-color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        

		/// <summary>
		/// gets and sets fill gradient name
		/// </summary>
		public string FillGradientName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-gradient-name",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-gradient-name",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-gradient-name", value, "draw");
				this._node.SelectSingleNode("@draw:fill-gradient-name",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        
		/// <summary>
		/// gets and sets gradient step count
		/// </summary>
		public string GradientStepCount
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:gradient-step-count",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:gradient-step-count",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("gradient-step-count", value, "draw");
				this._node.SelectSingleNode("@draw:gradient-step-count",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill hatch name
		/// </summary>

		public string FillHatchName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-hatch-name",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-hatch-name",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-hatch-name", value, "draw");
				this._node.SelectSingleNode("@draw:fill-hatch-name",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill hatch solid
		/// </summary>

		public string FillHatchSolid
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-hatch-solid",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-hatch-solid",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-hatch-solid", value, "draw");
				this._node.SelectSingleNode("@draw:fill-hatch-solid",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}
        
		/// <summary>
		/// gets and sets fill image name
		/// </summary>
		public string FillImageName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-name",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-name",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-image-name", value, "draw");
				this._node.SelectSingleNode("@draw:fill-image-name",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets style repeat
		/// </summary>

		public string StyleRepeat
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:repeat",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:repeat",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("repeat", value, "style");
				this._node.SelectSingleNode("@style:repeat",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill image width
		/// </summary>

		public string FillImageWidth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-width",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-width",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-image-width", value, "draw");
				this._node.SelectSingleNode("@draw:fill-image-width",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill image height
		/// </summary>

		public string FillImageHeight
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-height",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-height",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-image-height", value, "draw");
				this._node.SelectSingleNode("@draw:fill-image-height",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill image ref point
		/// </summary>

		public string FillImageRefPoint
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-ref-point",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-ref-point",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-image-ref-point", value, "draw");
				this._node.SelectSingleNode("@draw:fill-image-ref-point",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill image ref pointx
		/// </summary>

		public string FillImageRefPointX
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-ref-point-x",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-ref-point-x",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-image-ref-point-x", value, "draw");
				this._node.SelectSingleNode("@draw:fill-image-ref-point-x",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill image ref pointy
		/// </summary>

		public string FillImageRefPointY
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-ref-point-y",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:fill-image-ref-point-y",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-image-ref-point-y", value, "draw");
				this._node.SelectSingleNode("@draw:fill-image-ref-point-y",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets tile repeat offset
		/// </summary>

		public string TileRepeatOffset
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:tile-repeat-offset",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:tile-repeat-offset",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("tile-repeat-offset", value, "draw");
				this._node.SelectSingleNode("@draw:tile-repeat-offset",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}


		/// <summary>
		/// gets and sets opacity
		/// </summary>

		public string Opacity
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:opacity",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:opacity",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("opacity", value, "draw");
				this._node.SelectSingleNode("@draw:opacity",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets opacity name
		/// </summary>

		public string OpacityName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:opacity-name",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:opacity-name",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("opacity-name", value, "draw");
				this._node.SelectSingleNode("@draw:opacity-name",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets fill rule
		/// </summary>

		public string FillRule
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@svg:fill-rule",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@svg:fill-rule",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("fill-rule", value, "svg");
				this._node.SelectSingleNode("@svg:fill-rule",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets symbol color
		/// </summary>

		public string SymbolColor
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@draw:symbol-color",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@draw:symbol-color",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("symbol-color", value, "svg");
				this._node.SelectSingleNode("@draw:symbol-color",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets horizontal segment
		/// </summary>

		public string HorizontalSegment
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:horizontal-segments",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:horizontal-segments",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("horizontal-segments", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:horizontal-segments",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets vertical segment
		/// </summary>

		public string VerticalSegment
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:vertical-segments",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:vertical-segments",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("vertical-segments", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:vertical-segments",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets edge rounding
		/// </summary>

		public string EdgeRounding
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:edge-rounding",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:edge-rounding",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("edge-rounding", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:edge-rounding",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets edge rounding mode
		/// </summary>

		public string EdgeRoundingMode
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:edge-rounding-mode",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:edge-rounding-mode",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("edge-rounding-mode", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:edge-rounding-mode",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets back scale
		/// </summary>

		public string BackScale
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:back-scale",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:back-scale",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("back-scale", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:back-scale",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets depth
		/// </summary>

		public string Depth
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:depth",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:depth",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("depth", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:depth",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets back face culling
		/// </summary>

		public string BackFaceCulling
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:backface-culling",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:backface-culling",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("backface-culling", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:backface-culling",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets end angle
		/// </summary>

		public string EndAngle
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:end-angle",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:end-angle",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("end-angle", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:end-angle",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets close front
		/// </summary>

		public string CloseFront
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:close-front",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:close-front",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("close-front", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:close-front",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// gets and sets close back
		/// </summary>

		public string CloseBack
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@dr3d:close-back",
					this.Style.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@dr3d:close-back",
					this.Style.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("close-back", value, "dr3d");
				this._node.SelectSingleNode("@dr3d:close-back",
					this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

        /// <summary>
        /// the constructor of chart graphic property
        /// </summary>
        /// <param name="style"></param>
		public ChartGraphicProperties(IStyle style)
		{
			this.Style			= style;
			this.NewXmlNode();
		}

		/// <summary>
		/// Create the XmlNode which represent the propertie element.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Style.Document.CreateNode("style", "graphic-properties");
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Style.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		#region IProperty Member
		private XmlNode _node;
		/// <summary>
		/// The XmlNode which represent the property element.
		/// </summary>
		/// <value>The node</value>
		public System.Xml.XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		private IStyle _style;
		/// <summary>
		/// The style object to which this property object belongs
		/// </summary>
		/// <value></value>
		public IStyle Style
		{
			get { return this._style; }
			set { this._style = value; }
		}
		#endregion


	
	}
}

