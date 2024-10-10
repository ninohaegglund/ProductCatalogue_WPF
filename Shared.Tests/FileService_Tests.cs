using Shared.Services;

namespace Shared.Tests;

public class FileService_Tests
{
    private readonly string _fakeFilePath = Path.Combine(Path.GetTempPath(),"fakeProducts.json");
    private readonly IFileService _fileService;

    public FileService_Tests()
    {
        _fileService = new FileService(_fakeFilePath);
    }
    [Fact]
    public void SaveToFile_ShouldSaveTextToJsonFile()
    {

    }
    [Fact]
    public void LoadFromFile_ShouldGetTextFromJsonFile()
    {

    }
}
