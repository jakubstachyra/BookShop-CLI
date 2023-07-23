namespace ConsoleApp.Command
{
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
}
