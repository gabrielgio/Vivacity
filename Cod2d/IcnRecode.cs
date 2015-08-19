using System;
using Vivacity.Library.Parser;
using System.Collections.Generic;
using Vivacity.Library.Model;
using Vivacity.Library.Builder;
using Vivacity.Library.Parser.GitHub;
using System.IO;
using Newtonsoft.Json;

namespace Cod2d
{
    public class IcnRecode
    {
        public delegate void Draw(double x, double y, double w, double h, double s);

        public Draw DrawLine;

        private double _width;

        private double _heigth;

        private double pixel_unit = 0.001796491228;

        private Project _project;

        public IcnRecode(Project project, double width, double heigth, Draw draw)
        {
            DrawLine = draw;
            _width = width;
            _heigth = heigth;
            _project = project;
        }

        public void Exec()
        {
            /* 
            Node city = new Node("unicon", 570000, 1);

            Node config = new Node("config", 2042, 2);

            city.Add(config);

            config.Add(new Node("unix", 1098, 3));

            Node uni = new Node("uni", 137000, 2);

            city.Add(uni);

            uni.Add(new Node("3d", 5581, 3));
            uni.Add(new Node("parser", 5293, 3));

            Node gui = new Node("gui", 37000, 3);

            uni.Add(gui);

            gui.Add(new Node("ivib", 16200, 4));
            gui.Add(new Node("guidemos", 5730, 4));

            uni.Add(new Node("lib", 20000, 3));

            Node udb = new Node("udb", 15200, 3);

            uni.Add(udb);

            udb.Add(new Node("lib", 3706, 4));
            udb.Add(new Node("dta", 3077, 4));

            uni.Add(new Node("ivib", 13000, 3));
            uni.Add(new Node("unicon", 10205, 3));
            uni.Add(new Node("iyacc", 8300, 3));
            uni.Add(new Node("ide", 8234, 3));
            uni.Add(new Node("xml", 5756, 3));
            uni.Add(new Node("unidoc", 1237, 3));
            uni.Add(new Node("ulex", 1245, 3));
            uni.Add(new Node("unidep", 1130, 3));
            uni.Add(new Node("native", 1098, 3));

            Node shell = new Node("shell", 1940, 3);
            uni.Add(shell);

            shell.Add(new Node("dist", 1940, 4));

            Node src = new Node("src", 195000, 2);

            city.Add(src);

            src.Add(new Node("iconc", 29000, 3));
            src.Add(new Node("rtt", 14400, 3));
            src.Add(new Node("icont", 13500, 3));
            src.Add(new Node("h", 12000, 3));
            src.Add(new Node("runtime", 87000, 3));
            src.Add(new Node("common", 10500, 3));
            src.Add(new Node("preproc", 6502, 3));
            src.Add(new Node("gdbm", 6983, 3));

            Node xpm = new Node("xpm", 11300, 3);

            src.Add(xpm);

            xpm.Add(new Node("lib", 10351, 4));
            xpm.Add(new Node("", 0, 4));

            src.Add(new Node("libtp", 3433, 3));

            Node ipl = new Node("ipl", 205000, 2);

            city.Add(ipl);

            Node gpacks = new Node("gpacks", 31000, 3);

            ipl.Add(gpacks);

            ipl.Add(new Node("mprocs", 2625, 3));
            ipl.Add(new Node("mprogs", 5034, 3));
            ipl.Add(new Node("procs", 46000, 3));
            ipl.Add(new Node("progs", 43000, 3));

            gpacks.Add(new Node("", 1019, 4));

            gpacks.Add(new Node("weaving", 11300, 4));
            gpacks.Add(new Node("vib", 4414, 4));
            gpacks.Add(new Node("htetris", 4286, 4));
            gpacks.Add(new Node("drawtree", 2763, 4));
            gpacks.Add(new Node("", 1960, 4));
            gpacks.Add(new Node("", 1721, 4));
            gpacks.Add(new Node("ged", 3654, 4));

            ipl.Add(new Node("gprogs", 29000, 3));
            ipl.Add(new Node("gprocs", 27000, 3));
            ipl.Add(new Node("gincl", 1405, 3));
            ipl.Add(new Node("cfuncs", 1874, 3));

            Node packs = new Node("packs", 17500, 3);

            ipl.Add(packs);

            packs.Add(new Node("", 1977, 4));
            packs.Add(new Node("", 2178, 4));
            packs.Add(new Node("ibpag2", 3745, 4));
            packs.Add(new Node("", 3521, 4));
            packs.Add(new Node("", 3112, 4));
            packs.Add(new Node("", 2085, 4));

            Node tests = new Node("tests", 19500, 2);

            city.Add(tests);

            tests.Add(new Node("graphics", 9770, 3));
            tests.Add(new Node("general", 4868, 3));
            tests.Add(new Node("bench", 1177, 3));
            */

            var root = _project.Root.Where((x, y, z) => x.Children.Count > 0);
            Render(root, 0, 0, _width, _heigth, 0);

        }

        public void Render(Node<Entity> node, double x, double y, double w, double h, int level)  
        {   
            double lw = Math.Log(300000, Math.E) - level*2;
            double myWidth = pixel_unit * 300000;

            if (level % 2 == 1)
            {
                double xA = x + w / 2 - myWidth / 2;
                double xB = x + w / 2 + myWidth / 2;

                DrawLine(xA, y + h / 2, xB, y + h / 2, lw);

                foreach (var item in node.Children)
                {
                    double x0 = xA + (xB - xA) * (node.Children.IndexOf(item)) / node.Children.Count;
                    double x1 = xA + (xB - xA) * node.Children.IndexOf(item) / node.Children.Count;
                    Render(item, x0, y, x1 - x0, h, level+1);
                }
            }
            else
            {
                double yA = y + h / 2 - myWidth / 2;
                double yB = y + h / 2 + myWidth / 2;
                DrawLine(x + w / 2, yA, x + w / 2, yB, lw);

                foreach (var item in node.Children)
                {
                    double y0 = yA + (yB - yA) * (node.Children.IndexOf(item)) / node.Children.Count;
                    double y1 = yA + (yB - yA) * node.Children.IndexOf(item) / node.Children.Count;
                    Render(item, x, y0, w, y1 - y0, level +1);
                }
            }
        }

        public void DrawText(string text, params double[] position)
        {

        }
    }
}

