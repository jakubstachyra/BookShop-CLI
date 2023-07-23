using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace ConsoleApp.Command
{
    public class ExportCommand : ICommand
    {
        private string filename;
        private string format;

        public ExportCommand(string filename, string format = "XML")
        {
            this.filename = filename;
            this.format = format.ToUpper();
        }
        public ExportCommand()
        {

        }
        public void Execute()
        {
            List<ICommand> commandHistory = CommandFactory.GetCommandHistory();

            switch (format.ToUpper())
            {
                case "XML":
                    ExportToXml(commandHistory);
                    break;

                case "PLAINTEXT":
                    ExportToPlainText(commandHistory);
                    break;

                default:
                    Console.WriteLine("Invalid export format: " + format);
                    break;
            }
        }

        private void ExportToXml(List<ICommand> commandQueue)
        {
            List<object> concreteCommands = commandQueue.Cast<object>().ToList();
            string filenameWithExtension = Path.ChangeExtension(filename, "xml");

            using (var writer = new XmlTextWriter(filenameWithExtension, System.Text.Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;

                var serializer = new XmlSerializer(typeof(List<object>), GetCommandTypes());
                serializer.Serialize(writer, concreteCommands);
            }

            Console.WriteLine("History exported to XML file: " + filenameWithExtension);
        }

        private Type[] GetCommandTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToArray();
        }

        private void ExportToPlainText(List<ICommand> commandQueue)
        {
            string filenameWithExtension = Path.ChangeExtension(filename, "txt");

            using (var writer = new StreamWriter(filenameWithExtension))
            {
                foreach (var command in commandQueue)
                {
                    writer.WriteLine(command.ToString());
                }
            }

            Console.WriteLine("History exported to plaintext file: " + filenameWithExtension);
        }
        public override string  ToString()
        {
            return "Export " + filename + " "  + format;
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