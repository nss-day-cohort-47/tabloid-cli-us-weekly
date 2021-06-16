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

        //Hunter's code to Add  Blog-----------------------------------------
        private void Add()
        {
            Blog blog = new Blog();

            Console.Write("Title: ");
            blog.Title = Console.ReadLine();

            Console.Write("URL: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);
        }
        //Hunter's code to Add  Blog-----------------------------------------

        private void Edit()
        {
            Journal blogToEdit = Choose("Which blog entry would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.WriteLine();
            Console.Write("New URL (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                blogToEdit.Content = content;
            }
            _blogRepository.Update(blogToEdit);

        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
