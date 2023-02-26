using Moor.Core.SSH.Model;

namespace Moor.Core.SSH.Abstraction
{
    public interface ISshHelper
    {
        MediaUploadResult WriteFile(string fileName, string folderPath, string fileBase64);
        MediaUploadResult WriteAboutUsFile(string fileName, string folderPath, string fileBase64);
        string CreateHtmlFolder(string text);
        string CreateFolder(string fileDirectory);
        string ZipFolder(string folderName);
        string CreateDirectoryAndWriteInit(string fileDirectory, string text);
        string ReadHtmlFolder(string link);
        MediaUploadResult GetFileDirectory(string fileName, string folderPath);
    }
}
