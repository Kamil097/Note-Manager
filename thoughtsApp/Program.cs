using thoughtsApp;

FileManager.Initialize();
//FileManager.CreateNewFolder();
while (true)
{
    var choice = MainView.FolderListLoop();

    if (choice.name!=null)
    {
        MainView.MainWindowOptionListLoop(choice.id,choice.name);
    }
}