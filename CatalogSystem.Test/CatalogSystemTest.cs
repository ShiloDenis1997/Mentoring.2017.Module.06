using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;
using CatalogSystem.ElementParsers;
using CatalogSystem.EntityWriters;
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
            _catalog.AddWriters(new BookWriter(), new NewsPaperWriter(), new PatentWriter());
        }

        [Test]
        public void Test_Books_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-16", null),
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

            sr.Dispose();
        }

        [Test]
        public void Test_Papers_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-16", null),
                    new XElement("catalog",
                        new XElement("newspaper", new XAttribute("name", "Times")),
                        new XElement("newspaper", new XAttribute("name", "Intex-press"))
                    )).ToString());

            IEnumerable<ICatalogEntity> newspapers = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                new NewsPaper {Name = "Times"},
                new NewsPaper {Name = "Intex-press"}
            });

            sr.Dispose();
        }

        [Test]
        public void Test_Patents_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-16", null),
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

            sr.Dispose();
        }

        [Test]
        public void Test_MixedEntities_Read()
        {
            TextReader sr = new StringReader(
                new XDocument(new XDeclaration("1.0", "utf-16", null),
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
                new NewsPaper {Name = "Times"},
                new Patent {Name = "Vehicle"},
                new NewsPaper {Name = "Intex-press"},
                new Book {Name = "Lord of the Rings"}
            });

            sr.Dispose();
        }

        [Test]
        public void Test_MixedEntities_Write()
        {
            StringBuilder actualResult = new StringBuilder();
            StringWriter sw = new StringWriter(actualResult);
            var book = CreateBook();
            var newspaper = CreateNewspaper();
            var patent = CreatePatent();
            var entities = new ICatalogEntity[]
            {
                book,
                newspaper,
                patent
            };
            string expectedResult = @"<?xml version=""1.0"" encoding=""utf-16""?>" +
                "<catalog>" +
                    @"<book name=""A song of Ice and Fire"" " +
                        @"publicationCity=""New-York"" " +
                        @"publisherName=""Typography"" " +
                        @"publishYear=""1999"" " +
                        @"pagesCount=""500"" " +
                        @"isbn=""978-1-56619-909-4"">" +
                        "<note>This book is about history of Seven Kingdom.</note>" +
                        "<authors>" +
                            @"<author name=""George"" surname=""Martin"" />" +
                        "</authors>" +
                    "</book>" +
                    "<newspaper name=\"Times\" " +
	                    "publicationCity=\"London\" " +
	                    "publisherName=\"London typography\" " +
	                    "publishYear=\"1904\" " +
	                    "pagesCount=\"14\" " +
	                    "date=\"05.16.1905\" " + 
	                    "issn=\"0317-8471\">" +
	                    "<note>The most popular London newspaper</note>" +
                    "</newspaper>" +
                    "<patent name=\"Airplane\" " +
	                    "country=\"USA\" " +
                        "registrationNumber=\"D0000126\" " +
	                    "filingDate=\"12.24.1905\" " +
	                    "publishDate=\"01.20.1906\" " +
	                    "pagesCount=\"100\">" +
	                    "<note>First airplane in the world</note>" +
	                    "<creators>" +
		                    "<creator name=\"Orville\" surname=\"Wright\" />" +
		                    "<creator name=\"Wilbur\" surname=\"Wright\" />" +
	                    "</creators>" +
                    "</patent>" +
                "</catalog>";

            _catalog.WriteTo(sw, entities);
            sw.Dispose();

            Assert.AreEqual(expectedResult.ToString(), actualResult.ToString());
        }

        private NewsPaper CreateNewspaper()
        {
            return new NewsPaper
            {
                Name = "Times",
                PublicationCity = "London",
                PublisherName = "London typography",
                PublishYear = 1904,
                PagesCount = 14,
                Date = new DateTime(1905, 5, 16),
                IssnNumber = "0317-8471",
                Note = "The most popular London newspaper"
            };
        }

        private Patent CreatePatent()
        {
            return new Patent
            {
                Name = "Airplane",
                Country = "USA",
                RegistrationNumber = "D0000126",
                FilingDate = new DateTime(1905, 12, 24),
                PublishDate = new DateTime(1906, 1, 20),
                PagesCount = 100,
                Note = "First airplane in the world",
                Creators = new List<Creator>
                {
                    new Creator {Name = "Orville", Surname = "Wright"},
                    new Creator {Name = "Wilbur", Surname = "Wright"}
                }
            };
        }

        private Book CreateBook()
        {
            return new Book
            {
                Name = "A song of Ice and Fire",
                PublicationCity = "New-York",
                PublisherName = "Typography",
                PublishYear = 1999,
                PagesCount = 500,
                IsbnNumber = "978-1-56619-909-4",
                Note = "This book is about history of Seven Kingdom.",
                Authors = new List<Author>
                {
                    new Author {Name = "George", Surname = "Martin"}
                }
            };
        }
    }
}
