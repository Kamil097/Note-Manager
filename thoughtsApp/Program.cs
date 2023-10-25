using thoughtsApp;

string description = "";
FileManager.Initialize();
while (true)
{
    Console.WriteLine("Share your thought!");
    description = Console.ReadLine();
    FileManager.CreateNote(description);
    Console.Clear();
}