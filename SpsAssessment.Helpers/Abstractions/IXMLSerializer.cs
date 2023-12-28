namespace SpsAssessment.Helpers.Abstractions
{
    public interface IXMLSerializer
    {
        public string Serialize<T>(T obj);
    }
}
