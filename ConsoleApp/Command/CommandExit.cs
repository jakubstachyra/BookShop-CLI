namespace ConsoleApp.Command
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Exiting the application...");
            Environment.Exit(0);
        }
        public string GetDescription()
        {
            return "exit - exits the application.";
        }
        public string Man()
        {
            return GetDescription();
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
}
