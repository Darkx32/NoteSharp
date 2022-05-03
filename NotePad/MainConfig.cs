using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace NotePad
{
    class MainConfig
    {

        string DefaultDirectory = "Save";
        static string DefaultFile = "Save/Config.xml";

        static string rootName = "Configurations";
        public MainConfig()
        {

            if (!Directory.Exists(DefaultDirectory))
            {
                Directory.CreateDirectory(DefaultDirectory);
            } if (!File.Exists(DefaultFile))
            {
                //File.Create(DefaultFile);
                CreateXMLConfig();
            }
        }

        private void CreateXMLConfig()
        {
            XmlTextWriter textWriter = new XmlTextWriter(DefaultFile, Encoding.UTF8);

            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = 2;

            textWriter.WriteStartDocument();
            textWriter.WriteStartElement(rootName);

            textWriter.WriteStartElement("config");
            textWriter.WriteAttributeString("id", "TextSize");
            textWriter.WriteAttributeString("value", "10");
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("config");
            textWriter.WriteAttributeString("id", "TextFont");
            textWriter.WriteAttributeString("value", "Microsoft Sans Serif");
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();

            textWriter.Flush();
            textWriter.Close();
        }

        public static string GetValue(ConfigType type)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(DefaultFile));
            string retur = "";
            int i = 0;
            foreach(XmlElement element in doc.DocumentElement.GetElementsByTagName("config"))
            {
                if (i == ((int)type))
                {
                    break;
                }
                retur = element.GetAttribute("value");
                i++;
            }

            return retur;
        }

        public static void SetValue(ConfigType type, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(DefaultFile));

            int i = 0;
            foreach(XmlElement element in doc.DocumentElement.GetElementsByTagName("config"))
            {
                if (i == (int) type - 1) { element.SetAttribute("value", value); break; }
                i++;
            }
            File.WriteAllText(DefaultFile, doc.OuterXml);
        }

        public enum ConfigType
        {
            TEXTSIZE = 1,
            TEXTFONT,
        }
    }
}
