namespace Shared.Interfaces
{
    public interface IFileService
    {
        string LoadFromFile();
        bool SaveToFile(string content);
    }
}