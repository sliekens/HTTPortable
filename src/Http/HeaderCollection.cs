using System;
using System.Collections.ObjectModel;

namespace Http
{
    public abstract class HeaderCollection : KeyedCollection<string, IHeader>, IHeaderCollection
    {
        public HeaderCollection()
            : base(StringComparer.Ordinal)
        {
        }

        protected override string GetKeyForItem(IHeader item)
        {
            return item.Name;
        }
    }
}