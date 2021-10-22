using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyApp
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Client> Clients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SomeDataBase;Trusted_Connection=true");
        }
    }
    public class Client
    {
        public Client(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
    class Table
    {
        private string[][] table;
        private int[] maxLRaw;
        private int raws = 0;
        private readonly int cols;
        public Table(params string[] firstRaw)
        {
            cols = firstRaw.Length;
            maxLRaw = new int[cols];
            for (int i = 0; i < cols; i++)
                maxLRaw[i] = 0;
            AddRaw(firstRaw);
        }
        public void AddRaw(params string[] raw)
        {
            Array.Resize<string[]>(ref table, raws + 1);
            table[^1] = new string[cols];
            for (int i = 0; i < cols; i++)
            {
                table[raws][i] = raw[i];
                maxLRaw[i] = raw[i].Length > maxLRaw[i] ? raw[i].Length : maxLRaw[i];
            }
            raws++;
        }
        public void ShowTable()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            StringBuilder sb = new StringBuilder(Sum() + cols + 1);
            sb.Append('|').Append(' ', maxLRaw[0] - table[0][0].Length).Append(table[0][0]).Append('|');
            for (int i = 1; i < cols; i++)
                sb.Append(' ', maxLRaw[i] - table[0][i].Length).Append(table[0][i]).Append('|');
            System.Console.WriteLine(sb);
            sb.Clear();
            sb.Append('|').Append('-', maxLRaw[0]).Append('|');
            for (int i = 1; i < cols; i++)
                sb.Append('-', maxLRaw[i]).Append('|');
            System.Console.WriteLine(sb);
            sb.Clear();
            for (int i = 1; i < raws; i++)
            {
                sb.Append('|').Append(' ', maxLRaw[0] - table[i][0].Length).Append(table[i][0]).Append('|');
                for (int j = 1; j < cols; j++)
                    sb.Append(' ', maxLRaw[j] - table[i][j].Length).Append(table[i][j]).Append('|');
                System.Console.WriteLine(sb);
                sb.Clear();
            }
            Console.ResetColor();
        }
        private int Sum()
        {
            int sum = 0;
            for (int i = 0; i < cols; i++)
                sum += ++maxLRaw[i];
            return sum;
        }
    }
}