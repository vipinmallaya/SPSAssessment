namespace SpsAssessment.Helpers.Abstractions
{
    public interface IFixedLengthContentDeserializer
    {
        T Deserialize<T>(string content, T returnObject);
    }
}
