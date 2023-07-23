using Bajtpik.Data.Interfaces;
using Bajtpik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Command
{
    public class EditCommand : ICommand
    {
        private readonly ICollection<IEntity> collection;
        private readonly string className;
        private readonly List<string> requirements;
        private Dictionary<string, string> fieldValues;
        private IEntity editedObject;
        private Dictionary<string, string> originalFieldValues;

        public EditCommand(ICollection<IEntity> collection, string[] arguments)
        {
            this.collection = collection;

            if (arguments.Length < 1)
            {
                Console.WriteLine("Invalid command format. Usage: edit <name_of_the_class> [<requirement> ...] [<field_name>=<new_value> ...]");
                return;
            }

            className = arguments[0];
            requirements = arguments.Skip(1).ToList();
            fieldValues = new Dictionary<string, string>();
        }

        public void Execute()
        {
            FindCommand findCommand = new FindCommand(collection, new[] { className }.Concat(requirements).ToArray());
            IEnumerable<IEntity> foundObjects = findCommand.ExecuteAndReturn();

            if (foundObjects.Count() != 1)
            {
                Console.WriteLine("Unable to uniquely identify the object to edit.");
                return;
            }

            IEntity objectToEdit = foundObjects.First();
            editedObject = objectToEdit;
            originalFieldValues = SaveOriginalFieldValues(objectToEdit);

            Console.WriteLine($"Editing the following object: {objectToEdit.ToString()}");
            Console.WriteLine("Please provide the field name and new value for each field you want to edit (e.g., fieldName=newValue).\nEnter DONE when finished or EXIT to abandon");

            string input;

            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input != "DONE" && input != "EXIT")
                {
                    if (input.Contains("="))
                    {
                        string[] parts = input.Split('=');
                        if (parts.Length == 2)
                        {
                            string fieldName = parts[0].Trim();
                            string newValue = parts[1].Trim();
                            fieldValues[fieldName] = newValue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid field assignment: " + input);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please provide field name and new value in the format fieldName=newValue.");
                    }
                }
            } while (input != "DONE" && input != "EXIT");

            if (input == "DONE")
            {
                EditObjectFields(objectToEdit, fieldValues);
                Console.WriteLine("Object edited successfully.");
            }
            else if (input == "EXIT")
            {
                Console.WriteLine("Edition canceled.");
            }
        }

        public void Undo()
        {
            if (editedObject != null && originalFieldValues != null)
            {
                RestoreOriginalFieldValues(editedObject, originalFieldValues);
                Console.WriteLine($"Undo: {editedObject.ToString()} restored to its original values.");
            }
        }

        public void Redo()
        {
            if (editedObject != null && fieldValues != null)
            {
                EditObjectFields(editedObject, fieldValues);
                Console.WriteLine($"Redo: {editedObject.ToString()} updated with new values.");
            }
        }

        private Dictionary<string, string> SaveOriginalFieldValues(IEntity obj)
        {
            Dictionary<string, string> originalValues = new Dictionary<string, string>();

            foreach (var fieldValue in fieldValues)
            {
                string fieldName = fieldValue.Key;
                var (propertyValue, _) = obj.GetProperty(fieldName);

                if (propertyValue != null)
                {
                    originalValues[fieldName] = propertyValue.ToString();
                }
            }

            return originalValues;
        }

        private void RestoreOriginalFieldValues(IEntity obj, Dictionary<string, string> originalValues)
        {
            foreach (var originalValue in originalValues)
            {
                string fieldName = originalValue.Key;
                string originalValueStr = originalValue.Value;

                obj.SetProperty(fieldName, originalValueStr);
            }
        }

        private void EditObjectFields(IEntity obj, Dictionary<string, string> fieldValues)
        {
            foreach (var fieldValue in fieldValues)
            {
                string fieldName = fieldValue.Key;
                string newValue = fieldValue.Value;

                var (propertyValue, propertyType) = obj.GetProperty(fieldName);

                if (propertyValue != null)
                {
                    switch (propertyType)
                    {
                        case "string":
                            if (propertyValue is string)
                            {
                                obj.SetProperty(fieldName, newValue);
                            }
                            break;

                        case "int":
                            if (propertyValue is int)
                            {
                                if (int.TryParse(newValue, out int parsedValue))
                                {
                                    obj.SetProperty(fieldName, parsedValue);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid field value: " + newValue);
                                }
                            }
                            break;

                        default:
                            Console.WriteLine("Field type not supported: " + propertyType);
                            break;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Edit ").Append(className).Append(" ");

            foreach (string req in requirements)
            {
                sb.Append(req).Append(" ");
            }

            foreach (var fieldValue in fieldValues)
            {
                sb.Append(fieldValue.Key).Append("=").Append(fieldValue.Value).Append(" ");
            }

            return sb.ToString();
        }
    }


}
