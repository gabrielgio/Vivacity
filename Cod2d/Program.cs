using Gtk;
using System.Collections.Generic;
using Vivacity.Library.Parser;
using Vivacity.Library.Parser.GitHub;
using Vivacity.Library.Builder;
using Vivacity.Library.Model;
using System.IO;
using Newtonsoft.Json;

namespace Cod2d
{
    public class MainClass : Gtk.Window
    {
        private Project project;

        private static string _username = "";
        private static string _password = "";
        private string _owner = "JamesNK";
        private string _project = "Newtonsoft.Json";

        public static void Main(string[] args)
        {
            Application.Init();
            MainClass win = new MainClass();
            win.ShowAll();
            Application.Run();
        }

        public MainClass(): base("Cod2d")
        {
            SetDefaultSize(1920, 1080);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate
            {
                Application.Quit();
            };

            DrawingArea darea = new DrawingArea();
            darea.ExposeEvent += OnExpose;

            Add(darea);

            if (project == null)
            {

                string[] files =  {"csFiles.json", "csEntites.json", "csTree.json", "csProject.json", "csBuilding.json"};

                IParser csParser = new GitHubParser(_owner, _project, _username, _password);

                IEnumerable<IFile> csFiles;
                EntityCollection csEnitities;
                Node<Entity> csRoot;
                Node<Building> csBuilding;

                if (!File.Exists(files[0]))
                {
                    csFiles = csParser.Read();
                    SaveClass(csFiles, files[0]);
                }
                else
                    csFiles = ReadClass<IEnumerable<GitHubFile>>(files[0]);

                if (!File.Exists(files[1]))
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

                if (!File.Exists(files[4]))
                {
                    csBuilding = LayoutGenerator.GenerateSize(csRoot);
                    SaveClass(csBuilding, files[4]);
                }
                else
                    csBuilding = ReadClass<Node<Building>>(files[4]);

                Vivacity.Library.Model.Tree tree = new Vivacity.Library.Model.Tree
                    {
                        Root = csBuilding
                    };

                tree.Normalize(1920, 1080);

                project = new Project(csEnitities, csRoot, ProjectType.CSharp);
                SaveClass(project, files[3]);
            }

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

        protected override bool OnKeyReleaseEvent(Gdk.EventKey evnt)
        {
            if (evnt.Key == Gdk.Key.KP_Add)
            {
                scale += 0.5;
            }
            else if (evnt.Key == Gdk.Key.KP_Subtract)
            {
                scale -= 0.5;
            }
            else if (evnt.Key == Gdk.Key.Up)
            {
                Y += 50;
            }
            else if (evnt.Key == Gdk.Key.Down)
            {
                Y -= 50;
            }
            else if (evnt.Key == Gdk.Key.Left)
            {
                X += 50;
            }
            else if (evnt.Key == Gdk.Key.Right)
            {
                X -= 50;
            }

            QueueDraw();

            return base.OnKeyReleaseEvent(evnt);
        }

        double scale = 1;
        double X = 0;
        double Y = 0;

        void OnExpose(object sender, ExposeEventArgs args)
        {
            DrawingArea area = (DrawingArea) sender;
            Cairo.Context cr =  Gdk.CairoHelper.Create(area.GdkWindow);

            IcnRecode icn = new IcnRecode(project, 1920, 1080, (x, y, w, h, s) =>
                {
                    x*=scale;
                    y*=scale;
                    w*=scale;
                    h*=scale;

                    x+=X;
                    y+=Y;
                    w+=X;
                    h+=Y;

                    cr.Save();
                    
                    cr.SetSourceRGB(0, 0, 0);
                    cr.LineWidth = s * 0.7;
                    cr.MoveTo(x, y);
                    cr.LineTo(w, h);
                    cr.ClosePath();
                    cr.Stroke();

                    cr.Restore();
                });
            icn.Exec();

            cr.Dispose();
        }
    }
}
