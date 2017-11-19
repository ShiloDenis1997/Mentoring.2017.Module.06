using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.EntityWriters
{
    public class NewsPaperWriter : BaseXmlEntityWriter
    {
        public override Type TypeToWrite => typeof(NewsPaper);

        public override void WriteEntity(XmlWriter xmlWriter, ICatalogEntity entity)
        {
            NewsPaper newsPaper = entity as NewsPaper;
            if (newsPaper == null)
            {
                throw new ArgumentException($"provided {nameof(entity)} is null or not of type {nameof(NewsPaper)}");
            }

            XElement element = new XElement("newspaper");
            AddAttribute(element, "name", newsPaper.Name, true);
            AddAttribute(element, "publicationCity", newsPaper.PublicationCity);
            AddAttribute(element, "publisherName", newsPaper.PublisherName);
            AddAttribute(element, "publishYear", newsPaper.PublishYear.ToString());
            AddAttribute(element, "pagesCount", newsPaper.PagesCount.ToString());
            AddAttribute(element, "date", newsPaper.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture));
            AddAttribute(element, "issn", newsPaper.IssnNumber, true);
            AddAttribute(element, "number", newsPaper.Number.ToString());
            AddElement(element, "note", newsPaper.Note);
            element.WriteTo(xmlWriter);
        }
    }
}
