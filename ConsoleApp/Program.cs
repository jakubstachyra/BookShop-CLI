// See https://aka.ms/new-console-template for more information
using Bajtpik.Data;
using ConsoleApp;
using ConsoleApp.Command;
using System.Collections.Generic;
using Bajtpik.Data.Interfaces;

Console.Write("Bajtpik, buy a book! - for help type:");
Console.ForegroundColor = ConsoleColor.DarkCyan;
Console.Write(" help\n");
Console.ForegroundColor = ConsoleColor.White;
CommandProcessor commandProcessor = new CommandProcessor();

// Create and register your collections
List<IEntity> AuthorsCollection = ObjectFactory.CreateAuthors(); // Replace with your actu
commandProcessor.RegisterCollection("authors", AuthorsCollection);
commandProcessor.RegisterCollection("author", AuthorsCollection);

List<IEntity> BooksCollection = ObjectFactory.CreateBooks(AuthorsCollection); // Replace with your actu
commandProcessor.RegisterCollection("books", BooksCollection);
commandProcessor.RegisterCollection("book", BooksCollection);

List<IEntity> NewspapersCollection = ObjectFactory.CreateNewspapers(); // Replace with your actu
commandProcessor.RegisterCollection("newspapers", NewspapersCollection);
commandProcessor.RegisterCollection("newspaper", NewspapersCollection);

List<IEntity> BoardGamesCollection = ObjectFactory.CreateBoardGames(AuthorsCollection); // Replace with your actu
commandProcessor.RegisterCollection("boardgames", BoardGamesCollection);
commandProcessor.RegisterCollection("boardgame", BoardGamesCollection);



bool isRunning = true;
while (isRunning)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.Write(">");
    Console.ForegroundColor = ConsoleColor.White;
    string command = Console.ReadLine();
    CommandProcessor.ProcessCommand(command);
}