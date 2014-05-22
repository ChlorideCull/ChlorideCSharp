//
//  DIALDevice.cs
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
using System.Net;
using System.Collections.Generic;

namespace Chloride.DIAL
{
    public class DIALDevice
    {
		public DIALDevice(string FriendlyName, string Model, string Manufacturer, string Description, IPEndPoint RESTLocation, string RESTAppsBase)
        {
			throw new NotImplementedException();
        }

		public string GetCurrentApplication()
		{
			throw new NotImplementedException();
		}

		public void LaunchApplication(string Name, IDictionary<string, string> Parameters)
		{
			throw new NotImplementedException();
		}
    }
}

