using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp.Tui
{
    public static class MenuTemplates
    {
        public static Menu MainMenu = new Menu("Thoughts app.",MenuLogic.emptyAction, new (string functionArgument, string option)[]  {
            ("","1. Upload your thought."),
            ("","2. Upload text from file."),
            ("","3. View thoughts explorer."),
            ("","4. View random thought."),
            ("","5. Find given expression."),
            ("","Go back.")
        }
        );
        public static Menu InsertFromFileMenu = new Menu("Uplaod your thought!", MenuLogic.emptyAction,new (string functionArgument, string option)[]  {
            ("","1. Insert path."),
            ("","2. Choose .txt file."),
        });
        public static Menu InitialActionMenu = new Menu("Choose what you wanna do:", MenuLogic.emptyAction, new (string functionArgument, string option)[]  {
            ("","1. Manage notes"),
            ("","2. Create folder"),
            ("","3. Delete folder")
        });
    }
} 
