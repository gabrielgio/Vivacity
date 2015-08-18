using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Vivacity.Library;
using Vivacity.Library.Parser.GitHub;
using Vivacity.Library.Parser;
using Vivacity.Library.Model;
using Vivacity.Library.Builder;
using Vivacity.Library.Parser.Directory;
using Newtonsoft.Json;
using Vivacity.Library.Utils;

namespace Cities.Console
{
    class MainClass
    {
        private static string _username = "gabrielgio";
        private static string _password = "Diablo@123";

        public static void Main(string[] args)
        {
            System.Console.WriteLine("Doing C#");
            Cs();
            System.Console.WriteLine("Doing Java");
            Java();
            System.Console.WriteLine("Doing Unicon");
            Icn();

           /* string fileString = File.ReadAllText("/Users/gabrielgiovaninidesouza/Downloads/sample.cs");
            fileString = CsParser.RemoveComments(fileString);
            File.WriteAllText("/Users/gabrielgiovaninidesouza/Downloads/noCommentsSample.cs", fileString);
            fileString = CsParser.RemoveChars(fileString);
            File.WriteAllText("/Users/gabrielgiovaninidesouza/Downloads/noCharSample.cs", fileString);
            fileString = CsParser.RemoveStrings(fileString);
            File.WriteAllText("/Users/gabrielgiovaninidesouza/Downloads/noStringSample.cs", fileString);
            System.Console.WriteLine(fileString);*/

        }

        private static void Cs()
        {
            string[] files =  {"csFiles.json", "csEntites.json", "csTree.json", "csProject.json"};

            IParser csParser = new GitHubParser("JamesNK", "Newtonsoft.Json", _username, _password);

            IEnumerable<IFile> csFiles;
            EntityCollection csEnitities;
            Node<Entity> csRoot;

            if (!File.Exists(files[0]))
            {
                csFiles = csParser.Read();
                SaveClass(csFiles, files[0]);
            }
            else
                csFiles = ReadClass<IEnumerable<GitHubFile>>(files[0]);

            if (true)
            {
                csEnitities = Project.MakeEntities(csFiles);
                SaveClass(csEnitities, files[1]);
            }
            else
                csEnitities = ReadClass<EntityCollection>(files[1]);

            if (!File.Exists(files[2]))
            {
                csRoot = Project.MakeTree(csEnitities);
                SaveClass(csRoot, files[2]);
            }
            else
                csRoot = ReadClass<Node<Entity>>(files[2]);

            Project project = new Project(csEnitities, csRoot, ProjectType.CSharp);
            SaveClass(project, files[3]);
        }

        private static void Icn()
        {
            string[] files =  {"icnFiles.json", "icnEntites.json", "icnTree.json", "icnProject.json"};

            IParser csParser = new DirectoryParser("/Users/gabrielgiovaninidesouza/Documents/Svn/cve-code/cve/src");

            IEnumerable<IFile> icnFiles;
            EntityCollection icnEnitities;
            Node<Entity> icnRoot;

            if (!File.Exists(files[0]))
            {
                icnFiles = csParser.Read();
                SaveClass(icnFiles, files[0]);
            }
            else
                icnFiles = ReadClass<IEnumerable<DirectoryFile>>(files[0]);

            if (!File.Exists(files[1]))
            {
                icnEnitities = Project.MakeEntities(icnFiles);
                SaveClass(icnEnitities, files[1]);
            }
            else
                icnEnitities = ReadClass<EntityCollection>(files[1]);

            if (!File.Exists(files[2]))
            {
                icnRoot = Project.MakeTree(icnEnitities);
                SaveClass(icnRoot, files[2]);
            }
            else
                icnRoot = ReadClass<Node<Entity>>(files[2]);

            Project project = new Project(icnEnitities, icnRoot, ProjectType.CSharp);
            SaveClass(project, files[3]);
        }


        private static void Java()
        {
            string[] files =  {"javaFiles.json", "javaEntites.json", "javaTree.json", "javaProject.json"};

            IParser csParser = new GitHubParser("LyndonChin", "AndroidRubberIndicator", _username, _password);

            IEnumerable<IFile> javaFiles;
            EntityCollection javaEnitities;
            Node<Entity> javaRoot;

            if (!File.Exists(files[0]))
            {
                javaFiles = csParser.Read();
                SaveClass(javaFiles, files[0]);
            }
            else
                javaFiles = ReadClass<IEnumerable<GitHubFile>>(files[0]);

            if (!File.Exists(files[1]))
            {
                javaEnitities = Project.MakeEntities(javaFiles);
                SaveClass(javaEnitities, files[1]);
            }
            else
                javaEnitities = ReadClass<EntityCollection>(files[1]);

            if (!File.Exists(files[2]))
            {
                javaRoot = Project.MakeTree(javaEnitities);
                SaveClass(javaRoot, files[2]);
            }
            else
                javaRoot = ReadClass<Node<Entity>>(files[2]);

            Project project = new Project(javaEnitities, javaRoot, ProjectType.CSharp);
            SaveClass(project, files[3]);
        }

        public static void SaveClass(object value, string fileName)
        {
            string json = JsonConvert.SerializeObject(value, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public static T ReadClass<T>(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
