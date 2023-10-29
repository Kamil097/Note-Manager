using thoughtsApp;

FileManager.Initialize();

while (true)
{
    int option = MainView.MainWindowOptionListLoop();

	switch (option) 
    {
        case 1: 
            MainView.ThoughtLoop();
            break;
        case 2:
            MainView.DriveExplorer(FileConfig.folderId);
            break;
        case 3:
            MainView.RandomFileViewer(FileConfig.folderId);
            break;
    }
}
