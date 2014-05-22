//
//  PostingAPI.cs
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
using System.Collections.Generic;
using Chloride.ReCaptcha;

namespace Chloride.FourChan
{
    public class PostingAPI
    {
		private Captcha PostCaptcha;
		private Dictionary<CaptchaData, string> CaptchaCache;

        public PostingAPI()
        {
			PostCaptcha = new Captcha("6Ldp2bsSAAAAAAJ5uyx_lx34lJeEpTLVkP5k04qc", new Uri("http://boards.4chan.org/"));
			CaptchaCache = new Dictionary<CaptchaData, string>();
        }

		public CaptchaData GetCaptchaData()
		{
			return PostCaptcha.GetData();
		}

		public void CacheCaptchaResponse(CaptchaData cdata, string response)
		{
			if (cdata.Key == "6Ldp2bsSAAAAAAJ5uyx_lx34lJeEpTLVkP5k04qc")
				CaptchaCache.Add(cdata, response);
		}

		public void PostResponse(string board, uint threadid, string posttext, byte[] image = null)
		{
			throw new NotImplementedException();
		}

		public uint PostNewThread(string board, string posttext, byte[] image = null)
		{
			throw new NotImplementedException();
		}
    }
}