using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Command
{
    internal class CommandClear: ICommand
    {
        public void Execute()
        {
            Console.Clear();
            Console.Write("Bajtpik, buy a book! - for help type:");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" help\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public string GetDescription()
        {
            return "clear - clears the console";
        }
        public string Man()
        {
            return GetDescription();
        }
        public void Redo()
        {
            return;
        }
        public void Undo()
        {
            return;
        }

    }
}
