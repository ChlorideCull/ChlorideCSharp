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
using System.Threading;

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
			try {
				sock.Connect(pr.Target);
			} catch (SocketException ex) {
				#if DEBUG
				Console.WriteLine(ex);
				#endif
				pr.NetStream.Close();
				return 1;
			}

			bool Lock = true;
			Thread inco = new Thread(() => passthrough(pr.NetStream, sock, ref Lock));
			Thread outg = new Thread(() => passthrough(sock, pr.NetStream, ref Lock));
			inco.Start();
			outg.Start();

			while (true)
			{
				if (!Lock)
				{
					inco.Abort();
					outg.Abort();
					pr.NetStream.Close();
					sock.Close();
					break;
				}
				Thread.Sleep(10);
			}
			return 0;
		}

		public static bool Auth(string User)
		{
			return true;
		}

		private static void passthrough(Socket From, Socket To, ref bool Lock)
		{
			byte[] buffer = new byte[1];
			while (Lock)
			{
				//Sockets can be killed due to race condition I don't know how to get rid of
				try
				{
					From.Receive(buffer);
					To.Send(buffer);
				}
				catch (SocketException)
				{
					#if DEBUG
					Console.WriteLine("Thread ended.");
					#endif
					Lock = false;
					return;
				}
			}
		}
	}
}

