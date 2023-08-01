using Bajtpik.Data.Interfaces;
using System.Text;

namespace ConsoleApp.Command
{
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
        public string GetDescription()
        {
            return "list - lists all objects in the database";
        }
        public string Man()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("list - lists all objects of given kind in the database");
            sb.AppendLine("Usage: list <name_of_the_class>");
            return sb.ToString();
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
            Execute();
        }
    }
}
