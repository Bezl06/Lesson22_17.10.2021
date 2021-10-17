using System;
using MyDataBase;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new DataBaseContext();
            db.Clients.AddRange(
                new Client() { Name = "Hokim", Balance = 100 },
                new Client() { Name = "Osim", Balance = 200 },
                new Client() { Name = "Umed", Balance = 300 }
            );
            db.SaveChanges();
            foreach (var item in db.Clients)
            {
                System.Console.WriteLine($"Name : {item.Name}, Balance : {item.Balance}");
            }
        }
    }
}