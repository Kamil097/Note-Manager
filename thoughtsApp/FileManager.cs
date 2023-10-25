using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class FileManager
	{
		public static void CreateNote(string description)
		{
			string fileName = "note_" + DateTime.Now.ToString().Replace(':', '.') + ".txt";
			File.AppendAllText(FileConfig.FullPath+ $"/{fileName}", description);
		}
		public static void Initialize()
		{
			if (!Directory.Exists(FileConfig.FullPath))
			{
				Directory.CreateDirectory(FileConfig.FullPath);
			}
		}
		public static string[] getFiles(string dirPath) 
		{
			var files = Directory.GetFiles(dirPath);
			return files;
		}
		public static void UploadFiles() 
		{
			string folderId = "12Cy_QIkTUjqD-Gqg7jHCa2RERpMGj3m0";
			string credentialsPath = "credentials.json";
			UploadFilesToGoogleDrive(credentialsPath, folderId);
        }

        static async Task UploadFilesToGoogleDrive(string credentialsPath, string folderId) // Add "async" here
        {
            GoogleCredential credential = GoogleCredential.FromFile(credentialsPath).CreateScoped(DriveService.ScopeConstants.Drive);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            string[] files = getFiles(FileConfig.FullPath);

            foreach (var file in files)
            {
                Console.WriteLine(file);
                var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(file),
                    Parents = new List<string> { folderId }
                };

                await using (var stream = new FileStream(file, FileMode.Open,FileAccess.Read))
                {
                    var request = service.Files.Create(fileMetaData, stream, "test/plain");
                    request.Fields = "*";
                    var results = await request.UploadAsync(CancellationToken.None);

                    if (results.Status == UploadStatus.Failed)
                    {
                        Console.WriteLine($"Error uploading file: {results.Exception.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"Git: {request.ResponseBody.Id}");
                    }
                }     
            }
        }
    }
}
