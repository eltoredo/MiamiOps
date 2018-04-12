﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiamiOps.Tests
{
    [TestFixture]
    public class TestXML
    {
        [Test]
        public void SimpleTest()
        {
            using (FileStream fs = File.OpenRead(@"..\..\..\tilemap.tmx"))
            using (StreamReader sr = new StreamReader(fs, true))
            {
                XElement xml = XElement.Load(sr);
                Console.WriteLine(xml.Descendants("data").Single().Value);
            }
        }

    }
}