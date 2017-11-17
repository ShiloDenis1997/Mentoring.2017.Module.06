using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;
using CatalogSystem.ElementParsers;
using NUnit.Framework;

namespace CatalogSystem.Test
{
    [TestFixture]
    public class CatalogSystemTest
    {
        private CatalogSystem _catalog;

        [SetUp]
        public void Init()
        {
            _catalog = new CatalogSystem();
            _catalog.AddParsers(new BookElementParser(), new NewsPaperElementParser(), new PatentElementParser());
        }

        [Test]
        public void Test_Books_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-8", null),
                    new XElement("catalog",
                        new XElement("book", new XAttribute("name", "A song of Ice and Fire")),
                        new XElement("book", new XAttribute("name", "Lord of the Rings"))
                    )).ToString());

            IEnumerable<ICatalogEntity> books = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(books, new[]
            {
                new Book {Name = "A song of Ice and Fire"},
                new Book {Name = "Lord of the Rings"}
            });
        }

        [Test]
        public void Test_Papers_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-8", null),
                    new XElement("catalog",
                        new XElement("newspaper", new XAttribute("name", "Times")),
                        new XElement("newspaper", new XAttribute("name", "Intex-press"))
                    )).ToString());

            IEnumerable<ICatalogEntity> newspapers = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                new Newspaper {Name = "Times"},
                new Newspaper {Name = "Intex-press"}
            });
        }

        [Test]
        public void Test_Patents_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-8", null),
                    new XElement("catalog",
                        new XElement("patent", new XAttribute("name", "Airplane")),
                        new XElement("patent", new XAttribute("name", "Vehicle"))
                    )).ToString());

            IEnumerable<ICatalogEntity> newspapers = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                new Patent {Name = "Airplane"},
                new Patent {Name = "Vehicle"}
            });
        }

        [Test]
        public void Test_MixedEntities_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-8", null),
                    new XElement("catalog",
                        new XElement("book", new XAttribute("name", "A song of Ice and Fire")),
                        new XElement("patent", new XAttribute("name", "Airplane")),
                        new XElement("newspaper", new XAttribute("name", "Times")),
                        new XElement("patent", new XAttribute("name", "Vehicle")),
                        new XElement("newspaper", new XAttribute("name", "Intex-press")),
                        new XElement("book", new XAttribute("name", "Lord of the Rings"))
                    )).ToString());

            IEnumerable<ICatalogEntity> newspapers = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new ICatalogEntity[]
            {
                new Book {Name = "A song of Ice and Fire"},
                new Patent {Name = "Airplane"},
                new Newspaper {Name = "Times"},
                new Patent {Name = "Vehicle"},
                new Newspaper {Name = "Intex-press"},
                new Book {Name = "Lord of the Rings"}
            });
        }
    }
}
