using System;
using System.Linq;
using System.Collections.Generic;
using Vivacity.Library.Model;

namespace Vivacity.Library.Builder
{
    public class LayoutGenerator
    {
        public static City Generate(Node<Building> node)
        {
            City city = new City();
            Generate(node, city);
            return city;
        }

        public static Node<Building> GenerateSize(Node<Entity> root)
        {
            Queue<Node<Entity>> queue = new Queue<Node<Entity>>();
            Queue<Node<Building>> queueCopy = new Queue<Node<Building>>();

            Node<Building> copy = new Node<Building> { Value = Convert(root.Value) };

            queue.Enqueue(root);
            queueCopy.Enqueue(copy);

            while (queue.Any())
            {
                Node<Entity> node = queue.Dequeue();
                Node<Building> nodeCopy = queueCopy.Dequeue();

                foreach (var item in node.Children)
                {
                    Node<Building> newNode = new Node<Building> { Value = Convert(item.Value) };

                    nodeCopy.Add(newNode);
                    queueCopy.Enqueue(newNode);
                    queue.Enqueue(item);
              
                }
            }

            return  copy;
        }

        public static Building Convert(Entity entity)
        {
            if (entity == null)
                return null;
                
            return new Building
            {
                Size = new Size((double)entity.Aggregations.Count(), (double)entity.Aggregations.Count(), (double)entity.Revisions+1)
            };
        }

        private static void Generate(Node<Building> root, City city)
        {
            var stackX = new Stack<Tuple<Node<Building>, Position, int>>();

            stackX.Push(new Tuple<Node<Building>, Position, int>(root,new Position(200,200),1));

            bool u = true;
            double currentUp = 0;
            double currentDown = 0;
            double currentLeft = 0;
            double currentRigh = 0;

            while (stackX.Count > 0)
            {
                var node = stackX.Pop();

                currentUp = 0;
                currentDown = 0;
                currentLeft = 0;
                currentRigh = 0;

                foreach (var item in node.Item1.Children)
                {
                    if (item.Value != null)
                    {                        
                        item.Value.Position = GeneratePosition(u, ref currentUp, ref currentDown, ref currentLeft, ref currentRigh, node.Item3, item.Value.Size, node.Item2);
                        u = !u;
                        city.Buildings.Add(item.Value);
                    }
                    else
                    {
                        if (node.Item3 == 1)
                        {

                            Position position;
                            if (u)
                                position = new Position(node.Item2.X + currentUp, node.Item2.Y);
                            else
                                position = new Position(node.Item2.X + currentDown, node.Item2.Y);

                            var tuple = new Tuple<Node<Building>, Position, int>(item, position, u ? 2 : 4);
                            stackX.Push(tuple);
                        }
                        else if (node.Item3 == 3)
                        {
                            Position position;
                            if (u)
                                position = new Position(node.Item2.X - currentUp, node.Item2.Y);
                            else
                                position = new Position(node.Item2.X - currentDown, node.Item2.Y);

                            var tuple = new Tuple<Node<Building>, Position, int>(item, position, u ? 2 : 4);
                            stackX.Push(tuple);
                        }
                        else if (node.Item3 == 2)
                        {
                            Position position;

                            if (u)
                                position = new Position(node.Item2.X, node.Item2.Y + currentLeft);
                            else
                                position = new Position(node.Item2.X, node.Item2.Y + currentRigh);

                            var tuple = new Tuple<Node<Building>, Position, int>(item, position, u ? 1 : 3);
                            stackX.Push(tuple);
                        }
                        else if (node.Item3 == 4)
                        {
                            Position position;

                            if (u)
                                position = new Position(node.Item2.X, node.Item2.Y - currentLeft);
                            else
                                position = new Position(node.Item2.X, node.Item2.Y - currentRigh);

                            var tuple = new Tuple<Node<Building>, Position, int>(item, position, u ? 1 : 3);
                            stackX.Push(tuple);
                        }

                        u = !u;
                    }
                }
            }
        }

        private static Position GeneratePosition(bool u, ref double currentUp, ref double currentDown, ref double currentLeft, ref double currentRigth, int direction, Size size, Position position)
        {
            switch (direction)
            {
                case 1:
                    if (u)
                    {
                        currentUp += size.Width;
                        return new Position(position.X + currentUp - size.Width, position.Y+size.Length);
                    }
                    else
                    {
                        currentDown += size.Width;
                        return new Position(position.X+ currentDown - size.Width, position.Y);
                    }
                    break;
                case 2:
                    if (u)
                    {
                        currentLeft += size.Length;
                        return new Position(position.X - size.Width, position.Y + currentLeft - size.Length);
                    }
                    else
                    {
                        currentRigth += size.Length;
                        return new Position(position.X, position.Y + currentRigth - size.Length);
                    }
                    break;
                case 3:
                    if (u)
                    {
                        currentUp += size.Width;
                        return new Position(position.X - currentUp - size.Width, position.Y+size.Length);
                    }
                    else
                    {
                        currentDown += size.Width;
                        return new Position(position.X - currentDown - size.Width, position.Y);
                    }
                    break;
                case 4:
                    if (u)
                    {
                        currentLeft += size.Length;
                        return new Position(position.X - size.Width, position.Y - currentLeft - size.Length);
                    }
                    else
                    {
                        currentRigth += size.Length;
                        return new Position(position.X, position.Y - currentRigth - size.Length);
                    }
                    break;
                default:
                    return null;
            }
        }

        private static Size GetSize(Node<Building> node)
        {
            bool u = true;

            double currentUp = 0;
            double currentDown = 0;

            double maxHeigth = double.MinValue;
            double minHeigth = double.MaxValue;

            foreach (var item in node.Children)
            {
                if (item.Value != null)
                {
                    if (u)
                    {
                        currentUp += item.Value.Size.Width;

                        maxHeigth = maxHeigth < item.Value.Size.Length ? item.Value.Size.Length : maxHeigth;
                    }
                    else
                    {
                        currentDown += item.Value.Size.Width;

                        minHeigth = minHeigth > -item.Value.Size.Length ? -item.Value.Size.Length : -minHeigth;
                    }

                    u = !u;

                }
            }

            return new Size(currentUp > currentDown ? currentUp : currentDown, 1, maxHeigth - minHeigth);
        }
    }
}