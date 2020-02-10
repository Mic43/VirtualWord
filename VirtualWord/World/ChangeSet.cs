using System;
using System.Collections.Generic;

namespace VirtualWord.World
{
    public class ChangeSet<T>
    {
        public enum Operation
        {
            Delete,
            Add
        }

        private readonly List<Tuple<Operation, T>> _changeSet = new List<Tuple<Operation, T>>();

        public void Insert(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _changeSet.Add(new Tuple<Operation, T>(Operation.Add,obj));
        }

        public void Delete(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _changeSet.Add(new Tuple<Operation, T>(Operation.Delete, obj));

        }

        public IReadOnlyCollection<Tuple<Operation, T>> GetChanges()
        {
            return _changeSet;
        }

        public void Clear()
        {
            _changeSet.Clear();
        }
    }
}