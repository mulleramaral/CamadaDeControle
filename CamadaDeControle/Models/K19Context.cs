using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace CamadaDeControle.Models
{
    public class K19Context : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public K19Context()
        {
            Database.Log = x => Console.Write(x);
        }
    }
}