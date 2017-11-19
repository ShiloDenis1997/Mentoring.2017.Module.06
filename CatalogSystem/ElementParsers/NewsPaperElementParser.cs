using System;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.ElementParsers
{
    public class NewsPaperElementParser : IElementParser
    {
        public string ElementName => "newspaper";

        public ICatalogEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is null");
            }

            NewsPaper newsPaper = new NewsPaper { Name = element.Attribute("name").Value };
            return newsPaper;
        }
    }
}
