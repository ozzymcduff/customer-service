﻿using System;
using NUnit.Framework;
using Customers;
using System.IO;


namespace Tests
{
    [TestFixture]
    public class ParseXmlFromRails
    {
        private string xml;
        [SetUp]
        public void BeforeEachTest()
        {
            xml= @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ArrayOfCustomer xmlns=""http://schemas.datacontract.org/2004/07/Customers"">
    <Customer>
        <AccountNumber>1</AccountNumber>
        <AddressCity nil=""true""/>
        <AddressCountry nil=""true""/>
        <AddressStreet nil=""true""/>
        <FirstName>Oskar</FirstName>
        <Gender>Male</Gender>
        <LastName>Gewalli</LastName>
        <PictureUri nil=""true""/>
    </Customer>
</ArrayOfCustomer>";
        }

        [Test]
        public void CanParse()
        {
            ArrayOfCustomer customers;
            var serializer = new Serializer();
            using (var stream = new MemoryStream())
            {
                var writer=new StreamWriter(stream);
                writer.Write(xml);
                writer.WriteLine();
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                customers= serializer.Deserialize<ArrayOfCustomer>(stream);
            }
            Assert.That(customers.Customer.Length, Is.EqualTo(1));
        }
    }
}

