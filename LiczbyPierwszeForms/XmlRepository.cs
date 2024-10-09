using System.Xml.Linq;
using System;
using System.IO;
using System.Numerics;

namespace LiczbyPierwszeForms
{
    public class XmlRepository
    {
        public void ZapiszDaneDoXML(int cycle, TimeSpan cycleTimer, BigInteger primaryNumber)
        {
            string sciezkaPliku = "LiczbyPierwsze.xml";
            XDocument doc;

            if (File.Exists(sciezkaPliku))
            {
                doc = XDocument.Load(sciezkaPliku);
            }
            else
            {
                doc = new XDocument(new XElement("Cykl"));
            }

            XElement root = doc.Element("Cykl");

            XElement elementCyklu = new XElement("DaneCyklu",
                new XElement("NumerCyklu", cycle),
                new XElement("CzasTrwaniaCyklu", cycleTimer.ToString()),
                new XElement("LiczbaPierwsza", primaryNumber.ToString()),
                new XElement("CzasWyznaczenia", DateTime.Now.ToString("o"))
            );

            root.Add(elementCyklu);
            doc.Save(sciezkaPliku);
        }
    }
}
