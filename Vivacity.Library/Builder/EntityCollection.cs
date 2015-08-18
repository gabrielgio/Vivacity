using System;
using System.Collections.Generic;
using System.Linq;
using Vivacity.Library.Utils;

namespace Vivacity.Library.Builder
{
    /// <summary>
    /// A collection of <see cref="Cities.Library.Builder.Entity"/>.
    /// </summary>
    public class EntityCollection : IEnumerable<Entity>, ICollection<Entity>
    {
        private List<Entity> _entities;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get { return _entities.Count; } }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Gets the <see cref="Cities.Library.Builder.EntityCollection"/> with the specified i.
        /// </summary>
        /// <param name="i">The index.</param>
        public Entity this [int i] { get { return _entities.ElementAtOrDefault(i); } }

        /// <summary>
        /// Gets the <see cref="Cities.Library.Builder.EntityCollection"/> with the specified value.
        /// </summary>
        /// <param name="value">Value.</param>
        public Entity this [string value] { get { return _entities.FirstOrDefault(x => x.Type == value); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Builder.EntityCollection"/> class.
        /// </summary>
        public EntityCollection()
        {
            _entities = new List<Entity>();
        }

        /// <Docs>The item to add to the current collection.</Docs>
        /// <para>Adds an item to the current collection.</para>
        /// <remarks>To be added.</remarks>
        /// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
        /// <summary>
        /// Add the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public void Add(Entity entity)
        {
            if (_entities.Exists(x => x == entity))
                return;

            _entities.Add(entity);
        }

        /// <summary>
        /// Add the specified result, date and revision.
        /// </summary>
        /// <param name="result">Result.</param>
        /// <param name="date">Date.</param>
        /// <param name="revision">Revision.</param>
        public void Add(ParseResult result, DateTime date, long revision)
        {
            Add(result.Type, result.Namespace, date, result.Aggregations ,result.Inheritance, revision);
        }

        /// <summary>
        /// Add the specified type, nameSpace, date, agrregations and inheritance.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="nameSpace">Name space.</param>
        /// <param name="date">Date.</param>
        /// <param name="agrregations">Agrregations.</param>
        /// <param name="inheritance">Inheritance.</param>
        /// <param name="revisions">Revisions.</param>
        public void Add(string type, string nameSpace, DateTime date, IEnumerable<string> agrregations, string inheritance, long revisions)
        {
            Entity entity = _entities.FirstOrDefault(x => x.Type == type);

            if (entity == null)
            {
                entity = new Entity
                {
                    Type = type,
                    Namespace = nameSpace,
                    Revisions = revisions,
                    Aggregations = agrregations,
                    Inheritance = inheritance
                };

                _entities.Add(entity);
            }
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            _entities.Clear();
        }

        /// <Docs>The object to locate in the current collection.</Docs>
        /// <para>Determines whether the current collection contains a specific value.</para>
        /// <summary>
        /// Contains the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        public bool Contains(Entity item)
        {
            //loop through the inner ArrayList
            foreach (Entity obj in _entities)
            {
                //compare the BusinessObjectBase UniqueId property
                if (obj == item)
                {
                    //if it matches return true
                    return true;
                }
            }
            //no match
            return false;
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">Array.</param>
        /// <param name="arrayIndex">Array index.</param>
        public void CopyTo(Entity[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <Docs>The item to remove from the current collection.</Docs>
        /// <para>Removes the first occurrence of an item from the current collection.</para>
        /// <summary>
        /// Remove the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public bool Remove(int index)
        {
            return Remove(_entities.ElementAtOrDefault(index));
        }

        /// <Docs>The item to remove from the current collection.</Docs>
        /// <para>Removes the first occurrence of an item from the current collection.</para>
        /// <summary>
        /// Remove the specified type.
        /// </summary>
        /// <param name="type">Type.</param>
        public bool Remove(string type)
        {
            return Remove(_entities.FirstOrDefault(x => x.Type == type));
        }

        /// <Docs>The item to remove from the current collection.</Docs>
        /// <para>Removes the first occurrence of an item from the current collection.</para>
        /// <summary>
        /// Remove the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public bool Remove(Entity entity)
        {
            return _entities.Remove(entity);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<Entity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

    }
}

