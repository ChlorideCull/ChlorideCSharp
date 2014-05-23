//
//  XMLTest.cs
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
using Chloride.XML;
using NUnit.Framework;
using System.Xml;

namespace NUnitTests
{
    [TestFixture()]
    public class XMLTest
    {
        [Test()]
		public void TestSmall()
        {
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			byte[] testdata = System.Text.Encoding.UTF8.GetBytes("<root><text>best pone</text><answer honest=\"true\">applejack</answer></root>");
			ms.Write(testdata, 0, testdata.Length);
			XmlReader xr = XmlReader.Create(ms);
			HumaneXMLNode hxl = HumaneXMLNode.MakeNode(xr);

			Assert.AreEqual(2, hxl.Children.Count);
			Assert.AreEqual("best pone", hxl.getElementByTag("text").Value);
			Assert.AreEqual("applejack", hxl.getElementByParameter("honest", "true").Value);
        }
    }
}

