using System;
using System.IO;
using Vivacity.Library.Utils;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using Vivacity.Library.Builder;

namespace Citities.Library.Test
{
    [TestFixture]
    public class EntityTest
    {
        [Test]
        public void EqualsTest()
        {
            Stream cs1Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs1.txt");

            StreamReader cs1Reader = new StreamReader(cs1Stream);
            string cs1Text = cs1Reader.ReadToEnd();

            CsParser cs1Parser = new CsParser();
            ParseResult cs1Result = cs1Parser.Parse(cs1Text);
            Entity cs1Entity1 = new Entity(cs1Result, DateTime.Now, 1);
            Entity cs1Entity2 = new Entity(cs1Result, DateTime.Now, 2);
            Entity cs1Entity3 = new Entity(cs1Result, DateTime.Now, 1);

            Stream cs2Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs2.txt");
            StreamReader cs2Reader = new StreamReader(cs2Stream);
            string cs2Text = cs2Reader.ReadToEnd();

            CsParser cs2Parser = new CsParser();
            ParseResult cs2Result = cs2Parser.Parse(cs2Text);
            Entity cs2Entity1 = new Entity(cs2Result, DateTime.Now, 1);
            Entity cs2Entity2 = new Entity(cs2Result, DateTime.Now, 2);

            Assert.AreNotEqual(cs2Entity2, cs2Entity1);
            Assert.AreNotEqual(cs1Entity2, cs1Entity1);
            Assert.AreEqual(cs1Entity1, cs1Entity3);

        }
    }
}

