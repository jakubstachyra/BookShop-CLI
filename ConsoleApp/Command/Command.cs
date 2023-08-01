using Bajtpik.Data.Interfaces;

namespace ConsoleApp.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
        string GetDescription();
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

        public static ICommand? CreateCommand(string command, ICollection<IEntity> collection, string[] arguments)
        {
            ICommand? createdCommand;

            switch (command.ToLower())
            {
                case "list":
                    createdCommand = new ListCommand(collection, arguments[0]);
                    break;

                case "find":
                    createdCommand = new FindCommand(collection, arguments);
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
                    {
                        if (arguments == null)
                        {
                            arguments = new string[2];
                        }
                        createdCommand = new AddCommand(collection, arguments[0]);
                    }
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
        public static ICommand? CreateCommand(string command)
        {
            ICommand? createdCommand = null;

            switch (command.ToLower())
            {
                case "exit":
                    createdCommand = new ExitCommand();
                    break;
                case "history":
                    createdCommand = new HistoryCommand();
                    break;
                case "redo":
                    if (undoneCommands.Count == 0)
                    {
                        Console.WriteLine("No commands to redo");
                        break;
                    }
                    createdCommand = new RedoCommand(undoneCommands.Pop());
                    break;
                case "undo":
                    if (executedCommands.Count == 0)
                    {
                        Console.WriteLine("No commands to undo");
                        break;
                    }
                    ICommand todo = executedCommands.Last();
                    UndoCommand undo = new UndoCommand(todo);
                    undoneCommands.Push(todo);
               
                    createdCommand = new UndoCommand(executedCommands.Last());
                    break;
                case "help":
                    createdCommand = new HelpCommand();
                    break;
                case "clear":
                    createdCommand = new CommandClear();
                    break;
                default:
                    Console.WriteLine("Unknown command: " + command);
                    break;

            }

            return createdCommand;
        }
    }

    public class CommandProcessor
    {
        private readonly IDictionary<string, ICommand> commandHandlers;
        public static IDictionary<string, string> commandDescriptions;
        
        private static IDictionary<string, ICollection<IEntity>> collections;

        public CommandProcessor()
        {
            collections = new Dictionary<string, ICollection<IEntity>>();
            commandDescriptions = new Dictionary<string, string>
        {
            { "list", "List all entities in the collection." },
            { "find", "Find entities based on specified criteria." },
            { "edit", "Edit an entity." },
            { "delete", "Delete an entity." },
            { "add", "Add a new entity to the collection." },
            { "history", "Display the history of executed commands." },
            { "undo", "Undo the last executed command." },
            { "redo", "Redo the last undone command." },
            { "export", "Export the collection to a file. Usage: export <filename> [format]" },
            { "load", "Load a collection from a file. Usage: load <filename>" },
            { "help", "Display available commands and their descriptions." },
            { "man", "Show the manual for a specific command. Usage: man <command>" },
            {"clear","Clears screen." }
                
        };
      
        }

        public void RegisterCollection(string name, ICollection<IEntity> collection)
        {
            collections[name] = collection;
        }

        public static void ProcessCommand(string input, int loaded = 0)
        {
            string[] commandParts = input.Split(' ');

            string command = commandParts[0];
          

            if (command == "load")
            {   //Dodaj man dla kazej komendy
                if (commandParts.Length < 1)
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
            else if (command == "export")
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
            else if (command != "load" && command != "export")
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
                        if (command != "list")
                            CommandFactory.executedCommands.Add(commandObj);
                        commandObj.Execute();
                    }
                }
                else if(commandParts.Length == 1)
                {

                    ICommand commandObj = CommandFactory.CreateCommand(command);
                    if (commandObj != null)
                    {
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



