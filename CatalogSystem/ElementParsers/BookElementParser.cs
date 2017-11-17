using System;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.ElementParsers
{
    public class BookElementParser : IElementParser
    {
        public string ElementName => "book";

        public ICatalogEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is null");
            }

            Book book = new Book {Name = element.Attribute("name").Value};
            return book;
        }
    }
}
