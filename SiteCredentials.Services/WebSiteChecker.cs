using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteCredentials.Services.Interfaces;

namespace SiteCredentials.Services
{
    public class WebSiteChecker : IWebSiteChecker
    {
        private static Random random = new Random();

        /// <summary>
        /// Check if given url is online
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsOnline(string url)
        {
            return random.Next(2) == 1;
        }
    }
}
