using Bajtpik.Data.Interfaces;
using System.Text;

namespace ConsoleApp.Command
{
    public class DeleteCommand : ICommand
    {
        private readonly ICollection<IEntity> collection;
        private readonly string className;
        private readonly List<string> requirements;
        private IEntity deletedObject;

        public DeleteCommand(ICollection<IEntity> collection, string[] arguments)
        {
            this.collection = collection;

            if (arguments.Length < 1)
            {
                Console.WriteLine("Invalid command format. Usage: delete <name_of_the_class> [<requirement> ...]");
                return;
            }

            className = arguments[0];
            requirements = arguments.Skip(1).ToList();
        }

        public void Execute()
        {
            FindCommand findCommand = new FindCommand(collection, new[] { className }.Concat(requirements).ToArray());
            IEnumerable<IEntity> foundObjects = findCommand.ExecuteAndReturn();

            if (foundObjects == null || foundObjects.Count() != 1)
            {
                Console.WriteLine("Unable to uniquely identify the object to delete.");
                return;
            }

            IEntity objectToDelete = foundObjects.First();
            Console.WriteLine($"Deleting the following object: {objectToDelete.ToString()}");
            collection.Remove(objectToDelete);
            deletedObject = objectToDelete;
            Console.WriteLine("Object deleted successfully.");
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            collection.Add(deletedObject);
            Console.WriteLine($"Undo: {deletedObject.ToString()} restored.");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Delete ").Append(className).Append(" ");

            foreach (string req in requirements)
            {
                sb.Append(req).Append(" ");
            }

            return sb.ToString();
        }
    }
}
