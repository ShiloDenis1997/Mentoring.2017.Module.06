using System.Xml.Linq;

namespace CatalogSystem.Abstract
{
    public interface IElementParser
    {
        string ElementName { get; }
        ICatalogEntity ParseElement(XElement element);
    }
}
