using System;
using System.Collections.Generic;
using System.IO;

namespace BookManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BookDataAccess dal = new BookDataAccess();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Book Management System ===");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Find Book by ID");
                Console.WriteLine("4. Create Backup");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBookUI(dal);
                        break;
                    case "2":
                        ViewAllBooksUI(dal);
                        break;
                    case "3":
                        FindBookByIdUI(dal);
                        break;
                    case "4":
                        CreateBackupUI(dal);
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void AddBookUI(BookDataAccess dal)
        {
            Console.WriteLine("\n-- Add New Book --");
            
            try
            {
                Console.Write("Enter ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Author: ");
                string author = Console.ReadLine();

                Console.Write("Enter Price: ");
                double price = double.Parse(Console.ReadLine());

                Book newBook = new Book
                {
                    Id = id,
                    Title = title,
                    Author = author,
                    Price = price
                };

                dal.AddBook(newBook);
                Console.WriteLine("Book added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for ID or Price. Please enter numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void ViewAllBooksUI(BookDataAccess dal)
        {
            Console.WriteLine("\n-- All Books --");
            try
            {
                List<Book> books = dal.GetAllBooks();

                if (books.Count == 0)
                {
                    Console.WriteLine("No books found.");
                }
                else
                {
                    foreach (Book book in books)
                    {
                        book.DisplayInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred reading the file: {ex.Message}");
            }
        }

        private static void FindBookByIdUI(BookDataAccess dal)
        {
            Console.WriteLine("\n-- Find Book by ID --");
            Console.Write("Enter Book ID to search: ");
            
            if (int.TryParse(Console.ReadLine(), out int searchId))
            {
                try
                {
                    Book foundBook = dal.FindBookById(searchId);

                    if (foundBook != null)
                    {
                        Console.WriteLine("\nBook Found:");
                        foundBook.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred searching the file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
            }
        }

        private static void CreateBackupUI(BookDataAccess dal)
        {
            Console.WriteLine("\n-- Create Backup --");
            try
            {
                bool success = dal.CreateBackup();
                if (success)
                {
                    Console.WriteLine("Backup created successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to create backup. The original file might not exist yet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred creating the backup: {ex.Message}");
            }
        }
    }
}