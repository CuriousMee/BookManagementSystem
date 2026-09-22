using System;

namespace BookManagementSystem
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public Book()
        {
        }

        public Book(int id, string title, string author, double price)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id,-5} | Title: {Title,-30} | Author: {Author,-20} | Price: {Price:C}");
        }
        
        public string ToFileString()
        {
            return $"{Id}, {Title}, {Author}, {Price}";
        }
    }
}