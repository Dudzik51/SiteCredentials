using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SiteCredentials.Services.Interfaces;
using System.Net;

namespace SiteCredentials.Services.FileServices
{
    public class HttpFileFinder : IFileFinder
    {

        public StreamReader GetFile(string fileLocation)
        {
            throw new NotImplementedException();
        }
    }
}
