//
//  SOCKS5.cs
//
//  Author:
//       Chloride Cull <chloride@devurandom.net>
//
//  Copyright (c) 2014 Sebastian \"Chloride Cull\" Johansson
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
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace Chloride.SOCKS
{
	public class SOCKS4
	{
		private TcpListener tl;
		public SOCKS4(IPEndPoint LocalEndPoint)
		{
			tl = new TcpListener(LocalEndPoint);
		}

		public void Listen()
		{
			this.Listen(ProxyCommon.ProxyTCP, ProxyCommon.Auth);
		}
		public void Listen(Func<ProxyRequest, int> processTCPRequest, Func<string, bool> processAuthentication)
		{
			tl.Start();
			while (true)
			{
				Socket sock = tl.AcceptSocket();
				new Thread(() => ProcessConnection(processTCPRequest, processAuthentication, sock)).Start();
			}
		}

		private void ProcessConnection(Func<ProxyRequest, int> processTCPRequest, Func<string, bool> processAuthentication, Socket sock)
		{
			byte[] verinfo = new byte[2];
			sock.Receive(verinfo);
			if (verinfo[0] != 0x04)
			{
				sock.Close();
				return;
			}
			if (verinfo[1] != 0x01)
			{
				//Only support streams, not opening ports.
				sock.Close();
				return;
			}
			byte[] portip = new byte[6];
			byte[] bport = new byte[2];
			byte[] bip = new byte[4];
			sock.Receive(portip);
			Array.Copy(portip, bport, 2);
			Array.Copy(portip, 2, bip, 0, 4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bport);
				//Array.Reverse(bip);
			}

			int port = Convert.ToInt32(BitConverter.ToUInt16(bport, 0));
			IPAddress ip = new IPAddress((long)BitConverter.ToUInt32(bip, 0));

			List<byte> userid = new List<byte>();
			while ((userid.Count == 0) || (userid[userid.Count - 1] != 0x00))
			{
				byte[] temp = new byte[1];
				sock.Receive(temp);
				userid.Add(temp[0]);
			}
			if (BitConverter.IsLittleEndian)
				userid.Reverse();
			string user = System.Text.ASCIIEncoding.ASCII.GetString(userid.ToArray());

			ProxyRequest pr = new ProxyRequest(user, ip, port, sock);
			//Begin validation and reply
			bool reject = false;
			byte[] message = new byte[8];
			message[0] = 0x00;
			if (processAuthentication.Invoke(user))
				message[1] = 0x5a;
			else
			{
				message[1] = 0x5b;
				reject = true;
			}
			//Why do we have to add gibberish that is ignored
			System.Text.ASCIIEncoding.ASCII.GetBytes("P0NIES").CopyTo(message, 2);
			sock.Send(message);
			#if DEBUG
			Console.WriteLine("New tunnel!");
			Console.Write("\nPort/IP: ");
			foreach (byte b in portip)
			{
				Console.Write(b);
				Console.Write(" ");
			}
			Console.WriteLine();
			Console.WriteLine(port);
			Console.WriteLine(BitConverter.ToUInt32(portip, 2));
			Console.WriteLine(verinfo.Length + portip.Length);
			Console.WriteLine(reject);
			Console.Out.Flush();
			#endif
			if (reject)
				sock.Close();
			else
				processTCPRequest.Invoke(pr);
		}
	}
}

