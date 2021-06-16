using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;


        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add Blog");
            Console.WriteLine(" 3) Edit Blog");
            Console.WriteLine(" 4) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
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
        // -------------------------------------------------- cd  --------------------------------------------------------------------------
        /// <summary>
        /// Get and display all blog posts
        /// </summary>
        private void List()
        {
            Console.WriteLine($"{String.Format("{0,-5}","Id")}{String.Format("{0,-30}", " Title")}{String.Format("{0,-30}", " URL")}");
            foreach (Blog b in _blogRepository.GetAll())
            {
                Console.WriteLine($"{String.Format("{0,-5}", b.Id + ":")}{String.Format("{0,-30}", b.Title)}{String.Format("{0,-30}", b.Url )}");
            }
            Console.WriteLine();
        }
        // -------------------------------------------------- cd  --------------------------------------------------------------------------

        private void Add()
        {
            throw new NotImplementedException();
        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
