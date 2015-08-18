using System;
using NUnit.Framework;
using Vivacity.Library.Utils;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Citities.Library.Test
{
    [TestFixture()]
    public class CsParserTest
    {
        [Test()]
        public void GetNamespaceTest()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs1.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                string nameSpace = csParser.GetNamespace(text);
                Assert.AreEqual("System", nameSpace);
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs2.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                string nameSpace = csParser.GetNamespace(text);
                Assert.AreEqual("System.Collections.Generic", nameSpace);
            }
        }

        [Test()]
        public void GetTypeTest()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs1.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                string type = csParser.GetClassName(CsParser.RemoveComments(text));
                Assert.AreEqual("String", type);
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs2.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                string type = csParser.GetClassName(CsParser.RemoveComments(text));
                Assert.AreEqual("Dictionary", type);
            }
        }

        [Test()]
        public void GetInheritanceTest()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs1.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                string test = CsParser.RemoveComments(text);
                string inheritance = csParser.GetInheritance(test);
                Assert.AreEqual("IComparable", inheritance);
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs2.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                string inheritance = csParser.GetInheritance(CsParser.RemoveComments(text));
                Assert.AreEqual("IDictionary<TKey,TValue>", inheritance);
            }
        }

        [Test()]
        public void GetAggragationsTest()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs1.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                List<string> aggragations = csParser.GetAggragations(CsParser.RemoveComments(text));
                string value = "int,char,int,int,int,String,String,String,String,String,int,int,int,int,String,int,String,String,String,String,String,String,String,String,String,String,string,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,String,TypeCode,CharEnumerator";
                Assert.AreEqual(value, string.Join(",", aggragations));

            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Citities.Library.Test.Files.Cs2.txt"))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();

                CsParser csParser = new CsParser();
                List<string> aggragations = csParser.GetAggragations(text);
                string value = string.Join(",", aggragations);
                Assert.AreEqual(value, string.Join(",", aggragations));
            }
        }
    }
}