using Moor.Core.SSH.Abstraction;
using Moor.Core.SSH.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Moor.Core.SSH.Concretion
{
    public class SshHelper : ISshHelper
    {
        private readonly IOptions<AppSettings> appSettings;
        private FileSetting fileSetting { get; set; } = new FileSetting();

        public SshHelper(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
            this.fileSetting = this.appSettings.Value.PrivateInsuranceSettings.FileSetting;
        }

        public string CreateDirectoryAndWriteInit(string fileDirectory, string text)
        {
            string fileUrl = string.Empty;
            string extension = ".html";
            try
            {
                FileStream fileStream = new FileStream(fileDirectory + extension, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(text);
                writer.Close();
                fileStream.Close();

                return fileSetting.FileUploadFile + "/" + fileDirectory + "/" + fileDirectory + extension; ;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string CreateFolder(string fileDirectory)
        {
            try
            {
                string mainRoot = fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + fileDirectory;

                if (!Directory.Exists(mainRoot))
                {
                    Directory.CreateDirectory(mainRoot);
                }

                return mainRoot;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string CreateHtmlFolder(string text)
        {
            string fileUrl = string.Empty;

            try
            {
                string fileName = Guid.NewGuid().ToString().Substring(0, 7);
                string folderPath = fileName;
                string extension = ".html";

                string mainRoot = fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + folderPath;

                if (!Directory.Exists(mainRoot))
                {
                    Directory.CreateDirectory(mainRoot);
                }

                FileStream fileStream = new FileStream(mainRoot + "/" + fileName + extension, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(text);
                writer.Close();
                fileStream.Close();

                //byte[] bytes = File.ReadAllBytes(mainRoot+"/"+fileName + extension);
                //fileBase64 = Convert.ToBase64String(bytes);

                fileUrl = fileSetting.FileUploadFile + "/" + folderPath + "/" + fileName + extension;
            }
            catch (Exception ex)
            { }

            return fileUrl;
        }

        public string ReadHtmlFolder(string link)
        {
            string text = string.Empty;

            try
            {
                FileStream fileStream = new FileStream(fileSetting.FileMainRoot + "/" + link, FileMode.Open, FileAccess.Read);


                using (StreamReader sr = new StreamReader(fileStream))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        text += line;
                    }
                }
                fileStream.Close();
            }
            catch (Exception ex)
            {
                return text;
            }

            return text;
        }

        public MediaUploadResult WriteFile(string fileName, string folderPath, string fileBase64)
        {
            MediaUploadResult result = new MediaUploadResult { IsSuccess = true };
            folderPath = CleanFolderPath(folderPath + "_" + Guid.NewGuid().ToString().Substring(0, 7));
            try
            {
                string mainRoot = "../../" + fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + fileSetting.FileAppFile + folderPath;

                var bytes = Convert.FromBase64String(fileBase64);
                if (!Directory.Exists(mainRoot))
                {
                    Directory.CreateDirectory(mainRoot);
                }
                FileStream fs = new FileStream(mainRoot + fileName, FileMode.Create, FileAccess.Write);
                if (fs.CanWrite)
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                fs.Flush();
                fs.Close();
                result.Link = fileSetting.FileUploadFile + "/" + fileSetting.FileAppFile + folderPath + fileName;
            }
            catch (Exception ex)
            {
                return result = new MediaUploadResult { IsSuccess = false, ErrorMessageList = new List<string> { ex.Message } };
            }

            return result;
        }

        public MediaUploadResult WriteAboutUsFile(string fileName, string folderPath, string fileBase64)
        {
            MediaUploadResult result = new MediaUploadResult { IsSuccess = true };
            folderPath = CleanFolderPath(folderPath);
            try
            {
                string mainRoot = "../../" + fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + fileSetting.FileAppFile + folderPath;

                var bytes = Convert.FromBase64String(fileBase64);
                if (!Directory.Exists(mainRoot))
                {
                    Directory.CreateDirectory(mainRoot);
                }
                FileStream fs = new FileStream(mainRoot + fileName, FileMode.Create, FileAccess.Write);
                if (fs.CanWrite)
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                fs.Flush();
                fs.Close();
                result.Link = fileSetting.FileUploadFile + "/" + fileSetting.FileAppFile + folderPath + fileName;
            }
            catch (Exception ex)
            {
                return result = new MediaUploadResult { IsSuccess = false };
            }

            return result;
        }

        public string ZipFolder(string folderName)
        {
            string mainRoot = fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + folderName;
            string zipPath = fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + folderName + ".zip";
            ZipFile.CreateFromDirectory(mainRoot, zipPath);

            return "/" + fileSetting.FileUploadFile + "/" + folderName + ".zip";
        }

        #region Util
        private string CleanFolderPath(string folderPath)
        {
            return "/" + folderPath.Replace(@"\", "/").Trim('/') + "/";
        }

        public MediaUploadResult GetFileDirectory(string fileName, string folderPath)
        {
            MediaUploadResult result = new MediaUploadResult { IsSuccess = true };
            folderPath = CleanFolderPath(folderPath + "_" + Guid.NewGuid().ToString().Substring(0, 7));
            try
            {
                string mainRoot = "../../" + fileSetting.FileMainRoot + "/" + fileSetting.FileUploadFile + "/" + fileSetting.FileAppFile + folderPath;

                if (!Directory.Exists(mainRoot))
                {
                    Directory.CreateDirectory(mainRoot);
                }
                result.MainRoot = mainRoot + fileName; ;
                result.Link = fileSetting.FileUploadFile + "/" + fileSetting.FileAppFile + folderPath + fileName;
            }
            catch (Exception ex)
            {
                return result = new MediaUploadResult { IsSuccess = false };
            }

            return result;
        }
        #endregion
    }
}
