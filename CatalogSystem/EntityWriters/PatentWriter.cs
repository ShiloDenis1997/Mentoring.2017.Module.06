using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.EntityWriters
{
    public class PatentWriter : IEntityWriter
    {
        public Type TypeToWrite => typeof(Patent);

        public void WriteEntity(XmlWriter xmlWriter, ICatalogEntity entity)
        {
            Patent patent = entity as Patent;
            if (patent == null)
            {
                throw new ArgumentException($"provided {nameof(entity)} is null or not of type {nameof(Patent)}");
            }

            XElement element = new XElement("patent", 
                new XAttribute("name", patent.Name),
                new XAttribute("country", patent.Country),
                new XAttribute("registrationNumber", patent.RegistrationNumber),
                new XAttribute("filingDate", patent.FilingDate.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern)),
                new XAttribute("publishDate", patent.PublishDate.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern)),
                new XAttribute("pagesCount", patent.PagesCount),
                new XElement("note", patent.Note),
                new XElement("creators", patent.Creators.Select(
                    c => new XElement("creator", 
                        new XAttribute("name", c.Name),
                        new XAttribute("surname", c.Surname)))));
            element.WriteTo(xmlWriter);
        }
    }
}
