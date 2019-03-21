using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteCredentials.Common;
using SiteCredentials.Services.Interfaces;
using SiteCredentials.Services;
using SiteCredentials.DTO;
using System.IO;
using SiteCredentials.Services.FileServices;

namespace SiteCredentials.Test
{
    [TestClass]
    public class SiteCredentialParserTest
    {
        protected Dictionary<string, string> lines = new Dictionary<string, string>
            {
                {"test www.test.com", "Name:test\tUrl:www.test.com\tCredentials count:0"},
                {"test www.test.com username","Name:test\tUrl:www.test.com\tCredentials count:1"},
                {"test www.test.com username password","Name:test\tUrl:www.test.com\tCredentials count:2"}
            };

        protected string fileName = Directory.GetCurrentDirectory() + "\\TestFile.txt";

        [TestInitialize]
        public void Initialize()
        {
            ObjectFactory.Register<IWebSiteChecker, WebSiteChecker>();
            ObjectFactory.Register<ISiteCredentialParser, SiteCredentialParser>();
            ObjectFactory.Register<IFileFinderFactory, FileFinderFactory>();
            ObjectFactory.Register<IFileFinder, LocalFileFinder>("default");
            ObjectFactory.Register<IFileFinder, HttpFileFinder>("http");
        }

        [TestCleanup]
        public void Cleanup()
        {
            ObjectFactory.Clean();
        }

        [TestMethod]
        public void ParseLineSuccess()
        {
            ISiteCredentialParser parser = ObjectFactory.GetInstance<ISiteCredentialParser>();

            Site site = null;

            foreach (string line in lines.Keys)
            {
                site = parser.ParseLine(line);
                Assert.AreEqual(site.ToString(), lines[line]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseLineThrowsArgumentException()
        {
            ISiteCredentialParser parser = ObjectFactory.GetInstance<ISiteCredentialParser>();

            Site site = parser.ParseLine("test");
        }

        [TestMethod]
        public void ParseFileSuccess()
        {
            //Create file
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true);

            foreach (string line in lines.Keys)
            {
                file.WriteLine(line);
            }
            file.Close();
            
            //test parser
            ISiteCredentialParser parser = ObjectFactory.GetInstance<ISiteCredentialParser>();
            parser.ParseFile(fileName);

            //delete file
            File.Delete(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseFileMissingFile()
        {
            ISiteCredentialParser parser = ObjectFactory.GetInstance<ISiteCredentialParser>();
            parser.ParseFile("");
        }
    }
}
