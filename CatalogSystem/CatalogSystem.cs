using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.Exceptions;

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
                        IElementParser parser;
                        if (_elementParsers.TryGetValue(node.Name.LocalName, out parser))
                        {
                            yield return parser.ParseElement(node);
                        }
                        else
                        {
                            throw new UnknownElementException($"Founded unknown element tag: {node.Name.LocalName}");
                        }
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
                    IEntityWriter writer;
                    if (_entityWriters.TryGetValue(catalogEntity.GetType(), out writer))
                    {
                        writer.WriteEntity(xmlWriter, catalogEntity);
                    }
                    else
                    {
                        throw new EntityWriterNotFoundedException($"Cannot find entity writer for type {catalogEntity.GetType().FullName}");
                    }
                }
                xmlWriter.WriteEndElement();
            }
        }
    }
}
