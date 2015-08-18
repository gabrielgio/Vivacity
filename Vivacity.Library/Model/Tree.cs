using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vivacity.Library.Model
{
    /// <summary>
    /// Tree.
    /// </summary>
    public class Tree
    {

        /// <summary>
        /// Root of the tree
        /// </summary>
        /// <value>The root.</value>
        public Node<Building> Root { get; set; }

        public Tree ()
        {
        }

        /// <summary>
        /// Normalize the tree based on a width and heigth.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="heigth">Heigth.</param>
        public void Normalize(double width, double heigth)
        {
            double normilizedSize = Math.Sqrt(width * heigth);

            Stack<Tuple<Node<Building>, double>> stack = new Stack<Tuple<Node<Building>, double>> ();

            stack.Push(new Tuple<Node<Building>, double>(Root, normilizedSize));

            while (stack.Count > 0)
            {
                Tuple<Node<Building>, double> tuple = stack.Pop();

                foreach (Node<Building> item in tuple.Item1.Children)
                {
                    double w  = SizeOf(item)/SizeOf(tuple.Item1) * tuple.Item2;
                    if (item.Value != null)
                    {
                        item.Value.Size = new Size(w, item.Value.Size.Heigth, normilizedSize);
                        Normalize(item.Value);
                    }
                    stack.Push(new Tuple<Node<Building>, double>(item, w));
                }
            }
        }

        private void Normalize(Building parallelepiped)
        {
            if (parallelepiped.Size.Ratio == 1)
                return;

            double side = Math.Sqrt(parallelepiped.Size.Width * parallelepiped.Size.Length);
            parallelepiped.Size.Length = parallelepiped.Size.Width = side;
        }

        private double SizeOf(Node<Building> node)
        {
            if (node == null)
                return 0;

            double count = node.Value == null ? 0 : node.Value.Size.Width * node.Value.Size.Length;

            foreach (Node<Building> item in node.Children) 
                count += SizeOf (item);

            return count;
        }
    }
}

