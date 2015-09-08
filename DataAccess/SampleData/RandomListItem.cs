using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.SampleData
{
    public abstract class RandomListItem<T>
    {
        private IList<T> _list;
        private readonly Random _rnd;

        protected RandomListItem()
        {
            _rnd = new Random(Environment.TickCount);
            _list = CreateList();
        }

        public abstract IList<T> CreateList();

        public T Get()
        {
            if (!_list.Any())
            {
                _list = CreateList();
            }

            int at = _rnd.Next(0, _list.Count - 1);
            T item = _list[at];
            _list.RemoveAt(at);
            return item;
        }
    }
}
