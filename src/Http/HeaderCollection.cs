using System.Collections.ObjectModel;

namespace Http
{
    public abstract class HeaderCollection : KeyedCollection<string, IHeader>, IHeaderCollection
    {
        protected override string GetKeyForItem(IHeader item)
        {
            return item.Name;
        }
    }
}