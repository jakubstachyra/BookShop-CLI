using System.Text;

namespace ConsoleApp.Command
{
    public class LoadCommand : ICommand
    {
        private readonly string filename;

        public LoadCommand(string filename)
        {
            this.filename = filename;
        }

        public void Execute()
        {
            string[] loadedCommands = LoadCommandsFromFile(filename);
            //CommandFactory.commandHistory.Clear();

            foreach (var line in loadedCommands)
            {
                CommandProcessor.ProcessCommand(line, 1);
            }
            Console.WriteLine($"Commands loaded from '{filename}' and added to the history.");
        }
        public string GetDescription()
        {
            return "load - loads commands history from a file.";
        }
        public string Man()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("load - loads commands history from a file.");
            sb.AppendLine("Usage: load <filename>");
            return sb.ToString();
        }
        public override string ToString()
        {
            return $"Load {filename}";
        }
        public string[] LoadCommandsFromFile(string filename)
        {
            string[] lines = { " ", "" };
            try
            {
                lines = File.ReadAllLines(filename);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File '{filename}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading commands from '{filename}': {ex.Message}");
            }

            return lines;
        }
        public string[] LoadCommandsFromFileXML(string filename)
        {
            return null;
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
