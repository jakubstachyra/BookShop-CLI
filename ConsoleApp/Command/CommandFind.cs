using Bajtpik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bajtpik.Data.Interfaces;

namespace ConsoleApp.Command
{
    public class FindCommand : ICommand
    {
        private readonly ICollection<IEntity> collection;
        private readonly string className;
        private readonly List<string> requirements;

        public FindCommand(ICollection<IEntity> collection, string[] arguments)
        {
            this.collection = collection;

            if (arguments.Length < 1)
            {
                Console.WriteLine("Invalid command format. Usage: find <name_of_the_class> [<requirement> ...]");
                return;
            }

            className = arguments[0];
            requirements = arguments.Skip(1).ToList();
        }

        public void Execute()
        {
            Type classType = GetClassType(className);
            if (classType == null)
            {
                Console.WriteLine("Unknown class: " + className);
                return;
            }

            IEnumerable<IEntity> filteredObjects = collection.Where(obj => obj.GetType() == classType);
            filteredObjects = FilterObjects(filteredObjects);

            foreach (IEntity entity in filteredObjects)
            {
                Console.WriteLine(entity.ToString());
            }
        }
        public List<IEntity> ExecuteAndReturn()
        {
            Type classType = GetClassType(className);
            if (classType == null)
            {
                Console.WriteLine("Unknown class: " + className);
                return null;
            }

            IEnumerable<IEntity> filteredObjects = collection.Where(obj => obj.GetType() == classType);
            filteredObjects = FilterObjects(filteredObjects);

            return filteredObjects.ToList();
        }

        private Type GetClassType(string className)
        {
            switch (className.ToLower())
            {
                case "book":
                    return typeof(Book);

                case "newspaper":
                    return typeof(Newspaper);

                case "boardgame":
                    return typeof(BoardGame);

                case "author":
                    return typeof(Author);

                default:
                    return null;
            }
        }

        private IEnumerable<IEntity> FilterObjects(IEnumerable<IEntity> objects)
        {
            List<IEntity> filteredObjects = new List<IEntity>();

            foreach (var obj in objects)
            {
                bool meetsRequirements = true;

                foreach (var requirement in requirements)
                {
                    char[] separators = { '=', '<', '>'};
                    string[] requirementParts = requirement.Split(separators);

                    int separatorIndex = requirement.IndexOfAny(separators);
                    string fieldName="", operatorSymbol="", fieldValue="";
                    if (separatorIndex >= 0)
                    {
                        fieldName = requirement.Substring(0, separatorIndex).Trim();
                        operatorSymbol = requirement.Substring(separatorIndex, 1).Trim();
                        fieldValue = requirement.Substring(separatorIndex + 1).Trim();

                    }
                    if (operatorSymbol != "<" && operatorSymbol != "=" && operatorSymbol != ">")
                    {
                        Console.WriteLine("Invalid operator: " + operatorSymbol);
                        return null;
                    }
                    if (!CompareField(obj, fieldName, operatorSymbol, fieldValue))
                    {
                        meetsRequirements = false;
                        break;
                    }
                }

                if (meetsRequirements)
                {
                    filteredObjects.Add(obj);
                }
            }

            return filteredObjects;
        }


        private bool CompareField(IEntity obj, string fieldName, string operatorSymbol, string fieldValue)
        {
            var (propertyValue, propertyType) = obj.GetProperty(fieldName);

            if (propertyValue != null)
            {
                switch (propertyType)
                {
                    case "string":
                        if (propertyValue is string stringValue)
                        {
                            switch (operatorSymbol)
                            {
                                case "=":
                                    return string.Equals(stringValue, fieldValue, StringComparison.OrdinalIgnoreCase);
                                default:
                                    Console.WriteLine("Unknown operator: " + operatorSymbol);
                                    break;
                            }
                        }
                        break;

                    case "int":
                        if (propertyValue is int intValue)
                        {
                            if (int.TryParse(fieldValue, out int parsedValue))
                            {
                                switch (operatorSymbol)
                                {
                                    case "=":
                                        return intValue == parsedValue;
                                    case ">":
                                        return intValue > parsedValue;
                                    case ">=":
                                        return intValue >= parsedValue;
                                    case "<":
                                        return intValue < parsedValue;
                                    case "<=":
                                        return intValue <= parsedValue;
                                    default:
                                        Console.WriteLine("Unknown operator: " + operatorSymbol);
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid field value: " + fieldValue);
                            }
                        }
                        break;


                    default:
                        Console.WriteLine("Field type not supported: " + propertyType);
                        break;
                }
            }

            return false;
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Find ").Append(className).Append(" ");

            foreach (string req in requirements)
            {
                sb.Append(req).Append(" ");
            }

            return sb.ToString();
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
