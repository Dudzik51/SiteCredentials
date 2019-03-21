using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SiteCredentials.Services.Interfaces
{
    public interface IFileFinder
    {
        StreamReader GetFile(string fileLocation);
    }
}
