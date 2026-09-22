using System;
using System.Collections.Generic;
using System.IO;

namespace BookManagementSystem
{
    public class BookDataAccess
    {
        private readonly string _filePath = "books.txt";
        private readonly string _backupFilePath = "books_backup.txt";

        public void AddBook(Book book)
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(book.ToFileString());
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            if (!File.Exists(_filePath))
            {
                return books;
            }

            using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            try
                            {
                                int id = int.Parse(parts[0].Trim());
                                string title = parts[1].Trim();
                                string author = parts[2].Trim();
                                double price = double.Parse(parts[3].Trim());

                                Book book = new Book
                                {
                                    Id = id,
                                    Title = title,
                                    Author = author,
                                    Price = price
                                };
                                books.Add(book);
                            }
                            catch (FormatException)
                            {
                                // Skip malformed lines
                            }
                        }
                    }
                }
            }

            return books;
        }

        public Book FindBookById(int id)
        {
            if (!File.Exists(_filePath)) return null;

            using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(',');
                        if (parts.Length >= 1 && int.TryParse(parts[0].Trim(), out int currentId))
                        {
                            if (currentId == id && parts.Length == 4)
                            {
                                return new Book(
                                    currentId, 
                                    parts[1].Trim(), 
                                    parts[2].Trim(), 
                                    double.Parse(parts[3].Trim())
                                );
                            }
                        }
                    }
                }
            }
            return null; // Not found
        }

        public bool CreateBackup()
        {
            if (!File.Exists(_filePath))
            {
                return false; // Nothing to backup
            }

            using (FileStream sourceStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream destStream = new FileStream(_backupFilePath, FileMode.Create, FileAccess.Write))
                {
                    int byteRead;
                    while ((byteRead = sourceStream.ReadByte()) != -1)
                    {
                        destStream.WriteByte((byte)byteRead);
                    }
                }
            }
            return true;
        }
    }
}