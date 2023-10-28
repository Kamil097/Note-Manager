using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace thoughtsApp
{
	public static class FileManager
	{
		
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
		public static DriveService googleService(string credentialsPath) 
		{
			GoogleCredential credential = GoogleCredential.FromFile(credentialsPath).CreateScoped(DriveService.ScopeConstants.Drive);

			var service = new DriveService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential
			});
			return service;
		}
		public static string GetDateTimeName() 
		{
			return "note_" + DateTime.Now.ToString().Replace(':', '.') + ".txt";
		}
		public static void CreateNote(string description, string path)
		{
			
			File.AppendAllText(path + $"/{GetDateTimeName()}", description);
		}
		public static async void UploadFilesToGoogleDrive(string credentialsPath, string localDirPath, string folderId) 
		{
			
			var service = googleService(credentialsPath);	
			string[] files = getFiles(localDirPath);

			foreach (var file in files)
			{
				Console.WriteLine(file);
				var fileMetaData = new Google.Apis.Drive.v3.Data.File()
				{
					Name = Path.GetFileName(file),
					Parents = new List<string> { folderId }
				};

				using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
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
						Console.WriteLine($"File with id: {request.ResponseBody.Id} has been uploaded.");
					}
				}
			}
		}
		public static async void SendNewNote(string description, string credentialsPath, string folderId) 
		{
			string name = GetDateTimeName();
			var service = googleService(credentialsPath);
			var fileMetaData = new Google.Apis.Drive.v3.Data.File()
			{
				Name = name,
				Parents = new List<string> { folderId }
			};
		    using (MemoryStream stream = new MemoryStream())
			{
				byte[] textBytes = Encoding.UTF8.GetBytes(description);
				stream.Write(textBytes, 0, textBytes.Length);

				var request = service.Files.Create(fileMetaData,stream,"text/plain");
				request.Fields= "*";
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
		}

	}
}
