//
//  ProxyCommon.cs
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
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Chloride.SOCKS
{
	public class ProxyRequest
	{
		public string User;
		public IPEndPoint Target;
		public Socket NetStream;

		public ProxyRequest(string user, IPAddress ip, int port, Socket sockstream)
		{
			User = user;
			Target = new IPEndPoint(ip, port);
			NetStream = sockstream;
		}
	}

	public static class ProxyCommon
    {
		public static int ProxyTCP(ProxyRequest pr)
		{
			Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			sock.Connect(pr.Target);

			byte[] buffer = new byte[1];
			while (pr.NetStream.IsBound)
			{
				if (!sock.IsBound)
				{
					pr.NetStream.Close();
					break;
				}
				sock.Receive(buffer);
				pr.NetStream.Send(buffer);
			}
			sock.Disconnect(false);
			return 0;
		}

		public static bool Auth(string User)
		{
			return true;
		}
    }
}

