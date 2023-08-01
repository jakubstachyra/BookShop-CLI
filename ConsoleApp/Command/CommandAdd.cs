using Bajtpik.Data.Builders;
using Bajtpik.Data.Interfaces;
using System.Collections;

namespace ConsoleApp.Command
{
    public class AddCommand : ICommand
    {
        private readonly ICollection<IEntity> collection;
        private readonly string className;
        private readonly string representation;
        private IEntity addedEntity;
        private bool isExecuted;

        public AddCommand(ICollection<IEntity> collection, string className="<className>", string representation = "base")
        {
            this.collection = collection;
            this.className = className;
            this.representation = representation;
            isExecuted = false;
        }

        public void Execute()
        {
            Console.WriteLine($"Creating a new {className} object ({representation} representation).");

            IBuilder builder;

            switch (className.ToLower())
            {
                case "book":
                    if (representation.ToLower() == "secondary")
                    {
                        builder = new BookListOfTupleBuilder();
                    }
                    else
                    {
                        builder = new BookBuilder();
                    }
                    break;

                case "newspaper":
                    if (representation.ToLower() == "secondary")
                    {
                        builder = new NewspaperListOfTupleBuilder();
                    }
                    else
                    {
                        builder = new NewspaperBuilder();
                    }
                    break;

                case "boardgame":
                    if (representation.ToLower() == "secondary")
                    {
                        builder = new BoardGameListOfTupleBuilder();
                    }
                    else
                    {
                        builder = new BoardGameBuilder();
                    }
                    break;

                case "author":
                    if (representation.ToLower() == "secondary")
                    {
                        builder = new AuthorListOfTupleBuilder();
                    }
                    else
                    {
                        builder = new AuthorBuilder();
                    }
                    break;

                default:
                    Console.WriteLine("Unknown class: " + className);
                    return;
            }

            var fieldNames = builder.GetFieldNames();

            Console.WriteLine("Available fields:");
            foreach (var fieldName in fieldNames)
            {
                Console.WriteLine(fieldName);
            }
            Console.WriteLine("Enter field name and value (<name_of_field>=<value>) line by line:");

            // Prompt the user for field values
            while (true)
            {

                string input = Console.ReadLine().Trim();

                if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                {
                    // Build and add the object to the collection
                    IEntity entity = builder.Build();
                    collection.Add(entity);
                    addedEntity = entity;
                    isExecuted = true;
                    Console.WriteLine($"{className} created.");
                    break;
                }
                else if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Object creation abandoned.");
                    break;
                }

                // Parse the field name and value from the input
                string[] parts = input.Split('=');
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid input format. Expected <name_of_field>=<value>.");
                    continue;
                }

                string fieldName = parts[0].Trim();
                string value = parts[1].Trim();

                // Set the field value using the builder
                bool fieldSet = builder.SetField(fieldName, value);
                if (!fieldSet)
                {
                    Console.WriteLine("Invalid field name: " + fieldName);
                }
            }
        }

        public void Redo()
        {
            if (isExecuted && CommandFactory.undoneCommands.Count > 0)
            {
                collection.Add(addedEntity);
                Console.WriteLine($"{addedEntity} added (Redo).");
            }
            else
            {
                Console.WriteLine("Nothing to redo.");
            }
        }

        public void Undo()
        {
            if (isExecuted && CommandFactory.executedCommands.Count > 0)
            {
                collection.Remove(addedEntity);
                Console.WriteLine($"{addedEntity} removed (Undo).");
            }
            else
            {
                Console.WriteLine("Nothing to undo.");
            }
        }
        public string GetDescription()
        {
            return "\tadd - adds new object to the collection.\n";
        }
        public string Man()
        {
            return "add <className> <representation> - adds new object of <className> with <repesentation> to the collection";
        }
        public override string ToString()
        {
            return "Add " + className + " " + representation;
        }
    }
}
