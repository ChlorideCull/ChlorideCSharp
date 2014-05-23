//
//  DIALTest.cs
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
using NUnit.Framework;
using Chloride.DIAL;
using System;

namespace NUnitTests
{
    [TestFixture()]
    public class DIALTest
    {
        [Test()]
		[Ignore("We need Server functionality to create a listening DIAL server, else success depends on if any devices exist in the network.")]
		public void TestSearch()
        {
        }

		[Test()]
		[Ignore("No Server functionality yet implemented.")]
		public void TestServer()
		{

		}

		[Test()]
		[Ignore("DIALDevice class not yet implemented.")]
		public void TestDeviceClassCreate()
		{
			DIALDevice dd = new DIALDevice("TestDevice", "TST01", "Acme Inc.", "Test Device", new System.Net.IPEndPoint(System.Net.IPAddress.Parse("::1"), 8085), "/test/");
		}
    }
}

