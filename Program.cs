using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Insert(new Client("Ahmad", 100), new Client("Umar", 200), new Client("Ikrom", 300));
            await Select();
            System.Console.WriteLine();
            await Update(new Client("Jovid", 500), 1);
            await Select();
            System.Console.WriteLine();
            await Delete(1);
            await Select();
        }
        public async static Task Insert(params Client[] clients)
        {
            using var db = new DataBaseContext();
            await db.Clients.AddRangeAsync(clients);
            await db.SaveChangesAsync();
        }
        public async static Task Select()
        {
            using var db = new DataBaseContext();
            Table table1 = new Table("ID", "Name", "Balance");
            await foreach (var item in db.Clients.AsAsyncEnumerable())
                table1.AddRaw(item.Id.ToString(), item.Name, item.Balance.ToString());
            table1.ShowTable();
        }
        public async static Task Update(Client updated, int id)
        {
            using var db = new DataBaseContext();
            Client client = await db.Clients.FirstAsync(x => x.Id == id);
            if (client == null) return;
            client.Balance = updated.Balance;
            client.Name = updated.Name;
            await db.SaveChangesAsync();
        }
        public async static Task Delete(int id)
        {
            using var db = new DataBaseContext();
            Client client = await db.Clients.FirstAsync(x => x.Id == id);
            if (client == null) return;
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
        }
    }
}