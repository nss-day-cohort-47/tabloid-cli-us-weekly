using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private string _connectionString;

        public BackgroundColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
        }

        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Color Menu");
            Console.WriteLine(" 1) Black");
            Console.WriteLine(" 2) Dark Blue");
            Console.WriteLine(" 3) Dark Green");
            Console.WriteLine(" 4) Dark Magenta");
            Console.WriteLine(" 5) Dark Gray");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("You have chosen Black");
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("You have chosen Dark Blue");
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("You have chosen Dark Green");
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("You have chosen Dark Magenta");
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("You have chosen Dark Gray");
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
            
        }

        private void List()
        {
            throw new NotImplementedException();
        }

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
