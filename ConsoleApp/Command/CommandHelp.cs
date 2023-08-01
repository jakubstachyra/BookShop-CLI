using Bajtpik.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Command
{
    internal class HelpCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Available commands:");
            int index = 1;
            foreach (var commandDescription in CommandProcessor.commandDescriptions)
            {
                Console.Write($"\t{index}. ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{commandDescription.Key} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" - {commandDescription.Value}\n");
                index++;
            }
        }
        public string GetDescription()
        {
            return "help - lists all avaible commands with their description";
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
