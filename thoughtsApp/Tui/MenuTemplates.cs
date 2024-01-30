using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp.Tui
{
    public static class MenuTemplates
    {
        public static Menu MainMenu = new Menu("Thoughts app.", new (string option, object property)[] {
            ("1.","Upload your thought."),
            ("2.","Upload text from file."),
            ("3.","View thoughts explorer."),
            ("4.","View random thought."),
            ("5.","Perform operation on notes.")
        });
        public static Menu UploadThoughtMenu = new Menu("Uplaod your thought!", new (string option, object property)[] {
            ("1.","Back to menu."),
            ("2.","Break."),
        });
    }
}
