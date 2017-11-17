using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;
using CatalogSystem.ElementParsers;

namespace CatalogSystem
{
    public class CatalogSystem
    {
        private static string _catalogElementName = "catalog";

        private readonly IDictionary<string, IElementParser> _elementParsers;

        public CatalogSystem()
        {
            _elementParsers = new Dictionary<string, IElementParser>();
        }

        public void AddParsers(params IElementParser[] elementParsers)
        {
            foreach (var parser in elementParsers)
            {
                _elementParsers.Add(parser.ElementName, parser);
            }
        }

        public IEnumerable<ICatalogEntity> ReadFrom(TextReader input)
        {
            XmlReader xmlReader = XmlReader.Create(input, new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            });

            xmlReader.ReadToFollowing(_catalogElementName);
            xmlReader.ReadStartElement();

            do
            {
                while (xmlReader.NodeType == XmlNodeType.Element)
                {
                    var node = XNode.ReadFrom(xmlReader) as XElement;
                    yield return _elementParsers[node.Name.LocalName].ParseElement(node);
                }
            } while (xmlReader.Read());
        }
    }
}
