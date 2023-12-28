namespace SpsAssessment.Helpers.Abstractions
{
    public interface IFixedLengthFileProcessor
    {
        Task<string> ProcessFileAsyc(string filePath);
    }
}
