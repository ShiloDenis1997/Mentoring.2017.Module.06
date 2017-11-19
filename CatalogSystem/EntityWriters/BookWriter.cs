using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.EntityWriters
{
    public class BookWriter : IEntityWriter
    {
        public Type TypeToWrite => typeof(Book);

        public void WriteEntity(XmlWriter xmlWriter, ICatalogEntity entity)
        {
            Book book = entity as Book;
            if (book == null)
            {
                throw new ArgumentException($"provided {nameof(entity)} is null or not of type {nameof(Book)}");
            }

            XElement element = new XElement("book",
                new XAttribute("name", book.Name ?? ""),
                new XAttribute("publicationCity", book.PublicationCity ?? ""),
                new XAttribute("publisherName", book.PublisherName ?? ""),
                new XAttribute("publishYear", book.PublishYear),
                new XAttribute("pagesCount", book.PagesCount),
                new XAttribute("isbn", book.IsbnNumber ?? ""),
                new XElement("note", book.Note ?? ""),
                new XElement("authors",
                    book.Authors?.Select(a => new XElement("author",
                        new XAttribute("name", a.Name),
                        new XAttribute("surname", a.Surname)
                    ))
                ));

            element.WriteTo(xmlWriter);
        }
    }
}
