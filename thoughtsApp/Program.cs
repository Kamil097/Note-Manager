using thoughtsApp;

string description = "";
FileManager.Initialize();
FileManager.UploadFiles();
while (true)
{
    Console.WriteLine("Share your thought!");
    description = Console.ReadLine();
    FileManager.CreateNote(description);
    Console.Clear();
}