using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteCredentials.Common;
using SiteCredentials.Services.Interfaces;
using SiteCredentials.Services;
using SiteCredentials.DTO;
using SiteCredentials.Services.FileServices;

namespace SiteCredentials
{
    class Program
    {
        private static string fileName = "Sites.txt";

        static void Main(string[] args)
        {
            //Configure simple DI container
            Configure();

            //Get parser and website checker instances
            ISiteCredentialParser parser = ObjectFactory.GetInstance<ISiteCredentialParser>();
            IWebSiteChecker webSiteChecker = ObjectFactory.GetInstance<IWebSiteChecker>();

            //If there are arguments, take first as filename. Would be a bit more sophisticated in real life of course.
            if (args.Length != 0)
                fileName = args[0];

            //Parse file
            List<Site> sites = parser.ParseFile(fileName);

            //And order results. I didn't put that in service not to generate not neeeded overhead. Not always we need sorted list.
            sites = sites.OrderBy(p => p.Name).ToList();

            //While printing each Site details we will count total credentials.
            //Could be done by var totalCredentials = (from s in sites select s.LoginCredentials.Count).Sum(); but we must pass the list anyway.
            int totalCredentials = 0;

            //Print results
            foreach (Site site in sites)
            {
                Console.WriteLine(
                    String.Format("{0}\tIsOnline: {1}", site.ToString(), webSiteChecker.IsOnline(site.Url).ToString())
                );

                totalCredentials += site.LoginCredentials.Count;
            }

            //Empty line and total number of credentials
            Console.WriteLine();
            Console.WriteLine("Total number of credentials: " + totalCredentials);

            Console.ReadLine();
        }

        static void Configure()
        {
            //DI configuration for each used interface
            ObjectFactory.Register<IWebSiteChecker, WebSiteChecker>();
            ObjectFactory.Register<ISiteCredentialParser, SiteCredentialParser>();
            ObjectFactory.Register<IFileFinderFactory, FileFinderFactory>();
            ObjectFactory.Register<IFileFinder, LocalFileFinder>("default");
            ObjectFactory.Register<IFileFinder, HttpFileFinder>("http");
        }
    }
}