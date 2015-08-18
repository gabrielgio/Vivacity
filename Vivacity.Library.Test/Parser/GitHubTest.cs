using System;
using NUnit.Framework;
using Vivacity.Library.Parser.GitHub;
using System.Collections.Generic;
using Vivacity.Library.Parser;
using System.Linq;

namespace Citities.Library.Test
{
    [TestFixture()]
    public class GitHubTest
    {
        private string _username = "gabrielgio";
        private string _password = "Diablo@123";
        private string _owner = "JamesNK";
        private string _project = "Newtonsoft.Json";

        [Test()]
        public void ReadTest()
        {
            GitHubParser gitHubParser = new GitHubParser(_owner, _project, _username, _password);
            IEnumerable<IFile> files = gitHubParser.Read();

            Assert.NotNull(files);

            if (files == null)
                return;

            Assert.Greater(files.Count(), 0);

            foreach (var item in files)
            {
                Assert.IsNotNullOrEmpty(item.Path);
            }


        }
    }
}

