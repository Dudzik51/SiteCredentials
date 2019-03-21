using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteCredentials.DTO
{
    public class Site
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public List<LoginCredential> LoginCredentials { get; set; }

        public Site()
        {
            LoginCredentials = new List<LoginCredential>();
        }

        public string ToString()
        {
            return String.Format("Name:{0}\tUrl:{1}\tCredentials count:{2}", Name, Url, LoginCredentials.Count);
        }
    }
}
