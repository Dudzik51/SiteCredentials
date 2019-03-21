using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteCredentials.Services.Interfaces
{
    public interface IFileFinderFactory
    {
        IFileFinder GetFileFinder(string fileLocation);
    }
}
