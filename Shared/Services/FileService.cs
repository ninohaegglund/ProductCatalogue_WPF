using Shared.Interfaces;

namespace Shared.Services;

public class FileService : IFileService
{
    private readonly string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public bool SaveToFile(string content)
    {
        try
        {
           
            using var sw = new StreamWriter(_filePath);
            sw.WriteLine(content);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
            return false;
        }
    

    }

    public string LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                var content = sr.ReadToEnd();
                return content;
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return null!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not load file: {ex.Message}");
        }

        return null!;
    }
}