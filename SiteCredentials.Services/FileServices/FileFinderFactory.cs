using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteCredentials.Services.Interfaces;
using SiteCredentials.Common;

namespace SiteCredentials.Services.FileServices
{
    public class FileFinderFactory : IFileFinderFactory
    {
        /// <summary>
        /// File finder factory
        /// </summary>
        /// <param name="fileLocation">file location</param>
        /// <returns>IFileFinder based on file location</returns>
        public IFileFinder GetFileFinder(string fileLocation)
        {
            IFileFinder fileFinder = null;

            string[] tokens = fileLocation.Split(':');

            //Should be replaced with more soficticated implementation i.e. automatically list named instances
            switch (tokens[0])
            {
                case "http":
                    {
                        fileFinder = ObjectFactory.GetNamedInstance<IFileFinder>("http");
                        break;
                    }
                //case "ftp":
                //    {
                //        ObjectFactory.GetNamedInstance<IFileFinder>("ftp");
                //        break;
                //    }
                default:
                    {
                        fileFinder = ObjectFactory.GetNamedInstance<IFileFinder>("default");
                        break;
                    }

            }

            return fileFinder;
        }
    }
}
