using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostDetailManager _postDetailManager;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List all posts");
            Console.WriteLine(" 2) Add a new post");
            Console.WriteLine(" 3) Edit a post");
            Console.WriteLine(" 4) Delete a post");
            Console.WriteLine(" 5) View a post's details");
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
                case "5":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> postList = _postRepository.GetAll();
            foreach (Post p in postList)
            {
                Console.WriteLine(@$"
    {p.Id} ) [ Title: {p.Title} ]  
             [ URL: {p.Url} ]  
             [ Creation Date: {p.PublishDateTime} ] 
");
            }

        }

        private void Add()
        {
            Post newPost = new Post();


            Console.Write("Title: ");
            newPost.Title = Console.ReadLine();


            Console.Write("URL: ");
            newPost.Url = Console.ReadLine();


            List<Author> authors = _authorRepository.GetAll();
            foreach (Author a in authors)
            {
                Console.WriteLine($"{a.Id}) {a.FullName}");
            }
            Console.Write("Enter the number for the desired author: ");
            newPost.Author = authors[int.Parse(Console.ReadLine()) - 1];


            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog b in blogs)
            {
                Console.WriteLine($"{b.Id}) {b.Title}");
            }
            Console.Write("Enter the number for the desired blog: ");
            newPost.Blog = blogs[int.Parse(Console.ReadLine()) - 1];


            _postRepository.Insert(newPost);
            Console.WriteLine("Post added successfully");
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please select a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) Title: {post.Title}   |   URL: {post.Url}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }


        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.WriteLine();
            Console.Write("New URL (blank to leave unchanged: ");
            string URL = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(URL))
            {
                postToEdit.Url = URL;
            }
            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }

        private PostDetailManager SeeDetails()
        {
            Console.WriteLine("Which post do you want to see details for?");
            List<Post> posts = _postRepository.GetAll();
            foreach (Post p in posts)
            {
                Console.WriteLine($" {p.Id}) {p.Title}");
            }
            Console.Write("Enter the number for your choice of post > ");
            int postResponse = int.Parse(Console.ReadLine());
            Post selectedPost = posts[postResponse - 1];

            return new PostDetailManager(this, _connectionString, selectedPost.Id);
        }

    }
}
