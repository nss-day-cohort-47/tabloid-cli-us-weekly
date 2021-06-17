using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    NoteManage();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid selection");
                    return this;
            }
        }

        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} --- URL: {post.Url} --- Published: {post.PublishDateTime}");
        }
        private void AddTag()
        {
            throw new NotImplementedException();
        }
        private void RemoveTag()
        {
            Tag TagToDel = ChooseTag("Which post would you like to edit?");
            try
            {
                if (TagToDel != null) {
                    int choice = int.Parse(TagToDel.Id.ToString());
                    _postRepository.DeleteTag(_postId, choice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't remove any tags.");
            }
            _postRepository.DeleteTag(_postId, TagToDel.Id);

        }
        private void NoteManage()
        {
            throw new NotImplementedException();
        }

        private Tag ChooseTag(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please select a Post:";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _postRepository.GetPoatTags(_postId);

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) |   Tag: {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

    }
}
