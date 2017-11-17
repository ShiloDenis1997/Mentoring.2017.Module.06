using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Newspaper newspaper = new Newspaper { Name = element.Attribute("name").Value };
            return newspaper;
        }
    }
}
