using System;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.ElementParsers
{
    public class PatentElementParser : IElementParser
    {
        public string ElementName => "patent";

        public ICatalogEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is null");
            }

            Patent patent = new Patent { Name = element.Attribute("name").Value };
            return patent;
        }
    }
}
