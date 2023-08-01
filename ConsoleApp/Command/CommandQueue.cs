namespace ConsoleApp.Command
{
    public class CommandQueue
    {
        private readonly List<ICommand> commands;

        public CommandQueue()
        {
            commands = new List<ICommand>();
        }

        public void EnqueueCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void ExecuteCommands()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        public void Clear()
        {
            commands.Clear();
        }

        public List<ICommand> GetCommands()
        {
            return commands;
        }
    }
    public class HistoryCommand : ICommand
    {
        public void Execute()
        {
            List<ICommand> commandHistory = CommandFactory.GetCommandHistory();

            Console.WriteLine("History:");

            foreach (var command in commandHistory)
            {
                Console.WriteLine(command.ToString());
            }
            Console.WriteLine();
        }
        public string GetDescription()
        {
            return "history - shows the history of commands.";
        }
        public string Man()
        {
            return GetDescription();
        }
        public override string ToString()
        {
            return "history";

        }
        public void Redo()
        {
            Execute();
        }
        public void Undo()
        {
            return;
        }
    }

}
