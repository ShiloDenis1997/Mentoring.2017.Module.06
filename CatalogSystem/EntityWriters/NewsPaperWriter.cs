using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.EntityWriters
{
    public class NewsPaperWriter : IEntityWriter
    {
        public Type TypeToWrite => typeof(NewsPaper);

        public void WriteEntity(XmlWriter xmlWriter, ICatalogEntity entity)
        {
            NewsPaper newsPaper = entity as NewsPaper;
            if (newsPaper == null)
            {
                throw new ArgumentException($"provided {nameof(entity)} is null or not of type {nameof(NewsPaper)}");
            }

            XElement element = new XElement("newspaper", 
                new XAttribute("name", newsPaper.Name),
                new XAttribute("publicationCity", newsPaper.PublicationCity),
                new XAttribute("publisherName", newsPaper.PublisherName),
                new XAttribute("publishYear", newsPaper.PublishYear),
                new XAttribute("pagesCount", newsPaper.PagesCount),
                new XAttribute("date", newsPaper.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern)),
                new XAttribute("issn", newsPaper.IssnNumber),
                new XElement("note", newsPaper.Note));
            element.WriteTo(xmlWriter);
        }
    }
}
