using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteCredentials.DTO;

namespace SiteCredentials.Services.Interfaces
{
    public interface ISiteCredentialParser
    {
        List<Site> ParseFile(string filename);

        Site ParseLine(string line);
    }
}
