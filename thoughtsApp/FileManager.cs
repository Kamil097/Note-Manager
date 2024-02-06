using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Newtonsoft.Json.Linq;
using System.Text;

namespace thoughtsApp
{
    public static class FileManager
    {
        public static event Action DownloadStarted;
        public static event Action DownloadCompleted;
        public static event Action FoldersDownloadStarted;
        public static event Action FoldersDownloadCompleted;

        public static string[] getFiles(string dirPath)
        {
            var files = Directory.GetFiles(dirPath);
            return files;
        }
        public static DriveService googleService()
        {
            GoogleCredential credential = GoogleCredential.FromFile(FileConfig.credentialsPath).CreateScoped(DriveService.ScopeConstants.Drive);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }
        public static string GetDateTimeName(string prefix)
        {
            return $"{prefix}_" + DateTime.Now.ToString().Replace(':', '.') + ".txt";
        }
        public static void CreateNote(string description, string path)
        {

            File.AppendAllText(path + $"/{GetDateTimeName("note")}", description);
        }
        //public static async void UploadNotesToGoogleDrive(string localDirPath, string folderId)
        //{

        //    var service = googleService();
        //    string[] files = getFiles(localDirPath);

        //    foreach (var file in files)
        //    {
        //        Console.WriteLine(file);
        //        var fileMetaData = new Google.Apis.Drive.v3.Data.File()
        //        {
        //            Name = Path.GetFileName(file),
        //            Parents = new List<string> { folderId }
        //        };

        //        using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
        //        {
        //            var request = service.Files.Create(fileMetaData, stream, "test/plain");
        //            request.Fields = "*";
        //            var results = await request.UploadAsync(CancellationToken.None);

        //            if (results.Status == UploadStatus.Failed)
        //            {
        //                Console.WriteLine($"Error uploading file: {results.Exception.Message}");
        //            }
        //            else
        //            {
        //                Console.WriteLine($"File with id: {request.ResponseBody.Id} has been uploaded.");
        //            }
        //        }
        //    }
        //}
        public static async Task UpdateNoteToGoogleDrive(string fileId, string text)
        {
            string encryptedText = Encryptor.Encrypt(text, FileConfig.encryptionKey);
            var service = googleService();
            var fileMetaData = new Google.Apis.Drive.v3.Data.File();
            var stream = new MemoryStream();
            byte[] textBytes = Encoding.UTF8.GetBytes(encryptedText);
            stream.Write(textBytes, 0, textBytes.Length);
            var request = service.Files.Update(fileMetaData, fileId, stream, "text/plain");
            var result = await request.UploadAsync(CancellationToken.None);

            if (result.Status == UploadStatus.Failed)
            {
                Console.WriteLine($"Error uploading file: {result.Exception.Message}");
            }
            else
            {
                Console.WriteLine($"File with id: {request.ResponseBody.Id} has been uploaded.");
            }
            Thread.Sleep(1000);
            await Task.CompletedTask;
        }
        public static async Task DeleteNoteFromGoogleDrive(string fileId)
        {
            var service = googleService();
            var request = service.Files.Delete(fileId);
            var result = await request.ExecuteAsync(CancellationToken.None);
            await Task.CompletedTask;
        }
        public static async Task SendNewNote(string description, string name)
        {
            string encryptedMessage = Encryptor.Encrypt(description, FileConfig.encryptionKey);
            name = GetDateTimeName(name);
            var service = googleService();
            var fileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Parents = new List<string> { FileConfig.folderId }
            };
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(encryptedMessage);
                stream.Write(textBytes, 0, textBytes.Length);

                var request = service.Files.Create(fileMetaData, stream, "text/plain");
                request.Fields = "*";
                var result = await request.UploadAsync(CancellationToken.None);

                if (result.Status == UploadStatus.Failed)
                {
                    Console.WriteLine($"Error uploading file: {result.Exception.Message}");
                }
                else
                {
                    Console.WriteLine($"File with id: {request.ResponseBody.Id} has been uploaded.");
                }
            }
            await Task.CompletedTask; ///idk about that bruh
        }
        public static async Task<List<(string name, string id)>> GetNotesInfoFromDrive()
        {
            List<(string name, string id)> pliki = new List<(string name, string id)>();

            var service = googleService();
            var request = service.Files.List();
            request.Q = $"'{FileConfig.folderId}' in parents";
            var files = await request.ExecuteAsync();
            foreach (var file in files.Files)
            {
                pliki.Add((file.Name, file.Id));
            }

            return pliki;
        }
        public static async Task<string> GetFileText(string fileId)
        {
            var service = googleService();
            var requestFile = service.Files.Get(fileId);
            var stream = new MemoryStream();
            await requestFile.DownloadAsync(stream);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            var tekst = reader.ReadToEnd();
            string encryptedText = Encryptor.Decrypt(tekst, FileConfig.encryptionKey);
            return encryptedText;
        }
        //public static async Task DownloadAllNotesJson(string folderId, string combinedNotes)
        //{
        //    if (!File.Exists(combinedNotes))
        //    {
        //        File.Create(combinedNotes);
        //    }
        //    JObject data = new JObject();//GetJsonObject(combinedNotes);//GetJsonObject(combinedNotes);
        //    data["data"] = new JArray();
        //    JArray array = (JArray)data["data"];

        //    DownloadStarted?.Invoke();
        //    var service = googleService();
        //    var request = service.Files.List();
        //    request.Q = $"'{folderId}' in parents";
        //    var files = request.Execute();

        //    foreach (var file in files.Files)
        //    {
        //        var requestFile = service.Files.Get(file.Id);
        //        using (var stream = new MemoryStream())
        //        {
        //            requestFile.Download(stream);
        //            stream.Position = 0;
        //            using (StreamReader reader = new StreamReader(stream))
        //            {
        //                string fileText = reader.ReadToEnd();
        //                string encryptedText = Encryptor.Decrypt(fileText, FileConfig.encryptionKey);
        //                JObject newNote = createNoteObj(file.Name, encryptedText);
        //                array.Add(newNote);
        //            }
        //        }
        //    }
        //    File.WriteAllText(combinedNotes, data.ToString());
        //    DownloadCompleted?.Invoke();
        //}
        public static JObject createNoteObj(string name, string text)
        {
            JObject newNote = new JObject
            {
                { "name", name },
                { "text", text }
            };
            return newNote;
        }
        //public static JObject GetJsonObject(string jsonResourceName)
        //{

        //    using (StreamReader reader = new StreamReader(jsonResourceName))
        //    {

        //        string content = reader.ReadToEnd();
        //        JObject jobject = JObject.Parse(content);
        //        return jobject;
        //    }
        //}
        public static async Task<List<(string name, string Id)>> getCurrentFolders()
        {
            string folderId = "1y00Xd9fVkm7GeDF5gYspTKRP1nER9ldG";
           
                List<(string name, string id)> pliki = new List<(string name, string id)>();

            var service = googleService();
            var request = service.Files.List();
            request.Q = $"'{folderId}' in parents";
            FoldersDownloadStarted?.Invoke();
            var result = await request.ExecuteAsync();//.Files.ToList().Where(x => x.MimeType == "application/vnd.google-apps.folder").ToList();
            var folders =  result.Files.ToList().Where(x => x.MimeType == "application/vnd.google-apps.folder").ToList();
            foreach (var file in folders)
            {
                pliki.Add((file.Name, file.Id));
            }
            FoldersDownloadCompleted?.Invoke();
            await Task.Delay(3000);
            return pliki;

        }
        public static async Task CreateNewFolder(string folderName)
        {
            var service = googleService();
            var folderMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                Parents = new List<string> { FileConfig.mainFolder },
                MimeType = "application/vnd.google-apps.folder",
            };

            var request = service.Files.Create(folderMetadata);
            request.Fields = "id";
            var folder = request.Execute();

            Console.WriteLine($"Utworzono folder o Id: {folder.Id}");
            await Task.CompletedTask;
        }
    }
}
