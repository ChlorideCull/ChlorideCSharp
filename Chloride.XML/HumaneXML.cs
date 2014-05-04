//
//  HumaneXML.cs
//
//  Author:
//       Chloride Cull <chloride@devurandom.net>
//
//  Copyright (c) 2014 Sebastian "Chloride Cull" Johansson
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Xml;
using System.Collections.Generic;

namespace Chloride.XML
{
	public class HumaneXMLNode
	{
		//This entire class is literally hitler (on the inside, at least)
		public string Tag;
		public string Value;
		public IDictionary<string, string> Attributes;
		public List<HumaneXMLNode> Children;

		public HumaneXMLNode(string _Tag, string _Value, IDictionary<string, string> _Attributes, List<HumaneXMLNode> _Children)
		{
			this.Tag = _Tag;
			this.Value = _Value;
			this.Attributes = _Attributes;
			this.Children = _Children;
		}

		public static HumaneXMLNode MakeNode(XmlReader Doc)
		{
			List<HumaneXMLNode> xnstack = new List<HumaneXMLNode>();
			while (Doc.Read())
			{
				if (Doc.NodeType == XmlNodeType.Element)
				{
					Dictionary<string, string> d = new Dictionary<string, string>();
					List<HumaneXMLNode> l = new List<HumaneXMLNode>();
					HumaneXMLNode hxn = new HumaneXMLNode(Doc.Name, Doc.Value, d, l);
					if (xnstack.Count > 0)
						xnstack[xnstack.Count - 1].Children.Add(hxn);
					xnstack.Add(hxn);
				}
				else if (Doc.NodeType == XmlNodeType.EndElement)
				{
					if (xnstack.Count > 1)
					{
						xnstack.RemoveAt(xnstack.Count - 1);
					}
					else
					{
						return xnstack[0];
					}
				}
				else if (Doc.NodeType == XmlNodeType.Attribute)
					xnstack[xnstack.Count-1].Attributes.Add(Doc.Name, Doc.Value);
			}
			throw new ArgumentException("XML file did not properly end, possibly missing exit tag", "Doc");
		}
		public static HumaneXMLNode MakeNode(string path)
		{
			return MakeNode(XmlReader.Create(path));
		}

		public HumaneXMLNode getElementByTag(string tag)
		{
			foreach (HumaneXMLNode thxl in this.Children)
			{
				if (thxl.Tag == tag)
					return thxl;
			}
			throw new ArgumentException("Tag not found");
		}

		public HumaneXMLNode getElementByParameter(string param)
		{
			foreach (HumaneXMLNode thxl in this.Children)
			{
				foreach (KeyValuePair<string, string> kvp in thxl.Attributes)
				{
					if (kvp.Key == param)
						return thxl;
				}
			}
			throw new ArgumentException("Element not found");
		}

		public HumaneXMLNode this[string tag] { get { return this.getElementByTag(tag); }}
		public HumaneXMLNode this[int index] { get { return this.Children[index]; }}
	}
}

