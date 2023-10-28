using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections;
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
		public static DriveService googleService() 
		{
			GoogleCredential credential = GoogleCredential.FromFile(FileConfig.credentialsPath).CreateScoped(DriveService.ScopeConstants.Drive);

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
		public static async void UploadNotesToGoogleDrive( string localDirPath, string folderId) 
		{
			
			var service = googleService();	
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
		public static async void SendNewNote(string description, string folderId) 
		{
			string name = GetDateTimeName();
			var service = googleService();
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
		public static List<(string name, string id)> GetNotesInfoFromDrive(string folderId) 
		{
			List<(string name, string id)> pliki = new List<(string name, string id)>();

			var service = googleService();
			var request = service.Files.List();
			request.Q = $"'{folderId}' in parents";
			var files = request.Execute();
			foreach (var file in files.Files)
			{
				var fileId = file.Id;
				pliki.Add((file.Name,file.Id));
			}

			return pliki;
		}
		public static string GetFileText(string fileId)
		{
			var service = googleService();
			var requestFile = service.Files.Get(fileId);
			var stream = new MemoryStream();
			requestFile.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
			{
				switch (progress.Status)
				{
					case Google.Apis.Download.DownloadStatus.Downloading:
						Console.WriteLine(progress.BytesDownloaded);
						break;

					case Google.Apis.Download.DownloadStatus.Completed:
						Console.WriteLine("Download complete.");
						break;

					case Google.Apis.Download.DownloadStatus.Failed:
						Console.WriteLine("Download failed.");
						break;
				}
			};
			requestFile.Download(stream);
			stream.Position = 0;
			StreamReader reader = new StreamReader(stream);
			var tekst = reader.ReadToEnd();
			return tekst;
		}

	}
}