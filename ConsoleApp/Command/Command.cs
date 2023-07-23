using Bajtpik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bajtpik.Data.Interfaces;
using System.Collections;

namespace ConsoleApp.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();

    }

    public class ListCommand : ICommand
    {
        private readonly ICollection<IEntity> collection;
        private string classname;
        public ListCommand(ICollection<IEntity> collection, string classname)
        {
            this.collection = collection;
            this.classname = classname;
        }
        public ListCommand()
        {

        }
        public void Execute()
        {
            Console.WriteLine("Listing objects:");
            foreach (var obj in collection)
            {
                Console.WriteLine(obj.ToString());
            }
            Console.WriteLine();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("list").Append(" ").Append(classname);
            return sb.ToString();
        }
        public void Undo()
        {
            return;
        }
        public void Redo()
        {
            this.Execute();
        }
    }


    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Exiting the application...");
            Environment.Exit(0);
        }
        public override string ToString()
        {
            return "exit";
        }
        public void Undo()
        {
            return;
        }
        public void Redo()
        {
            return;
        }
    }

    public class CommandFactory
    {

        public static List<ICommand> commandHistory = new List<ICommand>();
        public static Stack<ICommand> undoneCommands = new Stack<ICommand>();
        public static Stack<ICommand> redoneCommands = new Stack<ICommand>();
        public static List<ICommand> executedCommands = new List<ICommand>();
        public static List<ICommand> GetCommandHistory()
        {
            return commandHistory;
        }

        public static ICommand CreateCommand(string command, ICollection<IEntity> collection, string[] arguments)
        {
            ICommand createdCommand;

            switch (command.ToLower())
            {
                case "list":
                    createdCommand = new ListCommand(collection, arguments[0]);
                    break;

                case "find":
                    createdCommand = new FindCommand(collection, arguments);
                    break;

                case "exit":
                    createdCommand = new ExitCommand();
                    break;
                case "edit":
                    createdCommand = new EditCommand(collection, arguments);
                    break;
                case "delete":
                    createdCommand = new DeleteCommand(collection, arguments);
                    break;
                case "add":
                    if (arguments != null && arguments.Length > 1)
                    {
                        createdCommand = new AddCommand(collection, arguments[0], arguments[1]);
                    }
                    else
                    {   if(arguments == null)
                        {
                            arguments = new string[2];
                        }
                        createdCommand = new AddCommand(collection, arguments[0]);
                    }
                    break;

                case "history":
                    createdCommand = new HistoryCommand();
                    break;
                case "redo":
                    createdCommand = new RedoCommand(executedCommands.Last());
                    break;
                case "undo":
                    createdCommand = new UndoCommand(executedCommands.Last());
                    break;
                case "export":
                    if (arguments.Length > 0)
                    {
                        string filename = arguments[0];
                        string format = arguments.Length > 1 ? arguments[1] : "XML";
                        return new ExportCommand(filename, format);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters. Usage: queue export {filename} [format]");
                        return null;
                    }
                case "load":
                    if (arguments.Length == 1)
                    {
                        string filename = arguments[0];
                        return new LoadCommand(filename);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters. Usage: queue load {filename}");
                        return null;
                    }
                default:
                    Console.WriteLine("Unknown command: " + command);
                    createdCommand = null;
                    break;

            }

            return createdCommand;
        }
    }

    public class CommandProcessor
    {
        private static  IDictionary<string, ICollection<IEntity>> collections;

        public CommandProcessor()
        {
            collections = new Dictionary<string, ICollection<IEntity>>();
        }

        public void RegisterCollection(string name, ICollection<IEntity> collection)
        {
            collections[name] = collection;
        }

        public static void ProcessCommand(string input, int loaded=0)
        {
            string[] commandParts = input.Split(' ');
            
                string command = commandParts[0];
                if (command == "exit")
                {
                    ExitCommand exitCommand = new ExitCommand();
                        exitCommand.Execute();
                return;
                }
                if (command == "history")
                {
                    HistoryCommand history = new HistoryCommand();
                        history.Execute();
                return;
                }
            if (command == "redo")
            {   if(CommandFactory.undoneCommands.Count == 0)
                {
                    Console.WriteLine("No commands to redo");
                    return;
                }
                RedoCommand redo = new RedoCommand(CommandFactory.undoneCommands.Pop());
                    redo.Execute();
                CommandFactory.commandHistory.Add(redo);
                return;
            }
                if(command == "undo")
            {
                if (CommandFactory.executedCommands.Count == 0)
                {
                    Console.WriteLine("No commands to undo");
                    return;
                }
                ICommand todo = CommandFactory.executedCommands.Last();
                UndoCommand undo = new UndoCommand(todo);
                CommandFactory.undoneCommands.Push(todo);
                        undo.Execute();
                CommandFactory.commandHistory.Add(undo);
                    return;
                }
                
            else if(command == "load")
            {   //Dodaj man dla kazej komendy
                if(commandParts.Length < 1)
                {
                    Console.WriteLine("Invalid command parameters. Usage: queue {load} {filename} ");
                    return;
                }
                string filename = commandParts[1];
                LoadCommand loadCommand = new LoadCommand(filename);
                CommandFactory.commandHistory.Add(loadCommand);
                //CommandFactory.executedCommands.Add(loadCommand);
                loadCommand.Execute();
                
                
                return;
            }
            else if(command == "export")
            {
                if (commandParts.Length < 1)
                {
                    Console.WriteLine("Invalid command parameters. Usage: queue {export} {filename} {format} ");
                    return;
                }
                string filename = commandParts[1];
                string format = commandParts.Length > 1 ? commandParts[2] : "XML";
                ExportCommand exportCommand = new ExportCommand(filename, format);
                if (loaded != 1)
                    exportCommand.Execute();
               
                CommandFactory.commandHistory.Add(exportCommand);
                CommandFactory.executedCommands.Add(exportCommand);
                return;
            }
            else if(command !="load" && command !="export")
            {
                string collectionName = commandParts.Length > 1 ? commandParts[1] : "";

                if (collections.TryGetValue(collectionName, out var collection))
                {
                    string[] arguments = new string[commandParts.Length - 1];
                    Array.Copy(commandParts, 1, arguments, 0, commandParts.Length - 1);
                    
                    ICommand commandObj = CommandFactory.CreateCommand(command, collection, arguments);
                    if (commandObj != null)
                    {
                        CommandFactory.commandHistory.Add(commandObj);
                        if(command != "list")
                        CommandFactory.executedCommands.Add(commandObj);
                        commandObj.Execute();
                    }
                }
                else 
                {
                    Console.WriteLine("Unknown collection: " + collectionName);
                }
            }


        }
        }

    }



