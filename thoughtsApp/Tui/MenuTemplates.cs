using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp.Tui
{
    public static class MenuTemplates
    {
        public static Menu MainMenu = new Menu(
            @"
  /\/\   __ _(_)_ __    _ __ ___   ___ _ __  _   _ 
 /    \ / _` | | '_ \  | '_ ` _ \ / _ | '_ \| | | |
/ /\/\ | (_| | | | | | | | | | | |  __| | | | |_| |
\/    \/\__,_|_|_| |_| |_| |_| |_|\___|_| |_|\__,_|              
",
            MenuLogic.emptyAction, new (string functionArgument, string option)[]  {
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
        public static Menu InitialActionMenu = new Menu(@"
  _______ _                       _     _            
 |__   __| |                     | |   | |           
    | |  | |__   ___  _   _  __ _| |__ | |_ ___ _ __ 
    | |  | '_ \ / _ \| | | |/ _` | '_ \| __/ _ \ '__|
    | |  | | | | (_) | |_| | (_| | | | | ||  __/ |   
    |_|  |_| |_|\___/ \__,_|\__, |_| |_|\__\___|_|   
                             __/ |                   
                            |___/     
", MenuLogic.emptyAction, new (string functionArgument, string option)[]  {
            ("","1. Manage notes"),
            ("","2. Create folder"),
            ("","3. Delete folder")
        });
    }
} 
