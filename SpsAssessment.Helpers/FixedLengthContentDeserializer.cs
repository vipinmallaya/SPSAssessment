using SpsAssessment.Helpers.Abstractions;
using SpsAssessment.Helpers.Attributes;
using System.Reflection;

namespace SpsAssessment.Helpers
{
    public class FixedLengthContentDeserializer : IFixedLengthContentDeserializer
    {
        public T Deserialize<T>(string content, T returnObject)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                foreach (object attr in property.GetCustomAttributes())
                {
                    if (attr is FixedSizeFieldAttribute)
                    {
                        var fieldAttribute = (FixedSizeFieldAttribute)attr;
                        var stringContent = content.Substring(fieldAttribute.index, fieldAttribute.length).Trim();
                        if (!string.IsNullOrEmpty(stringContent))
                        {
                            property.SetValue(returnObject, stringContent);
                        }
                    }
                }
            }

            return returnObject;
        }
    }
}