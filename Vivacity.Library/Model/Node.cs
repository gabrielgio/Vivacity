using System;
using System.Collections.Generic;
using System.Linq;

namespace Vivacity.Library.Model
{
    /// <summary>
    /// Node of <see cref="Cities.Library.Tree`1"/>
    /// </summary>
    public class Node<T>
    {
        /// <summary>
        /// Value of the <see cref="Cities.Library.Node"/>.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; set; }

        /// <summary>
        /// Children of this node
        /// </summary>
        /// <value>The children.</value>
        public List<Node<T>>  Children { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Node`1"/> class.
        /// </summary>
        public Node()
        {
            Children = new List<Node<T>> ();
        }

        /// <summary>
        /// Add a child node.
        /// </summary>
        /// <param name="node">Node to be added</param>
        public void Add(Node<T> node)
        {
            Children.Add (node);
        }

        /// <summary>
        /// Remove the specified node from children.
        /// </summary>
        /// <param name="node">Node.</param>
        public void Remove(Node<T> node)
        {
            Children.Remove (node);
        }

        /// <summary>
        /// Remove the specified index node from children based on index.
        /// </summary>
        /// <param name="index">Index.</param>
        public void Remove(int index)
        {
            if(Children.Count > index)
                Remove(Children[index]);
        }

        /// <summary>
        /// Depths-first search.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        public void DepthFirstSearch(Action<Node<T>,Node<T>, int> predicate)
        {
            Stack<Tuple<Node<T>,int>> stack = new Stack<Tuple<Node<T>, int>>();

            stack.Push(new Tuple<Node<T>, int>(this, 0));

            while (stack.Any())
            {
                Tuple<Node<T>,int> tuple = stack.Pop();

                foreach (var item in tuple.Item1.Children)
                {
                    stack.Push(new Tuple<Node<T>, int>(item, tuple.Item2 + 1));
                    predicate?.Invoke(item, tuple.Item1, tuple.Item2);
                }
            }
        }

        /// <summary>
        /// Breadths-first search.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        public void BreadthFirstSearch(Action<Node<T>,Node<T>, int> predicate)
        {
            Queue<Tuple<Node<T>,int>> queue = new Queue<Tuple<Node<T>, int>>();

            queue.Enqueue(new Tuple<Node<T>, int>(this, 0));

            while (queue.Any())
            {
                Tuple<Node<T>,int> tuple = queue.Dequeue();

                foreach (var item in tuple.Item1.Children)
                {
                    queue.Enqueue(new Tuple<Node<T>, int>(item, tuple.Item2 + 1));
                    predicate?.Invoke(item, tuple.Item1, tuple.Item2);
                }
            }
        }


        /// <summary>
        /// Where the specified predicate.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        public Node<T> Where (Func<Node<T>, Node<T>,int,bool> predicate)
        {
            if (predicate == null)
                return this;

            Queue<Node<T>> queue = new Queue<Node<T>>();
            Queue<Node<T>> queueCopy = new Queue<Node<T>>();
            Queue<int> queueLevel = new Queue<int>();

            Node<T> copy = new Node<T> { Value = this.Value };

            queue.Enqueue(this);
            queueCopy.Enqueue(copy);
            queueLevel.Enqueue(0);

            while (queue.Any())
            {
                Node<T> node = queue.Dequeue();
                Node<T> nodeCopy = queueCopy.Dequeue();
                int level = queueLevel.Dequeue();

                foreach (var item in node.Children)
                {
                    if (predicate(item, node,level))
                    {
                        Node<T> newNode = new Node<T> { Value = item.Value };

                        nodeCopy.Add(newNode);
                        queueCopy.Enqueue(newNode);
                        queue.Enqueue(item);
                        queueLevel.Enqueue(level + 1);
                    }
                }
            }

            return copy;
        }
    }
}
