using System;
using System.Collections.Generic;

namespace Cod2d
{
    public class Node
    {
        public double Value { get; set; }

        public List<Node>  Children { get; }

        public string Name { get; set; }

        public int Level { get; set; }

        public Node()
        {
            Children = new List<Node> ();
        }

        public Node(string name, double value, int level)
        {
            Children = new List<Node>();
            Name = name;
            Value = value;
            Level = level;
        }

        public void Add(Node node)
        {
            Children.Add (node);
        }

        public void Remove(Node node)
        {
            Children.Remove (node);
        }

        public void Remove(int index)
        {
            if(Children.Count > index)
                Remove(Children[index]);
        }
    }
}

