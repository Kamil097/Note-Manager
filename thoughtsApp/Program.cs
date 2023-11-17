using thoughtsApp;

FileManager.Initialize();
//FileManager.CreateNewFolder();
while (true)
{
    var choice = MainView.FolderListLoop();

    if (choice.option > 0)
    {
        string folderId = FileConfig.jsonDict[FileConfig.jsonName[choice.option - 1]];
        MainView.MainWindowOptionListLoop(folderId);
    }
}