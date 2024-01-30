using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace thoughtsApp.Tui
{
    public class MenuLogic
    {
        public async void RunMainMenu()
        {
            var menu = MenuTemplates.MainMenu;
            int option = menu.Run() + 1;
            Clear();
            switch (option)
            {
                case 1:
                    WriteLine("Write your thought");
                    await FileManager.SendNewNote(ReadLine(), "note");
                    await Task.Delay(1000);
                    break;
                case 2:

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
            RunMainMenu();
        }
        public async void FolderChoiceMenu()
        {
            var menu = MenuTemplates.UploadThoughtMenu;
            int option = menu.Run() + 1;
            Clear();
            switch (option)
            {
                case 1:
                    //
                    return;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
            FolderChoiceMenus();
        }
    }
}
