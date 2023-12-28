using SpsAssessment.Helpers.Abstractions;
using System.Xml;
using System.Xml.Serialization;

namespace SpsAssessment.Helpers
{
    public class XMLSerializer : IXMLSerializer
    {
        public string Serialize<T>(T obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj);
                    return sww.ToString();
                }
            }
        }
    }
}
