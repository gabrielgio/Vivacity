using System;
using System.Collections.Generic;
using Vivacity.Library.Parser;
using System.Linq;
using System.IO;
using Vivacity.Library.Model;
using Vivacity.Library.Utils;
using System.Threading.Tasks;

namespace Vivacity.Library.Builder
{
    /// <summary>
    /// An project of a repo.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public EntityCollection Entities { get; private set; }

        /// <summary>
        /// Gets the root.
        /// </summary>
        /// <value>The root.</value>
        public Node<Entity> Root{ get; private set; }

        /// <summary>
        /// Gets the type of the project.
        /// </summary>
        /// <value>The type of the project.</value>
        public ProjectType ProjectType { get; private set; }

        private Project()
        {
            Entities = new EntityCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Builder.Project"/> class.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="root">Root.</param>
        /// <param name="projectType">Project type.</param>
        public Project(EntityCollection entities, Node<Entity> root, ProjectType projectType)
        {
            Entities = entities;
            Root = root;
            ProjectType = projectType;
        }

        /// <summary>
        /// Creates a collection of entities from a collection go IFiles.
        /// </summary>
        /// <returns>The entities.</returns>
        /// <param name="files">Files.</param>
        public static EntityCollection MakeEntities(IEnumerable<IFile> files)
        {
            EntityCollection entities = new EntityCollection();

            ParallelOptions parallelOptions = new ParallelOptions
            {
                    MaxDegreeOfParallelism = 8
            };

            switch (CheckProjectType(files))
            {
                case ProjectType.CSharp:
                    CsParser csParser = new CsParser();
                    Parallel.ForEach(files.Where(x => Path.GetExtension(x.Path) == ".cs"), parallelOptions, item => {
                        entities.Add(csParser.Parse(item), item.Date.DateTime, item.Revisions);
                    });
                    break;

                case ProjectType.Unicon:
                    IcnParser icnParser = new IcnParser();
                    Parallel.ForEach(files.Where(x => Path.GetExtension(x.Path) == ".icn"), parallelOptions, item => {
                        entities.Add(icnParser.Parse(item), item.Date.DateTime, item.Revisions);
                    });
                    break;

                case ProjectType.Java:
                    JavaParser javaParser = new JavaParser();
                    Parallel.ForEach(files.Where(x => Path.GetExtension(x.Path) == ".java"), parallelOptions, item => {
                        entities.Add(javaParser.Parse(item), item.Date.DateTime, item.Revisions);
                    });
                    break;
            }

            return entities;
        }

        /// <summary>
        /// Makes based in entities.
        /// </summary>
        /// <returns>The tree.</returns>
        /// <param name="entities">Entities.</param>
        public static Node<Entity> MakeTree(IEnumerable<Entity> entities)
        {
            Node<Entity> root = new Node<Entity>();

            var queue = new Queue<Tuple<string,Node<Entity>>>();

            queue.Enqueue(new Tuple<string, Node<Entity>>(GetRootNamespace(entities), root));

            while (queue.Count > 0)
            {
                var tuple = queue.Dequeue();

                foreach (var item in EntitiesFromNameSpace(tuple.Item1,entities))
                    tuple.Item2.Add(new Node<Entity>{ Value = item });
                

                foreach (var item in NextNameSpaces(tuple.Item1,entities))
                {
                    Node<Entity> node = new Node<Entity>();

                    tuple.Item2.Add(node);

                    queue.Enqueue(new Tuple<string, Node<Entity>>(item, node));
                }
            }

            return root;
        }

        /// <summary>1
        /// Creates project from Files.
        /// </summary>
        /// <returns>The project.</returns>
        /// <param name="files">Files.</param>
        public static Project CreateProject(IEnumerable<IFile> files)
        {
            EntityCollection entities = MakeEntities(files);

            return new Project
            { 
                Entities = entities,
                Root = MakeTree(entities)
            };
        }

        private static ProjectType CheckProjectType(IEnumerable<IFile> files)
        {
            if (files.Count(x => Path.GetExtension(x.Path) == ".cs") > 0)
                return ProjectType.CSharp;

            if (files.Count(x => Path.GetExtension(x.Path) == ".icn") > 0)
                return ProjectType.Unicon;

            if (files.Count(x => Path.GetExtension(x.Path) == ".java") > 0)
                return ProjectType.Java;

            return ProjectType.Unknown;
        }

        private static string GetRootNamespace(IEnumerable<Entity> entities)
        {
            var namspaces = entities.Where(Where).Select(Select);

            return namspaces.ElementAt(0)[0];
        }

        private static bool Where(Entity value)
        {
            if (value == null)
                return false;

            return !string.IsNullOrEmpty(value.Namespace);
        }

        private static string[] Select(Entity value)
        {
            return value.Namespace.Split('.');
        }

        private static IEnumerable<Entity> EntitiesFromNameSpace(string nameSpace, IEnumerable<Entity> entities)
        {
            return entities.Where(x => x.Namespace == nameSpace);
        }

        private static IEnumerable<string> NextNameSpaces(string nameSpace, IEnumerable<Entity> entities)
        {
            string[] baseNameSpace = nameSpace.Split('.');

            IEnumerable<string[]> namespaces = entities.Select(Select);

            List<string> results = new List<string>();

            foreach (var item in namespaces)
            {
                bool valid = true;
                if(item.Length <= baseNameSpace.Length)
                    continue;
                
                int i = 0;

                for (; i < baseNameSpace.Length; i++)
                {
                    if (item[i] != baseNameSpace[i])
                        valid = false;
                }

                if (valid)
                {
                    string value = string.Join(".", item.Take(i + 1));
                    if (!results.Contains(value))
                        results.Add(value);
                }
            }

            return results;
        }
    }
}

