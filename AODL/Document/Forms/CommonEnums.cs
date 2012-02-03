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

namespace AODL.Document
{
	public enum XmlBoolean {True,False,NotSet};
}

namespace AODL.Document.Forms
{
	public enum TargetFrame {Self, Blank, Parent, Top, NotSet};
	public enum Method {Get,Post,NotSet};
	public enum CommandType {Table,Query,Command,NotSet};
	public enum NavigationMode {None,Current,Parent,NotSet};
	public enum TabCycle {Records,Current,Page,NotSet};
	public enum PropertyValueType {Float, Percentage, Currency, Date, Time, Boolean, String, NotSet};	
	public enum ListSourceType {Table, Query, Sql, SqlPassThrough, ValueList, TableFields, NotSet};	

	public enum VisualEffect {Flat, ThreeD, NotSet};
	public enum ImagePosition {Start, End, Top, Bottom, NotSet}
	public enum ImageAlign {Start, Center, End, NotSet}
	public enum State {Unchecked, Checked, Unknown, NotSet}
	public enum Orientation {Horizontal, Vertical, NotSet};
	public enum ButtonType {Submit, Reset, Push, Url, NotSet};
}
