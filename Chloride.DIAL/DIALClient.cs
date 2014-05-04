//
//  DIALClient.cs
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
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Chloride.DIAL
{
	public static class DIALClient
    {
		public static DIALDevice[] SearchDIALDevices(double MaxWait = 1)
		{
			UdpClient udpc = new UdpClient(AddressFamily.InterNetwork);
			udpc.JoinMulticastGroup(IPAddress.Parse("239.255.255.250"));
			IPEndPoint mcany = new IPEndPoint(IPAddress.Any, 1900);

			string discover = "M-SEARCH * HTTP/1.1\nHOST: 239.255.255.250:1900\nMAN: \"ssdp:discover\"\nMX: ";
			discover += Convert.ToString(MaxWait);
			discover += "\nST: urn:dial-multiscreen-org:service:dial:1\nUSER-AGENT: ";
			discover += Environment.OSVersion.Platform.ToString() + "/" + Environment.OSVersion.Version.ToString();
			discover += " .NET/" + Environment.Version.ToString();
			byte[] tosend = Encoding.ASCII.GetBytes(discover);
			udpc.Send(tosend, tosend.Length);

			DateTime now = DateTime.Now;
			List<DIALDevice> addresses = new List<DIALDevice>();
			while (DateTime.Now < now.Add(TimeSpan.FromSeconds(MaxWait)))
			{
				byte[] read = udpc.Receive(mcany);
				string[] headers = Encoding.ASCII.GetString(read).Split("/n");
				foreach (string header in headers)
				{
					if (header.StartsWith("LOCATION: "))
					{
						WebClient wc = new WebClient();
						string xmlinfo = Encoding.UTF8.GetString(wc.DownloadData(new Uri(header.Substring(10))));
						string appurl = wc.ResponseHeaders["Application-Url"];
						string[] appurla = appurl.Split("/", 3, StringSplitOptions.RemoveEmptyEntries);
						IPAddress ip;
						int port;
						if (appurla[1].Contains(":"))
						{
							ip = IPAddress.Parse(appurla[1].Split(":")[0]);
							port = Convert.ToInt32(appurla[1].Split(":")[1]);
						}
						else
						{
							ip = IPAddress.Parse(appurla[1]);
							port = 80;
						}
						//TODO: read xml, get name and description
						//		see http://192.168.1.39:8008/ssdp/device-desc.xml 
						addresses.Add(new DIALDevice("FriendlyName", "Model", "Manufacturer", "Description", new IPEndPoint(ip, port), appurla[2]));
						break;
					}
				}
			}
		}
    }
}

