using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;

namespace CatalogSystem
{
    public class CatalogSystem
    {
        private static string _catalogElementName = "catalog";

        private readonly IDictionary<string, IElementParser> _elementParsers;
        private readonly IDictionary<Type, IEntityWriter> _entityWriters;

        public CatalogSystem()
        {
            _elementParsers = new Dictionary<string, IElementParser>();
            _entityWriters = new Dictionary<Type, IEntityWriter>();
        }

        public void AddParsers(params IElementParser[] elementParsers)
        {
            foreach (var parser in elementParsers)
            {
                _elementParsers.Add(parser.ElementName, parser);
            }
        }

        public void AddWriters(params IEntityWriter[] writers)
        {
            foreach (var writer in writers)
            {
                _entityWriters.Add(writer.TypeToWrite, writer);
            }
        }

        public IEnumerable<ICatalogEntity> ReadFrom(TextReader input)
        {
            using (XmlReader xmlReader = XmlReader.Create(input, new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            }))
            {
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

        public void WriteTo(TextWriter output, IEnumerable<ICatalogEntity> catalogEntities)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(output, new XmlWriterSettings()))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(_catalogElementName);
                foreach (var catalogEntity in catalogEntities)
                {
                    _entityWriters[catalogEntity.GetType()].WriteEntity(xmlWriter, catalogEntity);
                }
                xmlWriter.WriteEndElement();
            }
        }
    }
}
