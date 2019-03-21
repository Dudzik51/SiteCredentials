using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteCredentials.Services.Interfaces;
using System.IO;
using System.Net;

namespace SiteCredentials.Services.FileServices
{
    public class LocalFileFinder : IFileFinder
    {
        public StreamReader GetFile(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                throw new ArgumentException("File does not exist");

            return new StreamReader(fileLocation);
        }
    }
}
