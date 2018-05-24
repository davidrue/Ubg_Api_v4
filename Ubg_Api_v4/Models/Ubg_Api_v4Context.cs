using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ubg_Api_v4.Models
{
    public class Ubg_Api_v4Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Ubg_Api_v4Context() : base("name=Ubg_Api_v4Context")
        {
        }

        public System.Data.Entity.DbSet<Ubg_Api_v4.Models.Actor> Actors { get; set; }

        public System.Data.Entity.DbSet<Ubg_Api_v4.Models.BankAccount> BankAccounts { get; set; }
    }
}
