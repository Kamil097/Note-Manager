using thoughtsApp;
using thoughtsApp.Tui;
using static thoughtsApp.Tui.MenuLogic;
do
{
    Visuals.WaitingAnimation("Downloading folder info");
}
while(!FolderChoiceMenu().IsCompleted);
    
MenuTemplateLogic menuLogic = new MenuTemplateLogic();
menuLogic.RunMainMenu();