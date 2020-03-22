using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Managers
{
    /// <summary>
    /// Serialization and deserialization between string and object containing xml data.
    /// </summary>
    public static class XmlSerializerManager
    {
        #region Methods

        /// <summary>
        /// Serializes an object representing xml data to a string containing the xml data.
        /// </summary>
        /// <typeparam name="T">The object type representing xml data to serialize to a string containing the xml data.</typeparam>
        /// <param name="objectToSerialize">The object representing xml data to serialize to a string containing the xml data.</param>
        /// <returns>The string containing the xml data.</returns>
        public static string SerializeToXmlString<T>(T objectToSerialize)
        {
            using (var stringWriter = new StringWriter())
            {
                var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());
                xmlSerializer.Serialize(stringWriter, objectToSerialize);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Serializes an object representing xml data to a string containing the xml data with the ability to set the encoding.
        /// </summary>
        /// <typeparam name="T">The object type representing xml data to serialize to a string containing the xml data.</typeparam>
        /// <param name="objectToSerialize">The object representing xml data to serialize to a string containing the xml data.</param>
        /// <param name="encoding">The encoding type of the string.</param>
        /// <returns>The string containing the xml data.</returns>
        public static string SerializeToXmlString<T>(T objectToSerialize, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream())
            {
                var xmlWriterSettings = new XmlWriterSettings()
                {
                    Encoding = encoding,
                    Indent = true,
                    NewLineOnAttributes = true,
                };
                using (var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
                {
                    var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());
                    xmlSerializer.Serialize(xmlWriter, objectToSerialize);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Deserializes a string containing xml data to an object representing the xml data.
        /// </summary>
        /// <typeparam name="T">The object type representing xml data to be deserialized to from a string containing the xml data.</typeparam>
        /// <param name="stringToSerialize">The string containing xml data to deserialize to an object representing the xml data.</param>
        /// <returns>The object representing the xml data.</returns>
        public static T DeserializeToObject<T>(string stringToSerialize)
        {
            using (var stringReader = new StringReader(stringToSerialize))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// Deserializes a string containing xml data to an object representing the xml data with the ability to set the encoding.
        /// </summary>
        /// <typeparam name="T">The object type representing xml data to be deserialized to from a string containing the xml data.</typeparam>
        /// <param name="stringToSerialize">The string containing xml data to deserialize to an object representing the xml data.</param>
        /// <param name="encoding">The encoding type of the string.</param>
        /// <returns>The object representing the xml data.</returns>
        public static T DeserializeToObject<T>(string stringToSerialize, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream(encoding.GetBytes(stringToSerialize)))
            {
                using (var streamReader = new StreamReader(memoryStream, encoding))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(streamReader);
                }
            }
        }

        /// <summary>
        /// Deserializes a string containing xml data to an object representing the xml data with the ability to set the encoding.
        /// </summary>
        /// <typeparam name="T">The object type representing xml data to be deserialized to from a string containing the xml data.</typeparam>
        /// <param name="byteArray">The byte array data to deserialize to an object representing the xml data.</param>
        /// <param name="encoding">The encoding type of the string.</param>
        /// <returns>The object representing the xml data.</returns>
        public static T DeserializeToObject<T>(byte[] byteArray, Encoding encoding)
        {
            using (var memoryStream = new MemoryStream(byteArray))
            {
                using (var streamReader = new StreamReader(memoryStream, encoding))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(streamReader);
                }
            }
        }

        #endregion Methods
    }
}
