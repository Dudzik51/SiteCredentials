using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteCredentials.Services.Interfaces;
using SiteCredentials.DTO;
using System.IO;
using SiteCredentials.Common;

namespace SiteCredentials.Services
{
    public class SiteCredentialParser : ISiteCredentialParser
    {
        /// <summary>
        /// Parses file from given file location and returns found sites
        /// </summary>
        /// <param name="fileLocation">File location</param>
        /// <returns>List of sites</returns>
        public List<Site> ParseFile(string fileLocation)
        {
            IFileFinder fileFinder =  ObjectFactory.GetInstance<IFileFinderFactory>().GetFileFinder(fileLocation);
            List<Site> sites = new List<Site>();

            using (StreamReader file = fileFinder.GetFile(fileLocation))
            {
                String line = string.Empty;

                while ((line = file.ReadLine()) != null)
                {
                    sites.Add(ParseLine(line));
                }
            }

            return sites;
        }

        /// <summary>
        /// Parses single line of text
        /// </summary>
        /// <param name="line">Text</param>
        /// <returns>Parsed Site</returns>
        public Site ParseLine(string line)
        {
            string[] tokens = line.Split(' ');

            if (tokens.Length < 2)
                throw new ArgumentException("There must be at least name and url for each site");

            Site newSite = new Site() { Name = tokens[0], Url = tokens[1] };

            for(int i = 2; i < tokens.Length; i++)
                newSite.LoginCredentials.Add(new LoginCredential() { Value = tokens[i] });

            return newSite;
        }
    }
}
