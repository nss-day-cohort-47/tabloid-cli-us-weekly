using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;


        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _journalRepository = new JournalRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Jouranl Menu");
            Console.WriteLine(" 1) List all journal entries");
            Console.WriteLine(" 2) Add a new journal entry");
            Console.WriteLine(" 3) Edit a journal entry");
            Console.WriteLine(" 4) Delete a journal entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListEntries();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void ListEntries()
        {
            List<Journal> AllEntries = _journalRepository.GetAll();
            foreach (Journal j in AllEntries)
            {
                Console.WriteLine($"{j.Id} ) {j.Title}");
                Console.WriteLine($"Created on: {j.CreateDateTime}");
                Console.WriteLine($"{j.Content}");
                Console.WriteLine("-------------------------------");
            }
        }

        private void Add()
        {
            Journal newEntry = new Journal();

            Console.Write("Title: ");
            newEntry.Title = Console.ReadLine();

            Console.Write("Content: ");
            newEntry.Content = Console.ReadLine();

            _journalRepository.Insert(newEntry);
            Console.WriteLine($"Journal entry {newEntry.Title} has been added");
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Journal Entry:";
            }

            Console.WriteLine(prompt);

            List<Journal> entries = _journalRepository.GetAll();

            for (int i = 0; i < entries.Count; i++)
            {
                Journal entry = entries[i];
                Console.WriteLine($" {i + 1}) {entry.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return entries[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Journal entryToEdit = Choose("Which journal entry would you like to edit?");
            if (entryToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                entryToEdit.Title = title;
            }
            Console.WriteLine();
            Console.Write("New Content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                entryToEdit.Content = content;
            }
            _journalRepository.Update(entryToEdit);
        }

        private void Remove()
        {
            Journal journalToDelete = Choose("Which entry would you like to remove?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
    }
}
