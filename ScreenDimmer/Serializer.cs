using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Augustine.ScreenDimmer
{
    class Serializer
    {
        public static void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                DataContractSerializer serializer = new DataContractSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                   using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.ReadObject(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objectOut;
        }
    }
}
