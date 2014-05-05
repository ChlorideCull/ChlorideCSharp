//
//  Captcha.cs
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
using System.Web;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Chloride.ReCaptcha
{
	public class Captcha
    {
		private string RCKey;
		private Uri RCRefer;
		private WebClient wc;
		public Captcha(string key, Uri referer)
        {
			RCKey = key;
			RCRefer = referer;
			wc = new WebClient();
			wc.Headers.Add("Referer: " + RCRefer.GetLeftPart(UriPartial.Authority) + "/");
			wc.Headers.Add("DNT: 1");
			wc.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17");
        }

		public CaptchaData GetData()
		{
			string data = wc.DownloadString("http://www.google.com/recaptcha/api/challenge?k=" + RCKey + "&ajax=1");
			Regex matcher = new Regex("    (?:(challenge) : '(.*)'|(timeout) : (\\d*)|(programming_error) : '(.*)'|(error_message) : '(.*)')");
			MatchCollection mc = matcher.Matches(data);
			CaptchaData cd = new CaptchaData();
			foreach (Match mx in mc)
			{
				string type = mx.Captures[0].Value;
				if (type == "challenge")
					cd.Challenge = mx.Captures[1].Value;
				else if (type == "timeout")
					cd.Expiry = DateTime.Now.AddSeconds(mx.Captures[1].Value);
				else if ((type == "programming_error") && (mx.Captures[1] != ""))
					throw new ArgumentException("Programming error: " + mx.Captures[1].Value);
				else if ((type == "error_message") && (mx.Captures[1] != ""))
					throw new ArgumentException("Error: " + mx.Captures[1].Value);
			}
			cd.Image = wc.DownloadData("http://www.google.com/recaptcha/api/image?c=" + cd.Challenge);
			return cd;
		}

		public string[] GetHeaders(CaptchaData cdata, string Response)
		{
			return new string[] { "recaptcha_challenge_field: " + cdata.Challenge, "recaptcha_response_field: " + Response };
		}
    }

	public class CaptchaData
	{
		public string Challenge;
		public byte[] Image;
		public DateTime Expiry;

		public CaptchaData(string challenge, byte[] image, DateTime expire)
		{
			Challenge = challenge;
			Image = image;
			Expiry = expire;
		}
		public CaptchaData() {}
	}	
}

