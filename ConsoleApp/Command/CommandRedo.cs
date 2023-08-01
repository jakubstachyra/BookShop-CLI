namespace ConsoleApp.Command
{

    public class RedoCommand : ICommand
    {
        private readonly ICommand commandManager;

        public RedoCommand(ICommand commandManager)
        {
            this.commandManager = commandManager;
        }

        public void Execute()
        {
            commandManager.Redo();
            CommandFactory.undoneCommands.Pop();
        }
        public string GetDescription()
        {
            return "redo - redo the last undone command";
        }
        public string Man()
        {
            return GetDescription();
        }
        public override string ToString()
        {
            return "redo";

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

    public class UndoCommand : ICommand
    {
        private readonly ICommand commandManager;

        public UndoCommand(ICommand commandManager)
        {
            this.commandManager = commandManager;
        }

        public void Execute()
        {
            CommandFactory.undoneCommands.Push(commandManager);
            commandManager.Undo();
            CommandFactory.executedCommands.Remove(commandManager);
        }
        public string GetDescription()
        {
            return "undo - undoes the last command.";
        }
        public string Man()
        {
            return GetDescription();
        }
        public override string ToString()
        {
            return "undo";
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
}

